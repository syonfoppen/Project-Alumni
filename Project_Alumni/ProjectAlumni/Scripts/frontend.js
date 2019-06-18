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