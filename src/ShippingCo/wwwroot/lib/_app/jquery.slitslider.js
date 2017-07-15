!function(t,i,s){"use strict";var e,n,a=t.event;e=a.special.debouncedresize={setup:function(){t(this).on("resize",e.handler)},teardown:function(){t(this).off("resize",e.handler)},handler:function(t,i){var s=this,o=arguments,l=function(){t.type="debouncedresize",a.dispatch.apply(s,o)};n&&clearTimeout(n),i?l():n=setTimeout(l,e.threshold)},threshold:20};var o=t(i),l=t(document),r=i.Modernizr;t.Slitslider=function(i,s){this.$elWrapper=t(s),this._init(i)},t.Slitslider.defaults={speed:800,optOpacity:!1,translateFactor:230,maxAngle:25,maxScale:2,autoplay:!1,keyboard:!0,interval:4e3,onBeforeChange:function(t,i){return!1},onAfterChange:function(t,i){return!1}},t.Slitslider.prototype={_init:function(i){this.options=t.extend(!0,{},t.Slitslider.defaults,i),this.transEndEventNames={WebkitTransition:"webkitTransitionEnd",MozTransition:"transitionend",OTransition:"oTransitionEnd",msTransition:"MSTransitionEnd",transition:"transitionend"},this.transEndEventName=this.transEndEventNames[r.prefixed("transition")],this.support=r.csstransitions&&r.csstransforms3d,this.$el=this.$elWrapper.children(".sl-slider"),this.$slides=this.$el.children(".sl-slide").hide(),this.slidesCount=this.$slides.length,this.current=0,this.isAnimating=!1,this._getSize(),this._layout(),this._loadEvents(),this.options.autoplay&&this._startSlideshow()},_getSize:function(){this.size={width:this.$elWrapper.outerWidth(!0),height:this.$elWrapper.outerHeight(!0)}},_layout:function(){this.$slideWrapper=t('<div class="sl-slides-wrapper" />'),this.$slides.wrapAll(this.$slideWrapper).each(function(i){var s=t(this),e=s.data("orientation");s.addClass("sl-slide-"+e).children().wrapAll('<div class="sl-content-wrapper" />').wrapAll('<div class="sl-content" />')}),this._setSize(),this.$slides.eq(this.current).show()},_navigate:function(i,e){if(this.isAnimating||this.slidesCount<2)return!1;this.isAnimating=!0;var n=this,a=this.$slides.eq(this.current);e!==s?this.current=e:"next"===i?this.current=this.current<this.slidesCount-1?++this.current:0:"prev"===i&&(this.current=this.current>0?--this.current:this.slidesCount-1),this.options.onBeforeChange(a,this.current);var o=this.$slides.eq(this.current),l="next"===i?a:o,r=l.data(),h={};h.orientation=r.orientation||"horizontal",h.slice1angle=r.slice1Rotation||0,h.slice1scale=r.slice1Scale||1,h.slice2angle=r.slice2Rotation||0,h.slice2scale=r.slice2Scale||1,this._validateValues(h);var c="horizontal"===h.orientation?{marginTop:-this.size.height/2}:{marginLeft:-this.size.width/2},d={transform:"translate(0%,0%) rotate(0deg) scale(1)",opacity:1},p="horizontal"===h.orientation?{transform:"translateY(-"+this.options.translateFactor+"%) rotate("+h.slice1angle+"deg) scale("+h.slice1scale+")"}:{transform:"translateX(-"+this.options.translateFactor+"%) rotate("+h.slice1angle+"deg) scale("+h.slice1scale+")"},u="horizontal"===h.orientation?{transform:"translateY("+this.options.translateFactor+"%) rotate("+h.slice2angle+"deg) scale("+h.slice2scale+")"}:{transform:"translateX("+this.options.translateFactor+"%) rotate("+h.slice2angle+"deg) scale("+h.slice2scale+")"};this.options.optOpacity&&(p.opacity=0,u.opacity=0),a.removeClass("sl-trans-elems");var f={transition:"all "+this.options.speed+"ms ease-in-out"};l.css("z-index",this.slidesCount).find("div.sl-content-wrapper").wrap(t('<div class="sl-content-slice" />').css(f)).parent().cond("prev"===i,function(){var t=this;this.css(p),setTimeout(function(){t.css(d)},50)},function(){var t=this;setTimeout(function(){t.css(p)},50)}).clone().appendTo(l).cond("prev"===i,function(){var t=this;this.css(u),setTimeout(function(){a.addClass("sl-trans-back-elems"),n.support?t.css(d).on(n.transEndEventName,function(){n._onEndNavigate(t,a,i)}):n._onEndNavigate(t,a,i)},50)},function(){var t=this;setTimeout(function(){o.addClass("sl-trans-elems"),n.support?t.css(u).on(n.transEndEventName,function(){n._onEndNavigate(t,a,i)}):n._onEndNavigate(t,a,i)},50)}).find("div.sl-content-wrapper").css(c),o.show()},_validateValues:function(t){(t.slice1angle>this.options.maxAngle||t.slice1angle<-this.options.maxAngle)&&(t.slice1angle=this.options.maxAngle),(t.slice2angle>this.options.maxAngle||t.slice2angle<-this.options.maxAngle)&&(t.slice2angle=this.options.maxAngle),(t.slice1scale>this.options.maxScale||t.slice1scale<=0)&&(t.slice1scale=this.options.maxScale),(t.slice2scale>this.options.maxScale||t.slice2scale<=0)&&(t.slice2scale=this.options.maxScale),"vertical"!==t.orientation&&"horizontal"!==t.orientation&&(t.orientation="horizontal")},_onEndNavigate:function(t,i,s){var e=t.parent(),n="sl-trans-elems sl-trans-back-elems";t.remove(),e.css("z-index",1).find("div.sl-content-wrapper").unwrap(),i.hide().removeClass(n),e.removeClass(n),this.isAnimating=!1,this.options.onAfterChange(e,this.current)},_setSize:function(){var t={width:this.size.width,height:this.size.height};this.$el.css(t).find("div.sl-content-wrapper").css(t)},_loadEvents:function(){var t=this;o.on("debouncedresize.slitslider",function(i){t._getSize(),t._setSize()}),this.options.keyboard&&l.on("keydown.slitslider",function(i){var s=i.keyCode||i.which,e={left:37,up:38,right:39,down:40};switch(s){case e.left:t._stopSlideshow(),t._navigate("prev");break;case e.right:t._stopSlideshow(),t._navigate("next")}})},_startSlideshow:function(){var t=this;this.slideshow=setTimeout(function(){t._navigate("next"),t.options.autoplay&&t._startSlideshow()},this.options.interval)},_stopSlideshow:function(){this.options.autoplay&&(clearTimeout(this.slideshow),this.isPlaying=!1,this.options.autoplay=!1)},_destroy:function(i){this.$el.off(".slitslider").removeData("slitslider"),o.off(".slitslider"),l.off(".slitslider"),this.$slides.each(function(i){var s=t(this),e=s.find("div.sl-content").children();e.appendTo(s),s.children("div.sl-content-wrapper").remove()}),this.$slides.unwrap(this.$slideWrapper).hide(),this.$slides.eq(0).show(),i&&i.call()},add:function(i,s){this.$slides=this.$slides.add(i);var e=this;i.each(function(i){var s=t(this),n=s.data("orientation");s.hide().addClass("sl-slide-"+n).children().wrapAll('<div class="sl-content-wrapper" />').wrapAll('<div class="sl-content" />').end().appendTo(e.$el.find("div.sl-slides-wrapper"))}),this._setSize(),this.slidesCount=this.$slides.length,s&&s.call($items)},next:function(){this._stopSlideshow(),this._navigate("next")},previous:function(){this._stopSlideshow(),this._navigate("prev")},jump:function(t){return t-=1,!(t===this.current||t>=this.slidesCount||t<0)&&(this._stopSlideshow(),void this._navigate(t>this.current?"next":"prev",t))},play:function(){this.isPlaying||(this.isPlaying=!0,this._navigate("next"),this.options.autoplay=!0,this._startSlideshow())},pause:function(){this.isPlaying&&this._stopSlideshow()},isActive:function(){return this.isAnimating},destroy:function(t){this._destroy(t)}};var h=function(t){i.console&&i.console.error(t)};t.fn.slitslider=function(i){var s=t.data(this,"slitslider");if("string"==typeof i){var e=Array.prototype.slice.call(arguments,1);this.each(function(){return s?t.isFunction(s[i])&&"_"!==i.charAt(0)?void s[i].apply(s,e):void h("no such method '"+i+"' for slitslider self"):void h("cannot call methods on slitslider prior to initialization; attempted to call method '"+i+"'")})}else this.each(function(){s?s._init():s=t.data(this,"slitslider",new t.Slitslider(i,this))});return s}}(jQuery,window);