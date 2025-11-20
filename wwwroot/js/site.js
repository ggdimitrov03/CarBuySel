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

  const initGallery = () => {
    const thumbs = document.querySelectorAll("[data-gallery-thumb-for]");
    thumbs.forEach((thumb) => {
      const targetId = thumb.getAttribute("data-gallery-thumb-for");
      const target = document.getElementById(targetId);
      if (!target) return;

      thumb.addEventListener("click", () => {
        const full = thumb.dataset.full || thumb.getAttribute("src");
        if (!full) return;
        target.setAttribute("src", full);
        target.dataset.full = full;
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

