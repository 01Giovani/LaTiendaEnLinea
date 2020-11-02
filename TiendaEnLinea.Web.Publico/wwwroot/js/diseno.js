$(function(){
   	
	$(".menu-toggle").click(function(e){
		e.preventDefault();
		$("body").toggleClass("open_menu");
	});
	
	if($(".slider-ppal .slider").length){
		$(".slider-ppal .slider").slick({
			arrows: false,
			dots: true,
			appendDots: $(".slider-ppal .dots").first(),
			autoplay: true,
			autoplaySpeed: 3000,
			fade: true
		});
	}

	if($(".slider-detalle .slider").length){
		$(".slider-detalle .slider").slick({
			arrows: false,
			dots: false,
			autoplay: false,
			fade: true,
		});
		$(".thumbs-detalle ul li:eq(0)").addClass("active");
		$(".slider-detalle .slider").on('beforeChange', function(event, slick, currentSlide, nextSlide){
			$(".thumbs-detalle ul li").removeClass("active");
			$(".thumbs-detalle ul li:eq("+nextSlide+")").addClass("active");
		});
		$(".thumbs-detalle ul li").click(function(e){
			e.preventDefault();
			var index = $(this).index();
			$(".slider-detalle .slider").slick('slickGoTo',index);
		});
	}
	
	if($(".slider-promos .slider").length){
		
		$(".slider-promos .slider").slick({
			autoplay: true,
			speed: 2000,
			autoplaySpeed: 5000,
			slidesToScroll: 4,
			slidesToShow: 4,
			infinite: true,
			prevArrow: $("#prevSldrPromos"),
			nextArrow: $("#nextSldrPromos"),
			responsive: [
				{
				  	breakpoint: 992,
				  	settings: {
						slidesToShow: 3,
					  	slidesToScroll: 3,
				  	}
				},
				{
				  	breakpoint: 768,
				  	settings: {
						slidesToShow: 2,
					  	slidesToScroll: 2,
				  	}
				},
			],
		});
	}
	
	if($(".slider-marcas .slider").length){
		
		$(".slider-marcas .slider").slick({
			autoplay: true,
			speed: 2000,
			autoplaySpeed: 500,
			slidesToScroll: 6,
			slidesToShow: 6,
			infinite: true,
			prevArrow: $("#prevSldrMarcas"),
			nextArrow: $("#nextSldrMarcas"),
			responsive: [
				{
				  	breakpoint: 992,
				  	settings: {
						slidesToShow: 4,
					  	slidesToScroll: 4,
				  	}
				},
				{
				  	breakpoint: 768,
				  	settings: {
						slidesToShow: 2,
					  	slidesToScroll: 2,
				  	}
				},
			],
		});
	}
	
	if($(".slider-novedades .slider").length){
		
		$(".slider-novedades .slider").slick({
			autoplay: false,
			speed: 1500,
			autoplaySpeed: 4000,
			slidesToScroll: 3,
			slidesToShow: 3,
			infinite: true,
			prevArrow: $("#prevSldrNovedades"),
			nextArrow: $("#nextSldrNovedades"),
			responsive: [
				{
				  	breakpoint: 992,
				  	settings: {
						slidesToShow: 2,
					  	slidesToScroll: 2,
				  	}
				},
			],
		});
	}

	/* $(".open-list-categorias").click(function(e){
		e.preventDefault();
		if($("body").is(".open_categories")){
			closeCategoriasMenu();
		} else{
			$("body").addClass("open_categories");
		}
		
	});

	$(document).on("click touchend", "#categorias li.has-sm > a", function (e) {
		e.preventDefault();
		var theLink = $(this);
        if(e.type=='click'){
			window.location.href = theLink.attr("href");
			console.log("click url");
		} else{
			catLinkClick(theLink);
			console.log("click panel");
		}
	});

	$(document).on("mouseover", "#categorias li.has-sm > a", function (e) {
		e.preventDefault();
		var theLink = $(this);
		catLinkClick(theLink);
		console.log("hover");
	});

	$(document).on("mouseleave", "#categorias li.has-sm.active", function (e) {
		e.preventDefault();
		$(this).removeClass("active");
	});

	$("#categorias li.go-back > a").click(function(e){
		e.preventDefault();
		var parentLI = $(this).parent("li");
		var ulToClose = parentLI.parent("ul");
		if(ulToClose.is(".l1")){
			closeCategoriasMenu();
		} else{
			ulToClose.parent("li").removeClass("active");
		}
	});

	function catLinkClick(theLink){
		var parentLI = theLink.parent("li");
		if(parentLI.is(".active")){
			parentLI.removeClass("active");
			return false;
		}

		var ulToOpen = parentLI.find("ul").first();
		if(ulToOpen.is(".l3")){
			$("#categorias ul.l3 li.has-sm").removeClass("active");
		} else{
			$("#categorias li.has-sm").removeClass("active");
		}

		parentLI.addClass("active");
	}

	$("#categorias ul").outclick(function(){
		closeCategoriasMenu();
	}); */




	$(".toggle-categorias").click(function(e){
		e.preventDefault();
		$("body").removeClass("open_menu");
		$("body").toggleClass("open_categorias");
	});
	
	$("#categorias").on("click","a.back.l2,a.back.l3",function(e){
		e.preventDefault();
		var theLink = $(this);
		var level = 2;
		if(theLink.hasClass("l3")){
			level = 3;
		}
		closePanelCategorias(level);
	});
	
	$("#categorias").on("click","a.open-panel",function(e){
		e.preventDefault();
		var cats = $("#categorias");
		var theLink = $(this);
		var theMenu = theLink.next("ul.menu");
		var level = 2;
		if(!theMenu.length) return false;
		
		if(theMenu.hasClass("l3")){
			level = 3;
			cats.find(".panel.level-3 .menu-wrap").html("");
		} else{
			cats.find(".panel.level-2 .menu-wrap, .panel.level-3 .menu-wrap").html("");
		}
		
		console.log(theLink);
		if(theLink.hasClass("active")){
			closePanelCategorias(level);
			return false;
		}
		
		if(level>2){
			cats.find(".panel.level-2 a.open-panel").removeClass("active");
		} else{
			cats.find(".panel a.open-panel").removeClass("active");
		}
		
		var titleContainer = cats.find(".panel.level-"+level+" a.back span").first();
		titleContainer.html(theLink.html());
		var menuContainer = cats.find(".panel.level-"+level+" .menu-wrap").first();
		theMenu.clone().appendTo(menuContainer);
		cats.removeClass("l2 l3");
		cats.addClass("l"+level);
		theLink.addClass("active");
		//console.log(container);
	});

});

function closePanelCategorias(level){
	var cats = $("#categorias");
	var newLevel = level-1;
	console.log(newLevel);
	if(level>2){
		cats.find(".panel.level-3 .menu-wrap").html("");
		cats.find(".panel.level-2 a.open-panel").removeClass("active");
	}else{
		cats.find(".panel.level-2 .menu-wrap, .panel.level-3 .menu-wrap").html("");
		cats.find(".panel a.open-panel").removeClass("active");
	}
	
	cats.removeClass("l2 l3");
	cats.addClass("l"+newLevel);
}

function closeCategoriasMenu(){
	$("#categorias li.has-sm").removeClass("active");
	$("body").removeClass("open_categories");
}