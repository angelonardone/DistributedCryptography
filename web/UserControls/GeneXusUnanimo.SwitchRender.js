function GeneXusUnanimo_Switch($) {
	  
	  

	var template = '<div class=\"switch-container\" tabindex=\"0\">    <input type=\"checkbox\" role=\"switch\" data-gx-binding=\"checked\" {{^Enabled}}disabled{{/Enabled}} />    <label></label></div>';
	var partials = {  }; 
	Mustache.parse(template);
	var _iOnControlValueChanged = 0; 
	var $container;
	var valueObject = {};

	this.setAttribute = function (v) {
		valueObject.value = gx.lang.gxBoolean(v);
	}
	this.getAttribute = function () {
		var v = valueObject.value;
		return v;
	}

	this.show = function() {
			$container = $(this.getContainerControl());

			// Raise before show scripts

			_iOnControlValueChanged = 0; 

			//if (this.IsPostBack)
				this.setHtml(Mustache.render(template, this, partials));
			this.renderChildContainers();

			var $dataElement = $container.find("[data-gx-binding]");
			var dataElementProp = $dataElement.attr("data-gx-binding") || "value";
			$dataElement.on("input", function () {
				valueObject.value = gx.lang.gxBoolean(this[dataElementProp]);
			});
			$dataElement.on("change", function () {
				valueObject.value = gx.lang.gxBoolean(this[dataElementProp]);
			});
			$dataElement.on("focus", this.onfocus.closure(this));
			$dataElement.on("input", this.oninput.closure(this));
			$dataElement.on("change", this.onchange.closure(this));

			$dataElement.prop(dataElementProp, valueObject.value);

			$(this.getContainerControl())
				.find("[data-event='ControlValueChanged']")
				.on('controlvaluechanged', this.onControlValueChangedHandler.closure(this))
				.each(function (i) {
					this.setAttribute("data-items-index", i + 1);
				}); 

			// Raise after show scripts
			this.init(); 
	}

	this.Scripts = [];

		this.init = function() {

			  	const UC = this;

				const containerEl = document.getElementById(UC.ContainerName);
				var el = containerEl.getElementsByTagName("input")[0];
				var label = containerEl.getElementsByTagName("label")[0];
				el.setAttribute("id", "switch" + UC.ControlId);
				el.setAttribute("aria-checked", el.checked)
				label.setAttribute("for", "switch" + UC.ControlId);
				label.innerHTML = "switch" + UC.ControlId;
				
				const controlValueChanged = function(){
					if (UC.ControlValueChanged){
						el.setAttribute("aria-checked", UC.getAttribute())
						UC.ControlValueChanged()
					}
				}
				
				el.addEventListener("change", controlValueChanged);
				containerEl.addEventListener("keydown", function(ev){
					if (ev.code == "Space" || ev.code == "Enter") {
						el.checked = !el.checked
						UC.setAttribute(el.checked)
						controlValueChanged();
					}
				})
			  
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