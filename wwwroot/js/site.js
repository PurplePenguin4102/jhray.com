// Write your JavaScript code.
var inspirations = ["Thought Leader", "Genius", "Industrialist", "Solver", "Problem Destroyer", "Code Artist", "Thinker", "Architect", "Artisan", "Reality Shaper", "Teacher", "Possibility Realiser", "Dervish", "Scientist", "Philosopher", "Creator", "Dream Enabler", "Physicist", "Generalissimo", "Mind Expander", "Entrepreneur", "Cloud Specialist", "Blockchain", ".Com Developer", "Project Illuminary"];
setInterval(function () { inspire() }, 1000);
function inspire() {
	var text = document.getElementById("thought_leader");
	var randomInt = Math.floor(Math.random() * inspirations.length);
	text.innerHTML = inspirations[randomInt];
}