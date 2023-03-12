$(function() {
  "use strict";
  // ==============================================================
  //This is for preloader
  // ==============================================================
  $(function() {
    $(".preloader").fadeOut();
  });
  // ==============================================================
  //Tooltip
  // ==============================================================
  $(function() {
    $('[data-toggle="tooltip"]').tooltip();
  });
  // ==============================================================
  //Popover
  // ==============================================================
  //$(function() {
  //  $('[data-toggle="popover"]').popover();
  //});
  // ==============================================================
  // For mega menu
  // ==============================================================
  jQuery(document).on("click", ".mega-dropdown", function(e) {
    e.stopPropagation();
  });
  jQuery(document).on("click", ".navbar-nav > .dropdown", function(e) {
    e.stopPropagation();
  });
  $(".dropdown-submenu").click(function() {
    $(".dropdown-submenu > .dropdown-menu").toggleClass("show");
  });
  // ==============================================================
  // Resize all elements
  // ==============================================================
  $("body").trigger("resize");
  // ==============================================================
  //Fix header while scroll
  // ==============================================================
  var wind = $(window);
  wind.on("load", function() {
    var bodyScroll = wind.scrollTop(),
      navbar = $(".topbar");
    if (bodyScroll > 100) {
      navbar.addClass("fixed-header animated slideInDown");
    } else {
      navbar.removeClass("fixed-header animated slideInDown");
    }
  });
  $(window).scroll(function() {
    if ($(window).scrollTop() >= 300) {
      $(".topbar").addClass("fixed-header animated slideInDown");
      $(".bt-top").addClass("visible");
    } else {
      $(".topbar").removeClass("fixed-header animated slideInDown");
      $(".bt-top").removeClass("visible");
    }
  });
  // ==============================================================
  // Animation initialized
  // ==============================================================
    AOS.init();
    AOS.init({
        easing: 'ease-out-back',
        duration: 800,
        delay: 300,
        once: true,
        disable: 'mobile'
});
  // ==============================================================
  // Back to top
  // ==============================================================
  $(".bt-top").on("click", function(e) {
    e.preventDefault();
    $("html,body").animate(
      {
        scrollTop: 0
      },
      700
    );
  });
  $(".navbar-brand-home").on("click", function(e) {
    e.preventDefault();
    $("html,body").animate(
      {
        scrollTop: 0
      },
      700
    );
  });
});

const egxbuttons = document.querySelectorAll(".egxbutton");
const egxlabels = document.querySelectorAll(".egxlabel");
const marketBreadthbuttons = document.querySelectorAll(".marketBreadthbutton");

const tabBtnsHandler = (btn, sectionTabBtns) => {
  sectionTabBtns.forEach(el => {
    el.classList.remove("active");
  });
  btn.classList.add("active");
};

for (let btn of egxbuttons) {
  btn.addEventListener("click", tabBtnsHandler.bind(null, btn, egxbuttons));
}

for (let btn of egxlabels) {
  btn.addEventListener("click", tabBtnsHandler.bind(null, btn, egxlabels));
}

for (let btn of marketBreadthbuttons) {
  btn.addEventListener(
    "click",
    tabBtnsHandler.bind(null, btn, marketBreadthbuttons)
  );
}

//const ticker = document.querySelector(".ticker-wrap .ticker");
//const tickerItems = document.querySelectorAll(".ticker__item");

//ticker.style.animationDuration = `${tickerItems.length * 2}s`;

//ticker.addEventListener("mouseenter", () => {
//  ticker.style.animationPlayState = "paused";
//});

//ticker.addEventListener("mouseleave", () => {
//  ticker.style.animationPlayState = "running";
//});