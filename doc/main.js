var copyIcon = '<svg viewBox="0 0 0.45 0.45"><path d="M 0.405,0 H 0.18 C 0.154075,0 0.135,0.019074 0.135,0.045 v 0.09 H 0.045 C 0.019074,0.135 0,0.154076 0,0.18 V 0.405 C 0,0.430925 0.019074,0.45 0.045,0.45 H 0.27 c 0.025925,0 0.045,-0.019076 0.045,-0.045 v -0.09 h 0.09 C 0.430925,0.315 0.45,0.295924 0.45,0.27 V 0.045 C 0.45,0.019074 0.430925,0 0.405,0 Z m -0.36,0.18 h 0.09 v 0.09 c 0,0.025924 0.019075,0.045 0.045,0.045 h 0.09 v 0.09 H 0.045 Z M 0.18,0.27 V 0.045 H 0.405 V 0.27 Z" /></svg>';
var okIcon = '<svg viewBox="0 0 0.45 0.45"><path d="m 0.02496763,0.29617244 q -0.0058404,-0.006132 -0.0090526,-0.0105126 -0.0029202,-0.00438 -0.0029202,-0.008469 0,-0.005256 0.0049643,-0.0105127 0.0052563,-0.005256 0.01314081,-0.009345 0.0081765,-0.004088 0.01752109,-0.006424 0.0096366,-0.002628 0.01839714,-0.002628 0.0093446,0 0.01518493,0.002628 0.0061324,0.002336 0.01168073,0.008177 0.02248539,0.0236535 0.04059051,0.048475 0.0183971,0.0245295 0.0359182,0.0508111 0.016937,-0.0452628 0.0350422,-0.0873134 0.0181051,-0.0423426 0.0382544,-0.0826411 0.0204413,-0.0405905 0.0435107,-0.0797209 0.0233614,-0.03942244 0.0505191,-0.07913689 0.009929,-0.01489293 0.0292018,-0.02190136 0.0195652,-0.007008433 0.0499351,-0.007008433 0.009053,0 0.0146009,0.002628163 0.005548,0.00262816 0.005548,0.00700843 0,0.0037962 -0.001752,0.0075925 -0.001752,0.0037962 -0.005256,0.0087605 -0.0662881,0.09256973 -0.12060346,0.19389999 -0.0543154,0.10133027 -0.0943218,0.21375722 -0.002628,0.008177 -0.0128488,0.0122648 -0.0102206,0.004088 -0.0300779,0.004088 -0.009345,0 -0.015769,-5.8403e-4 -0.006424,-5.8404e-4 -0.0108047,-0.002336 -0.00438,-0.00146 -0.0073,-0.003796 -0.00292,-0.002336 -0.004964,-0.00584 Q 0.12074955,0.41765195 0.10877681,0.40013087 0.09709609,0.38231777 0.08424729,0.36567274 0.07169051,0.34902771 0.05708961,0.33209066 0.04278073,0.31515361 0.02496763,0.29617244 Z" /></svg>';
document.addEventListener('DOMContentLoaded', function ()
{
  [...document.querySelectorAll("[role=tab]")].forEach((tab) => {
    tab.addEventListener("click", (e) => {
      e.preventDefault();

      if (!e.target.hasAttribute("aria-current")) {
        e.target.setAttribute("aria-current", true);
      }

      const parent = e.target.closest("div[role=sample]");
      const find = (el) => [...parent.querySelectorAll(el)];

      find("[role=tab]").forEach((t) => {
        if (t.hasAttribute("aria-current") && t != e.target) {
          t.removeAttribute("aria-current");
        }
      });

      const currentIndex = find("[role=tab]").indexOf(e.target);
      const tabPanels = find("[role=tabpanel]");
      tabPanels.forEach((tp) => {
        if (tabPanels.indexOf(tp) == currentIndex) {
          tp.removeAttribute("hidden");
        } else {
          tp.setAttribute("hidden", true);
        }
      });
    });
  });
  if (navigator.clipboard && navigator.clipboard.writeText) {
    [...document.querySelectorAll("div[role=tabpanel]")].forEach((div) => {
      const span = document.createElement('span');
      span.innerHTML = copyIcon;
      span.role = 'tabcopy';
      div.appendChild(span);
      const isMainPanel =  !div.hasAttribute("hidden");
      const pre = div.getElementsByTagName('pre')[0];
      span.onclick = function (e)
      {
        const content = isMainPanel ? pre.outerHTML : pre.innerText;
        navigator.clipboard.writeText(content.trim()).then(function ()
        {
          span.innerHTML = okIcon;
          setTimeout(function ()
            {
              span.innerHTML = copyIcon;
            }, 2000);
        });
      }
    });
  }
});
