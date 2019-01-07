// Write your JavaScript code.
var inspirations = ["Thought Leader", "Awesomenaut", "Man of Athens", "1/0 Limit Endpoint", "Galaxy Brain", "Freelance Reimaginer", ".com Aficionado", "Fearless", "Genius", "Latte Critic", "Imagination Darer", "Thinkgineer", "Industrialist", "Solver", "Problem Destroyer", "Code Artist", "Thinker", "Architect", "Artisan", "Reality Shaper", "Teacher", "Possibility Realiser", "Divine Yogi", "Celestial Savant", "Dervish", "Scientist", "Philosopher", "Creator", "Dream Enabler", "Physicist", "Generalissimo", "Mind Expander", "Entrepreneur", "Cloud Specialist", "Kubernetes Kreator", "NoSql Relational Manager", "Blockchain", "Proud Roman", "Project Illuminary"];
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
        "transition": "all 0.2s ease-out"
    });
	if (!flip) {
		setTimeout(function () {
			text.html(inspirations[randomInt]);
		}, 200);
	}
}

function expando(endpoint, id) {
    var website = "/home/podcast/" + id;
    $.getJSON(website, function (data) {
        $("#lightbox_bkg").show();
        $("#gem_full_title").text(data["Title"]);
        $("#gem_full_contents").text(data["Text"]);
        $("#gem_full_audio_src").attr("src", data["AudioLink"]);
        var audio = $("#gemAudioBig");  
        audio[0].pause();
        audio[0].load();
    });
}

$(function () {
    $("#lightbox_bkg").click(function () {
        $("#lightbox_bkg").hide();
    });
});

function PopulateViewArea() {
    var area = $("#view_area");
    area.empty();

    var title = $("#blog_title");
    var subtitle = $("#blog_subtitle");
    if (subtitle.val() && title.val()) {
        area.append("<h2>" + title.val() + " - " + subtitle.val() + "</h2>");
    } else if (title.val()) {
        area.append("<h2>" + title.val() + "</h2>");
    }
    var auth = $("#blog_author");
    if (auth.val()) {
        area.append("<h3><em>" + auth.val() + "</em></h3>");
    }

    var content = $("#blog_markdown_content");
    $.post("/GemMaster/ConvertMarkdownToHtml", { markdown: content.val() }, function (data) {
        area.append(data["converted"]);
    });
    var hashtags = $("#blog_hashtags");
}