function ajaxLoader(el, options) {
    // Becomes this.options
    var defaults = {
        //bgColor: '#fff',
        duration: 800,
        opacity: 0.7,
        classOveride: false
    }
    this.options = jQuery.extend(defaults, options);
    //this.container = $(el);
    this.container = $('body');


    this.init = function () {
        var container = this.container;
        // Delete any other loaders
        this.remove();
        // Create the overlay 
        var overlay = $('<div></div>').css({
            //'background-color': this.options.bgColor,
            'opacity': this.options.opacity,
            'width': container.width(),
            //'height':container.height(),
            'height': '100%',
            'position': 'fixed',
            'top': '0px',
            'left': '0px',
            //'right':'0px',
            //'bottom':'0px',
            'z-index': 99999
        }).addClass('ajax_overlay');
        // add an overiding class name to set new loader style 
        if (this.options.classOveride) {
            overlay.addClass(this.options.classOveride);
        }
        // insert overlay and loader into DOM 
        //container.append(
		//	overlay.append(
		//		$('<div></div>').addClass('ajax_loader').css({
		//		    //'content': 'url(../../Content/images/other_img/Spinner_envi.gif)',
		//		    'content': 'url(../../Content/images/other_img/ajax-loader_blue.gif)',
		//		    'top': '50%',
		//		    'left': '50%',
		//		    'margin-top': '-50px',
		//		    'margin-left': '-50px',
		//		    //'width': '50px',
		//		    //'height': '50px',
		//		    'position': 'absolute'
		//		})
		//	).fadeIn(this.options.duration)
        //);

        container.append(
			overlay.append(
				$('<div></div>').addClass('ajax_loader')
			).fadeIn(this.options.duration)
		);
    };

    this.remove = function () {
        var overlay = this.container.children(".ajax_overlay");
        if (overlay.length) {
            overlay.fadeOut(this.options.classOveride, function () {
                overlay.remove();
            });
        }
    }

    this.init();
}