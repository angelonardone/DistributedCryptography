function GeneXusUnanimo_Button($) {
	  
	  
	  
	  
	  
	  

	var template = '<button class=\"{{Class}} button-icon\" type=\"button\" {{^Enabled}}disabled{{/Enabled}}  data-event=\"Event\" >	<ch-icon src=\"{{Icon}}\" style=\"--icon-size:{{IconSize}}; --icon-color:{{IconColor}}; {{^Enabled}}--icon-color:var(--colors_on-disabled);{{/Enabled}} float:left\">	</ch-icon>	{{Caption}}</button>';
	var partials = {  }; 
	Mustache.parse(template);
	var _iOnEvent = 0; 
	var $container;
	this.show = function() {
			$container = $(this.getContainerControl());

			// Raise before show scripts
			this.Init(); 

			_iOnEvent = 0; 

			//if (this.IsPostBack)
				this.setHtml(Mustache.render(template, this, partials));
			this.renderChildContainers();

			$(this.getContainerControl())
				.find("[data-event='Event']")
				.on('click', this.onEventHandler.closure(this))
				.each(function (i) {
					this.setAttribute("data-items-index", i + 1);
				}); 

			// Raise after show scripts

	}

	this.Scripts = [];

		this.Init = function() {

			  	const UC = this;
				switch(UC.Variant) {
					case "Text + icon":
					if (UC.Class.includes("icon-only")) {
						UC.Class = UC.Class.replace(' icon-only', '');
					}
					break;
					case "Icon only":
					if (!UC.Class.includes("icon-only")) {
						UC.Class += " icon-only";
					}
					break;
				}
				if (UC.Style === "Rounded") {
					if (!UC.Class.includes("Rounded")) {
						UC.Class += " Rounded";
					}
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