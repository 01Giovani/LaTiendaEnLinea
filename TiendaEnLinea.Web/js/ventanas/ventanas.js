/*
###############################################################################

VERSION 1.0

###############################################################################



***** VENTANA CONFIRMAR *****
	
	* Parámetros: (titulo, texto, texto_btn_aceptar, texto_btn_cancelar, callback_aceptar, callback_cancelar, callback_onload)
	
	Ejemplo 1 (simple): 
	ventanas.mostrarConfirmar("Alto!", "Estás seguro que quieres hacer eso?", "Si", "No");
	-------
	
	Ejemplo 2 (con callbacks): 
	ventanas.mostrarConfirmar("Alto!", "Estás seguro que quieres hacer eso?", "Si", "No", function(){
		alert("Presiono ACEPTAR!");
	}), function(){
		alert("Presiono CANCELAR!");
	}, function(){
		alert("Callback que se ejecuta SIEMPRE cuando carga la ventana");
	};	
	-------

***** LOADING *****
	
	Mostrar:
	ventanas.mostrarLoading();
	-------
	
	Ocultar:
	ventanas.ocultarLoading();
	-------
	

***** ALERTA *****
	
	* Parámetros: (titulo, texto, texto_btn_cerrar, tipo, callback_cerrar)
		* En tipo, el numero que se manda cambia el estilo de la ventana:  1-EXITO, 2-ADVERTENCIA, 4-ERROR
	
	Ejemplo 1 (simple):
	ventanas.alerta("Alerta!", "Esto es una alerta.", "Cerrar", tipo); //  1-exito, 2-advertencia, 3-error
	-------
	
	Ejemplo 2 (callback al botón de cerrar):
	ventanas.alerta("Alerta!", "Esto es una alerta.", "Cerrar", tipo, function(){
		alert("Presionó el boton de cerrar!");
	});
	-------


***** ALERTA HTML *****
	
	* Parámetros: (html, clase_personalizada, boton_o_x, callback_cerrar)
		* Enviarle un string de clase personalizada si es necesario
		* El parametro boton_o_x (1 o null si se necesita un botón de cerrar - 2 si se necesita una X de cerrar en la esquina)
	
	Ejemplo 1 (simple):
	ventanas.alertaHtml("<ul><li>Te faltó ingresar tu e-mail.</li><li>Te faltó ingresar tu nombre.</li></ul>", "errores-formulario1", 1, "");
	
	Ejemplo 2 (Callback cerrar):
	ventanas.alertaHtml("<ul><li>Te faltó ingresar tu e-mail.</li><li>Te faltó ingresar tu nombre.</li></ul>", "errores-formulario1", 2, "Aceptar", function(){
		alert("Presionó el boton de cerrar!");
	});

* */

(function (window) {
    window.Bitworks = (typeof (window.Bitworks) === "undefined") ? {} : window.Bitworks;
    
    Bitworks.Ventanas = {
		
		confirmar: function(texto, callback_aceptar, callback_cancelar, texto_btn_aceptar, texto_btn_cancelar){
			
            // Seteo default texto botones aceptar y cancelar
            if ((texto_btn_aceptar != null && texto_btn_aceptar.length == 0) || texto_btn_aceptar == null) {
                texto_btn_aceptar = "Aceptar";
            }
            if ((texto_btn_cancelar != null && texto_btn_cancelar.length == 0) || texto_btn_cancelar == null) {
                texto_btn_cancelar = "Cancelar";
            }
            
            Bitworks.Ventanas.mostrarConfirmar(null, texto, texto_btn_aceptar, texto_btn_cancelar, callback_aceptar, callback_cancelar);
            
		},
		
        mostrarConfirmar: function (titulo, texto, texto_btn_aceptar, texto_btn_cancelar, callback_aceptar, callback_cancelar, callback_onload) {
			
            // Seteo default texto botones aceptar y cancelar
            if ((texto_btn_aceptar != null && texto_btn_aceptar.length == 0) || texto_btn_aceptar == null) {
                texto_btn_aceptar = "Aceptar";
            }
            if ((texto_btn_cancelar != null && texto_btn_cancelar.length == 0) || texto_btn_cancelar == null) {
                texto_btn_cancelar = "Cancelar";
            }

            // Creo elemento ventana en el DOM
            var div_ventana = $('<div class="ventana ventana-confirmar" style="display:none"></div>');

            var html = "";

            // Boton de cerrar (la X en la esquina)
            html += '<a href="#" class="cerrar">Cerrar</a>';

            // Agrego el titulo (solamente si fue especificado en los parametros
            if (titulo && titulo.length > 0 && titulo != null) html += '<h2>' + titulo + '</h2>';

            // Agrego el resto del html de la ventana
            html += '<p>' + texto + '</p><div class="botones"><button class="btn-aceptar">' + texto_btn_aceptar + '</button><button class="btn-cancelar">' + texto_btn_cancelar + '</button></div></div>';

            // Agrego todo el html al div .ventana creado anteriormente
            div_ventana.append(html);

            // Agrego la ventana al body
            $("body").append(div_ventana).promise().done(function () {

                $("body").append('<div class="bg-ventana">&nbsp;</div>');
                div_ventana.fadeIn(144);

                // Seteo funcionamiento boton ACEPTAR (solamente si se envio un callback en los parametros)
                div_ventana.find(".btn-aceptar").click(function () {
                    if (typeof callback_aceptar == "function") callback_aceptar();
                });
                // Seteo funcionamiento boton CANCELAR (si se envio un callback en los parametros... si no, por default cierra la ventana)
                div_ventana.find(".btn-cancelar").click(function () {
                    if (typeof callback_cancelar == "function") callback_cancelar();
                    else cerrar(div_ventana); // Por default cierra la ventana
                });
                // Seteo funcionamiento boton CERRAR (cierra la ventana)
                div_ventana.find(".cerrar").click(function (e) {
                    e.preventDefault(); cerrar(div_ventana);
                });
                if (typeof callback_onload == "function") callback_onload();
            });

            function cerrar(_div_ventana) {
                _div_ventana.stop(true, true).fadeOut(144, function () {
                    _div_ventana.remove();
                    $(".bg-ventana").fadeOut(80).remove();
                });
            }
        }, // end of mostrarConfirmar()
		
        mostrarLoading: function () {

            var htmlLoading = "<div class='spiner-example'>"
                                        + "<div class='sk-spinner sk-spinner-three-bounce'>"
                                            + "<div class='sk-bounce1'></div>"
                                            + "<div class='sk-bounce2'></div>"
                                            + "<div class='sk-bounce3'></div>"
                                        + "</div>"
                                  + "</div>";
            swal({
                title: "Cargando...",
                text: htmlLoading,
                html: true,
                showConfirmButton: false
            });

            //var html = '<div id="ventana_loading" style="display:none"><div>';
            //var loading = "<div class='sk-spinner sk-spinner-wave'>" +
            //       "<div class='sk-rect1'></div>" +
            //       "<div class='sk-rect2'></div>" +
            //       "<div class='sk-rect3'></div>" +
            //       "<div class='sk-rect4'></div>" +
            //       "<div class='sk-rect5'></div>" +
            //   "</div>";
            //var html = '<div id="ventana_loading" style="display:none">' + loading + ' <div>';
            //$("body").append(html).promise().done(function () {
            //    $("#ventana_loading").show();
            //});
        },

        ocultarLoading: function () {
            swal.close();
            //$("#ventana_loading").hide().remove();
        },
		
		alertaExito: function(titulo, texto, texto_btn_cerrar, callback_cerrar){
			if(!titulo) var titulo = null;
			if(!texto) var texto = "Alerta Exito";
			if(!texto_btn_cerrar) var texto_btn_cerrar = null;
			
			if (typeof callback_cerrar == "function") Bitworks.Ventanas.alerta(titulo, texto, texto_btn_cerrar, 1, callback_cerrar);
			else Bitworks.Ventanas.alerta(titulo, texto, texto_btn_cerrar, 1);
		},
		
		alertaAdvertencia: function(titulo, texto, texto_btn_cerrar, callback_cerrar){
			if(!titulo) var titulo = null;
			if(!texto) var texto = "Alerta advertencia";
			if(!texto_btn_cerrar) var texto_btn_cerrar = null;
			
			if (typeof callback_cerrar == "function") Bitworks.Ventanas.alerta(titulo, texto, texto_btn_cerrar, 2, callback_cerrar);
			else Bitworks.Ventanas.alerta(titulo, texto, texto_btn_cerrar, 2);
		},
		
		alertaError: function(titulo, texto, texto_btn_cerrar, callback_cerrar){
			if(!titulo) var titulo = null;
			if(!texto) var texto = "Alerta Error";
			if(!texto_btn_cerrar) var texto_btn_cerrar = null;
			
			if (typeof callback_cerrar == "function") Bitworks.Ventanas.alerta(titulo, texto, texto_btn_cerrar, 3, callback_cerrar);
			else Bitworks.Ventanas.alerta(titulo, texto, texto_btn_cerrar, 3);
		},
		
		mensajeExito: function(texto, fadeOut){
			if(texto.length>0){
				if(!fadeOut) var fadeOut = null;
				Bitworks.Ventanas.mensaje(texto, fadeOut, 1);
			}
		},
		
		mensajeAdvertencia: function(texto, fadeOut){
			if(texto.length>0){
				if(fadeOut==0){} else if( !fadeOut || fadeOut == null || isNaN(fadeOut) || fadeOut<=500 ) var fadeOut = 8000; // 8 segundos por default
				Bitworks.Ventanas.mensaje(texto, fadeOut, 2);
			}
		},
		
		mensajeError: function(texto, fadeOut){
			if(texto.length>0){
				if(fadeOut==0){} else if( !fadeOut || fadeOut == null || isNaN(fadeOut) || fadeOut<=500 ) var fadeOut = 8000; // 8 segundos por default
				Bitworks.Ventanas.mensaje(texto, fadeOut, 3);
			}
		},
		
		mensaje: function(texto, fadeOut, tipo, callback_cerrar){
			if(!tipo || tipo<0 || tipo>3) var tipo = 0;
			if(fadeOut==0){} else if( !fadeOut || fadeOut == null || isNaN(fadeOut) || fadeOut<=500 ) var fadeOut = 8000; // 8 segundos por default
			
			var clase_tipo = "";
            switch (tipo) {
                case 1:
                    clase_tipo = "exito"; break;
                case 2:
                    clase_tipo = "advertencia"; break;
                case 3:
                    clase_tipo = "error"; break;
                default:
                    clase_tipo = ""; break;
            }
            
            var div_ventana = $('<div class="mensaje ' + clase_tipo + '" style="display:none"></div>');
            var html = '<p>' + texto + '</p><span class="cerrar">Cerrar</span>';
            div_ventana.append(html);
            
            var div_contenedor = "";
            if($(".div-contenedor").length > 0){
            	$(".div-contenedor").append(div_ventana).promise().done(function(){
            		ventana_agregada();
            	});
            	
            } else { // no existe el div-contenedor
            	div_contenedor = $('<div class="div-contenedor"></div>');
            	div_contenedor.append(div_ventana);
            	$("body").append(div_contenedor).promise().done(function () {
	               ventana_agregada();
	            });
            }
            
            function ventana_agregada(){
            	div_ventana.fadeIn(144);
            	if(fadeOut>0) desaparecer();
                div_ventana.find(".cerrar").click(function (e) {
                    e.preventDefault();
                    if (typeof callback_cerrar == "function") callback_cerrar();
                    else cerrar(div_ventana);
                });
            }

            function cerrar(_div_ventana) {
                _div_ventana.stop(true, true).fadeOut(144, function () {
                    _div_ventana.remove();
                });
            }
            
            function desaparecer(){
            	div_ventana.delay(fadeOut).fadeOut(700, function(){
            		div_ventana.remove();
            	});
            }
            
		},
		
		listaErrores: function(array_errores, boton_o_x, txt_boton, clase_personalizada){
			if(array_errores.length>0){
				
				if(!clase_personalizada) var clase_personalizada = "";
				if (!boton_o_x || boton_o_x.length == 0 || boton_o_x == null) var boton_o_x = 1;
            	if (!txt_boton || txt_boton.length == 0 || txt_boton == null) var txt_boton = "Cerrar";
            	
            	var html = "";
            	for(v in array_errores){
            		html += "" + array_errores[v] + "";
            	}
            	html += "<";
            	
				if( Object.prototype.toString.call( array_errores ) === '[object Array]' ) {
				    Bitworks.Ventanas.alertaHtml(html, "listaErrores "+clase_personalizada, boton_o_x, txt_boton);
				    
				} else {
					console.log("El primer parámetro de listaErrores debe ser un array.");
				}
			}
		},
		
        //alertaHtml: function (html, clase_personalizada, boton_o_x, txt_boton, callback_cerrar) {
		
        alerta: function (titulo, texto, texto_btn_cerrar, tipo, callback_cerrar) {
            if ((texto_btn_cerrar != null && texto_btn_cerrar.length == 0) || texto_btn_cerrar == null) {
                var texto_btn_cerrar = "Cerrar";
            }

            if (tipo && tipo >= 1 && tipo <= 3) { } else var tipo = 0;
            var clase_tipo = "";

            switch (tipo) {
                case 1:
                    clase_tipo = "exito"; break;
                case 2:
                    clase_tipo = "advertencia"; break;
                case 3:
                    clase_tipo = "error"; break;
                default:
                    clase_tipo = ""; break;
            }

            var div_ventana = $('<div class="ventana ventana-alerta ' + clase_tipo + '" style="display:none"></div>');

            var html = '';
            if (titulo && titulo.length > 0) html += '<h2>' + titulo + '</h2>';
            html += '<p>' + texto + '</p><div class="botones"><button class="cerrar">' + texto_btn_cerrar + '</button></div>';

            div_ventana.append(html);
			
            $("body").append(div_ventana).promise().done(function () {
                $("body").append('<div class="bg-ventana">&nbsp;</div>');
                div_ventana.fadeIn(144);
                div_ventana.find(".cerrar").click(function (e) {
                    e.preventDefault();
                    if (typeof callback_cerrar == "function") callback_cerrar();
                    else cerrar(div_ventana);
                });
            });

            function cerrar(_div_ventana) {
                _div_ventana.stop(true, true).fadeOut(144, function () {
                    _div_ventana.remove();
                    $(".bg-ventana").fadeOut(80).remove();
                });
            }
        }, // end of alerta

        alertaHtml: function (html, clase_personalizada, boton_o_x, txt_boton, callback_cerrar) {
            if (!clase_personalizada) var clase_personalizada = "";
            if (!boton_o_x || boton_o_x.length == 0 || boton_o_x == null) var boton_o_x = 1;
            if (!txt_boton || txt_boton.length == 0 || txt_boton == null) var txt_boton = "Cerrar";

            swal({
                title: "Revise los siguientes campos",
                text: html,
                html: true
                
            });

        } // end of alertaHtml
		
		
    };
})(window);