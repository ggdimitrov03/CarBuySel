(() => {
  const prefersReduced = window.matchMedia("(prefers-reduced-motion: reduce)").matches;
  const pageContent = document.getElementById("pageContent");
  const themeToggleBtn = document.getElementById("themeToggle");
  const themeStorageKey = "carbuy-theme";
  const docElement = document.documentElement;
  const lightbox = document.getElementById("lightbox");
  const lightboxImage = lightbox?.querySelector("img");
  const header = document.querySelector(".shadowed-header");

  const handleNavigation = (event) => {
    const anchor = event.currentTarget;
    const url = anchor.getAttribute("href");

    if (!url || url.startsWith("#") || anchor.dataset.instant === "true") {
      return;
    }

    if (anchor.target === "_blank" || anchor.host !== window.location.host) {
      return;
    }

    event.preventDefault();

    if (prefersReduced || !pageContent) {
      window.location.href = url;
      return;
    }

    document.body.classList.add("is-transitioning");
    setTimeout(() => {
      window.location.href = url;
    }, 280);
  };

  const initPageTransitions = () => {
    if (!pageContent || prefersReduced) return;
    document.querySelectorAll("a").forEach((anchor) => {
      anchor.removeEventListener("click", handleNavigation);
      anchor.addEventListener("click", handleNavigation);
    });

    window.addEventListener("pageshow", () => {
      document.body.classList.remove("is-transitioning");
    });
  };

  const animateStats = () => {
    if (prefersReduced) return;
    const stats = document.querySelectorAll(".stat-number");
    stats.forEach((stat) => {
      const finalValue = stat.textContent.trim();
      stat.dataset.target = finalValue;
      let frame = 0;

      const animation = () => {
        frame += 1;
        stat.textContent = frame % 2 === 0 ? finalValue : finalValue.replace(/./g, "•");
        if (frame < 6) {
          requestAnimationFrame(animation);
        } else {
          stat.textContent = finalValue;
        }
      };

      animation();
    });
  };

  const revealOnScroll = () => {
    if (prefersReduced || typeof IntersectionObserver === "undefined") return;
    const observed = document.querySelectorAll(".glass-card, .hero-panel, .car-card, .feature-card, .recommend-card");

    const observer = new IntersectionObserver(
      (entries) => {
        entries.forEach((entry) => {
          if (entry.isIntersecting) {
            entry.target.classList.add("in-view");
            observer.unobserve(entry.target);
          }
        });
      },
      { threshold: 0.2 }
    );

    observed.forEach((element) => observer.observe(element));
  };

  const handleHeaderScroll = () => {
    if (!header) return;
    header.classList.toggle("is-scrolled", window.scrollY > 16);
  };

  const initHeaderState = () => {
    handleHeaderScroll();
    window.addEventListener("scroll", handleHeaderScroll, { passive: true });
  };

  const applyTheme = (nextTheme) => {
    const normalized = nextTheme === "light" ? "light" : "dark";
    docElement.dataset.theme = normalized;
    localStorage.setItem(themeStorageKey, normalized);
  };

  const initThemeToggle = () => {
    if (!themeToggleBtn) return;
    const stored = localStorage.getItem(themeStorageKey);
    if (stored) {
      applyTheme(stored);
    }

    themeToggleBtn.addEventListener("click", () => {
      const current = docElement.dataset.theme ?? "dark";
      const next = current === "dark" ? "light" : "dark";
      applyTheme(next);
    });
  };

  const openLightbox = (src) => {
    if (!lightbox || !lightboxImage) return;
    lightboxImage.src = src;
    lightbox.removeAttribute("hidden");
    document.body.classList.add("lightbox-open");
  };

  const closeLightbox = () => {
    if (!lightbox) return;
    lightbox.setAttribute("hidden", "hidden");
    document.body.classList.remove("lightbox-open");
  };

  const changeGalleryImage = (target, newSrc, newIndex, targetId) => {
    if (!target || !newSrc) return;
    
    // Fade out
    target.classList.add("fade-out");
    target.classList.remove("fade-in");
    
    // Update active thumbnail
    if (targetId) {
      const thumbsForGallery = document.querySelectorAll(
        `[data-gallery-thumb-for="${targetId}"]`
      );
      thumbsForGallery.forEach(thumb => thumb.classList.remove("active"));
      const activeThumb = Array.from(thumbsForGallery).find(
        thumb => thumb.dataset.galleryIndex === newIndex?.toString()
      );
      if (activeThumb) {
        activeThumb.classList.add("active");
      }
    }
    
    // Change image after fade out starts
    setTimeout(() => {
      target.setAttribute("src", newSrc);
      target.dataset.full = newSrc;
      if (newIndex !== undefined) {
        target.dataset.galleryIndex = newIndex.toString();
      }
      
      // Fade in
      requestAnimationFrame(() => {
        target.classList.remove("fade-out");
        target.classList.add("fade-in");
      });
    }, 200); // Half of the transition duration
  };

  const initGallery = () => {
    // Initialize active thumbnail for each gallery
    document.querySelectorAll("[data-gallery-index]").forEach((mainImg) => {
      const targetId = mainImg.id;
      const currentIndex = mainImg.dataset.galleryIndex ?? "0";
      const thumbsForGallery = document.querySelectorAll(
        `[data-gallery-thumb-for="${targetId}"]`
      );
      thumbsForGallery.forEach(thumb => {
        if (thumb.dataset.galleryIndex === currentIndex) {
          thumb.classList.add("active");
        }
      });
      // Ensure initial image has fade-in class
      mainImg.classList.add("fade-in");
    });

    const thumbs = document.querySelectorAll("[data-gallery-thumb-for]");
    thumbs.forEach((thumb) => {
      const targetId = thumb.getAttribute("data-gallery-thumb-for");
      const target = document.getElementById(targetId);
      if (!target) return;

      thumb.addEventListener("click", () => {
        const full = thumb.dataset.full || thumb.getAttribute("src");
        if (!full) return;
        const thumbIndex = thumb.dataset.galleryIndex;
        changeGalleryImage(target, full, thumbIndex, targetId);
      });
    });

    document.querySelectorAll("[data-gallery='listing']").forEach((image) => {
      image.addEventListener("click", () => {
        const full = image.dataset.full || image.getAttribute("src");
        if (full) {
          openLightbox(full);
        }
      });
    });

    lightbox?.addEventListener("click", (event) => {
      if (event.target === lightbox || event.target.hasAttribute("data-lightbox-close")) {
        closeLightbox();
      }
    });

    window.addEventListener("keydown", (event) => {
      if (event.key === "Escape") {
        closeLightbox();
      }
    });

    document.querySelectorAll("[data-gallery-nav]").forEach((button) => {
      button.addEventListener("click", (event) => {
        event.preventDefault();
        event.stopPropagation();
        
        const targetId = button.getAttribute("data-target");
        const direction = button.getAttribute("data-direction") ?? "next";
        const target = document.getElementById(targetId);
        if (!target) return;

        const thumbsForGallery = document.querySelectorAll(
          `[data-gallery-thumb-for="${targetId}"]`
        );
        if (!thumbsForGallery.length) return;

        const currentIndex = Number(target.dataset.galleryIndex ?? "0");
        const maxIndex = thumbsForGallery.length;
        let nextIndex = currentIndex;

        if (direction === "prev") {
          nextIndex = (currentIndex - 1 + maxIndex) % maxIndex;
        } else {
          nextIndex = (currentIndex + 1) % maxIndex;
        }

        const nextThumb = thumbsForGallery[nextIndex];
        if (nextThumb) {
          const full = nextThumb.dataset.full || nextThumb.getAttribute("src");
          const thumbIndex = nextThumb.dataset.galleryIndex;
          changeGalleryImage(target, full, thumbIndex, targetId);
        }
      });
    });
  };

  const initCategoryChips = () => {
    const chips = document.querySelectorAll(".category-chip");
    if (!chips.length) return;
    chips.forEach((chip) => {
      chip.addEventListener("pointerdown", () => {
        chips.forEach((c) => c.classList.remove("is-active"));
        chip.classList.add("is-active");
      });
    });
  };

  document.addEventListener("DOMContentLoaded", () => {
    initPageTransitions();
    animateStats();
    revealOnScroll();
    initThemeToggle();
    initGallery();
    initHeaderState();
    initCategoryChips();
  });
})();

