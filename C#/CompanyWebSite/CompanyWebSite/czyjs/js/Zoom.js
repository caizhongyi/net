         var i = 0;
          document.onkeydown = zoom;
          function zoom() {
              var IEKey = event.keyCode;
              if (IEKey == 66) {
                  i++;
                  document.body.style.zoom = 1 + i / 100;
              }
              if (IEKey == 68) {
                  i--;
                  document.body.style.zoom = 1 + i / 100;
              }
              if (IEKey == 82) {
                  document.body.style.zoom = 1;
                  i = 1;
              }
          }