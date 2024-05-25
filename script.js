const elsosor = document.querySelectorAll(".elsmezo");
const elsogomb = document.querySelector("#elsogomb");
const masodiksor = document.querySelectorAll(".masodmezo");
const masodikgomb = document.querySelector("#masodikgomb");
console.log(elsosor);
console.log(elsosor[0]);

function KockaDobas() {
  let rnd = Math.floor(Math.random() * 6) + 1;
  document.querySelector("#dobottszam").innerHTML = rnd;
}

function MozgasElso() {
  let current = 0;
  let lepett = Number(document.querySelector("#dobottszam").innerHTML);

  for (let i = 0; i < elsosor.length; i++) {
    if (elsosor[i].classList.contains("p1")) current = i;
  }

  if (current + lepett < 19) {
    elsosor[current].classList.remove("p1");
    elsosor[current + lepett].classList.add("p1");
  } 
  
  else if (current + lepett == 19) {
    elsosor[current].classList.remove("p1");
    elsosor[current + lepett].classList.add("p1");

    document.querySelector("body").style.backgroundColor = "lime";

    elsogomb.style.display="none"
    masodikgomb.style.display="none"

    document.querySelector("#dobottszam").innerHTML = "P1 Nyert.";

    console.log("dsa")
    console.log(current + lepett)
  } 
  
  else {
    elsosor[current].classList.remove("p1");
    elsosor[0].classList.add("p1");
  }

  elsogomb.disabled=true;
  masodikgomb.disabled=false;
  console.log(current + lepett)
}

function MozgasMasodik() {
  let current = 0;
  let lepett = Number(document.querySelector("#dobottszam").innerHTML);

  for (let i = 0; i < masodiksor.length; i++) {
    if (masodiksor[i].classList.contains("p2")) current = i;
  }

  if (current + lepett < 19) {
    masodiksor[current].classList.remove("p2");
    masodiksor[current + lepett].classList.add("p2");
  } 
  
  else if (current + lepett == 19) {
    masodiksor[current].classList.remove("p2");
    masodiksor[current + lepett].classList.add("p2");

    document.querySelector("body").style.backgroundColor = "darkred";

    elsogomb.style.display="none"
    masodikgomb.style.display="none"

    document.querySelector("#dobottszam").innerHTML = "P2 Nyert.";

    console.log("dsa")
    console.log(current + lepett)
  } 
  
  else {
    masodiksor[current].classList.remove("p2");
    masodiksor[0].classList.add("p2");
  }
  masodikgomb.disabled=true;
  elsogomb.disabled=false;
  console.log(current + lepett)
}

elsogomb.addEventListener("click", KockaDobas);
elsogomb.addEventListener("click", MozgasElso);
masodikgomb.addEventListener("click", KockaDobas);
masodikgomb.addEventListener("click", MozgasMasodik);
