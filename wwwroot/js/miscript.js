$(document).ready(function () {
    $("#p1").click(function () {
        $("#sp1").text("hola, " + $("#p1").text());
    });
    $("#p2").click(function () {
        $("#sp1").text("hola, " + $("#p2").text());
    });
    $("#p3").click(function () {
        $("#sp1").text("hola, " + $("#p3").text());
    });

    $("#boton").click(function () {
        $("#tabla").append("<tr><td>" + $("#texto").val() + "</td></tr>");
        $("#texto").val("");
        $("#texto").focus();
    });

    $("#p11").mouseenter(function () {
        //$("#p11").addClass("text-primary");
        $("#p11").css("color", "blue")
    });

    $("#p11").mouseleave(function () {
        $("#p11").removeClass("text-primary");
    });
});
