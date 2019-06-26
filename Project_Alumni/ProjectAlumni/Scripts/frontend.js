let x = false; 
function nav() {
	if (x == false) {
		document.getElementById("navbar").style.transition = "right 0.25s";
		document.getElementById("navbar").style.right = "-450px";
		x = true;
	}
	else {
		document.getElementById("navbar").style.transition = "right 0.25s";
		document.getElementById("navbar").style.right = "-10px";
		x = false
	}
}
let y = false;
function mobilenav() {
    if (y == false) {
        y = true;
        document.getElementById("main").style.transition = "width 0.25s";
        document.getElementById("mobile-links").style.transition = "right 0.25s ease-in-out";
        document.getElementById("main").style.width = "98vw";
        document.getElementById("mobile-links").style.right = "-8vw";
    }
    else {
        y = false;
        document.getElementById("main").style.transition = "width 0.25s ease-in-out";
        document.getElementById("mobile-links").style.transition = "right 0.25s ease-in-out";
        document.getElementById("main").style.width = "200px";
        document.getElementById("mobile-links").style.right = "-110vw";
    }
}
// if pressed mobile-nav width  100% and right:0; of mobile-links
let newsToggle = false;
function news() {
    if (newsToggle == false) {
        newsToggle = true;
        document.getElementById('information').scrollIntoView();
    }
    else {
        newsToggle = false;
        document.getElementById('top').scrollIntoView();
    }
    
};