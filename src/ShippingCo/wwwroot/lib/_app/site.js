$(function(){var n=function(){var n=$("#nav-arrows"),i=$("#slider").slitslider({autoplay:!0}),r=function(){t()},t=function(){n.children(":last").on("click",function(){return i.next(),!1}),n.children(":first").on("click",function(){return i.previous(),!1})};return{init:r}}();n.init()});