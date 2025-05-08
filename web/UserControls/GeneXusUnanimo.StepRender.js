function GeneXusUnanimo_Step($) {
	  
	  
	  
	 this.setStepItems = function(value) {
			this.StepItems = value;
		}

		this.getStepItems = function() {
			return this.StepItems;
		} 
	  

	var template = '<ch-step-list id=\"ch-step{{id}}\">	{{#StepItems}}	<ch-step-list-item class=\"{{Class}}\" >		{{text}}	</ch-step-list-item>	{{/StepItems}}</ch-step-list>';
	var partials = {  }; 
	Mustache.parse(template);
	var _iOnItemClick = 0; 
	var $container;
	this.show = function() {
			$container = $(this.getContainerControl());

			// Raise before show scripts

			_iOnItemClick = 0; 

			//if (this.IsPostBack)
				this.setHtml(Mustache.render(template, this, partials));
			this.renderChildContainers();

			$(this.getContainerControl())
				.find("[data-event='ItemClick']")
				.on('itemclick', this.onItemClickHandler.closure(this))
				.each(function (i) {
					this.setAttribute("data-items-index", i + 1);
				}); 

			// Raise after show scripts
			this.initItemClick(); 
	}

	this.Scripts = [];

		this.initItemClick = function() {

					const UC = this;
					const elId = "ch-step" + UC.id;
					const el = document.getElementById(elId);
					el.addEventListener("itemClicked", function(e){
						/*get the selected item*/
						for (let i = 0; i < UC.StepItems.length; i++) {
							const item = UC.StepItems[i];
							if (item.text === e.detail["item-clicked"]){
								UC.SelectedItem = item.text;
								if (UC.ItemClick){
									UC.ItemClick();
								}
								break;
							}
						}
					});
				
		}


		this.onItemClickHandler = function (e) {
			if (e) {
				var target = e.currentTarget;
				e.preventDefault();
				 
				 
				 
				 this.StepItemsCurrentIndex = (parseInt($(target).attr('data-items-index'), 10) || 1);  
				 
			}

			if (this.ItemClick) {
				this.ItemClick();
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