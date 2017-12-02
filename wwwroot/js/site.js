// Write your JavaScript code.
var inspirations = ["Thought Leader", "Genius", "Thinkgineer", "Industrialist", "Solver", "Problem Destroyer", "Code Artist", "Thinker", "Architect", "Artisan", "Reality Shaper", "Teacher", "Possibility Realiser", "Dervish", "Scientist", "Philosopher", "Creator", "Dream Enabler", "Physicist", "Generalissimo", "Mind Expander", "Entrepreneur", "Cloud Specialist", "Blockchain", ".Com Developer", "Project Illuminary"];
var flip = true;
setInterval(function () { inspire() }, 1000);

function inspire() {
	var randomInt = Math.floor(Math.random() * inspirations.length);
	var text = $("#thought_leader");
	var flop = flip ? "90" : "0";
	if (!flip) {
		text.html(inspirations[randomInt]);
	}
		
	flip = !flip;
	text.css({
		transform: "rotatex" + "(" + flop + "deg)",
		"transition": "all 0.2s ease-out",
	})
	if (!flip) {
		setTimeout(function () {
			text.html(inspirations[randomInt]);
		}, 200);
	}
}