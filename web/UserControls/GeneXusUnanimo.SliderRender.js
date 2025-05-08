function GeneXusUnanimo_Slider($) {
	  
	  
	  
	  
	  

	var template = '<div class=\"slider-container\">    <input type=\"range\" min=\"{{MinValue}}\" max=\"{{MaxValue}}\" step=\"{{Step}}\" class=\"slider\" tabindex=\"0\" {{^Enabled}}disabled{{/Enabled}} data-gx-binding=\"value\">	<output class=\"slider-value\"></div> ';
	var partials = {  }; 
	Mustache.parse(template);
	var _iOnEvent = 0; 
	var _iOnControlValueChanged = 0; 
	var $container;
	var valueObject = {};

	this.setAttribute = function (v) {
		valueObject.value = Number(v);
	}
	this.getAttribute = function () {
		var v = valueObject.value;
		return v;
	}

	this.show = function() {
			$container = $(this.getContainerControl());

			// Raise before show scripts

			_iOnEvent = 0; 
			_iOnControlValueChanged = 0; 

			//if (this.IsPostBack)
				this.setHtml(Mustache.render(template, this, partials));
			this.renderChildContainers();

			var $dataElement = $container.find("[data-gx-binding]");
			var dataElementProp = $dataElement.attr("data-gx-binding") || "value";
			$dataElement.on("input", function () {
				valueObject.value = Number(this[dataElementProp]);
			});
			$dataElement.on("change", function () {
				valueObject.value = Number(this[dataElementProp]);
			});
			$dataElement.on("focus", this.onfocus.closure(this));
			$dataElement.on("input", this.oninput.closure(this));
			$dataElement.on("change", this.onchange.closure(this));

			$dataElement.prop(dataElementProp, valueObject.value);

			$(this.getContainerControl())
				.find("[data-event='Event']")
				.on('click', this.onEventHandler.closure(this))
				.each(function (i) {
					this.setAttribute("data-items-index", i + 1);
				}); 
			$(this.getContainerControl())
				.find("[data-event='ControlValueChanged']")
				.on('controlvaluechanged', this.onControlValueChangedHandler.closure(this))
				.each(function (i) {
					this.setAttribute("data-items-index", i + 1);
				}); 

			// Raise after show scripts
			this.InitSlider(); 
	}

	this.Scripts = [];

		this.InitSlider = function() {

			  	const UC = this;
				
				var containerEl = document.getElementById(UC.ContainerName);
				var el = containerEl.getElementsByTagName("input")[0];
				var output = containerEl.getElementsByTagName("output")[0];
				containerEl = containerEl.firstChild;
				containerEl.setAttribute("id", "sliderContainer" + UC.ControlId);
				el.setAttribute("id", "slider" + UC.ControlId);
				output.setAttribute("for", "slider" + UC.ControlId);
				const range = el;
				const bubble = output;
				
				setBubble(range, bubble);
				
				range.addEventListener("input", function() {
					setBubble(range, bubble);
					if (UC.ControlValueChanged){
						UC.ControlValueChanged()
					}
				});
				
				range.addEventListener("keydown", function(ev){
					switch (ev.keyCode) {
						case 37:
						case 40:
							range.value - UC.Step;
							break;
						case 38:
						case 39:
							range.value + UC.Step;
							break;
					}
				})
				
				function setBubble(range, bubble) {
					const val = range.value;
					const min = range.min ? range.min : 0;
					const max = range.max ? range.max : 100;
					const newVal = Number(((val - min) * 100) / (max - min));
					bubble.innerHTML = val;
					bubble.style.left = `calc(${newVal}% + (${8 - newVal * 0.15}px))`;
				}

		}


		this.onEventHandler = function (e) {
			if (e) {
				var target = e.currentTarget;
				e.preventDefault();
				 
				 
				 
				 
				 
			}

			if (this.Event) {
				this.Event();
			}
		} 

		this.onControlValueChangedHandler = function (e) {
			if (e) {
				var target = e.currentTarget;
				e.preventDefault();
				 
				 
				 
				 
				 
			}

			if (this.ControlValueChanged) {
				this.ControlValueChanged();
			}
		} 

	this.autoToggleVisibility = true;

	var childContainers = {};
	this.renderChildContainers = function () {
		$container
			.find("[data-slot][data-parent='" + this.ContainerName + "']")
			.each((function (i, slot) {
				var $slot = $(slot),
					slotName = $slot.attr('data-slot'),
					slotContentEl;

				slotContentEl = childContainers[slotName];
				if (!slotContentEl) {				
					slotContentEl = this.getChildContainer(slotName)
					childContainers[slotName] = slotContentEl;
					slotContentEl.parentNode.removeChild(slotContentEl);
				}
				$slot.append(slotContentEl);
				$(slotContentEl).show();
			}).closure(this));
	};

}