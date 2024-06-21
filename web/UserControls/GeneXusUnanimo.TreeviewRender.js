function GeneXusUnanimo_Treeview($) {
	  
	  
	 this.setTreeviewItems = function(value) {
			this.TreeviewItems = value;
		}

		this.getTreeviewItems = function() {
			return this.TreeviewItems;
		} 
	  
	  
	  

	var template = '<ch-tree id=\"ch-treeview\" toggle-checkboxes=\"{{ToggleCheckboxes}}\">	{{#TreeviewItems}}    <ch-tree-item id=\"{{id}}\" class=\"ch-treeview-item\" {{^SingleSelection}}checkbox{{/SingleSelection}} {{#isOpen}}opened{{/isOpen}} left-icon=\"{{icon}}\">      {{text}}	  {{#hasSubItems}}      <ch-tree {{^SingleSelection}}checkbox{{/SingleSelection}} slot=\"tree\">	  	{{#TreeviewSubItems}}        <ch-tree-item id=\"{{id}}\" class=\"ch-treeview-item\" {{^SingleSelection}}checkbox{{/SingleSelection}} {{#isOpen}}opened{{/isOpen}} left-icon=\"{{icon}}\">          {{text}}		  {{#hasSubItems}}          <ch-tree {{^SingleSelection}}checkbox{{/SingleSelection}} slot=\"tree\">		  	{{#TreeviewSubSubItems}}            <ch-tree-item id=\"{{id}}\" {{^SingleSelection}}checkbox{{/SingleSelection}} class=\"ch-treeview-item\" left-icon=\"{{icon}}\">              {{text}}            </ch-tree-item>			{{/TreeviewSubSubItems}}          </ch-tree>		  {{/hasSubItems}}        </ch-tree-item>		{{/TreeviewSubItems}}      </ch-tree>	  {{/hasSubItems}}    </ch-tree-item>	{{/TreeviewItems}}</ch-tree>';
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
			this.initTree(); 
	}

	this.Scripts = [];

		this.initTree = function() {

					const UC = this;
					const tree = document.getElementById("ch-treeview");
					
					tree.addEventListener("liItemClicked", function(e){
						/*get the item's target*/
						for (let i = 0; i < UC.TreeviewItems.length; i++) {
							const item = UC.TreeviewItems[i];
							if (item.id === e.target.id){	
								console.log("item id: " + item.id);
								UC.SelectedItemId = item.id;
								UC.ItemClick();
							}
							if (item.hasSubItems) {
								for (let j = 0; j < item.TreeviewSubItems.length; j++) {
									const subItem = item.TreeviewSubItems[j];
									if (subItem.id === e.target.id){
										console.log("item id: " + subItem.id);
										UC.SelectedItemId = subItem.id;
										UC.ItemClick();
									}
									if (subItem.hasSubItems) {		
										for (let k = 0; k < subItem.TreeviewSubSubItems.length; k++) {
											const subSubItem = subItem.TreeviewSubSubItems[k];
											if (subSubItem.id === e.target.id){
												console.log("item id: " + subSubItem.id);
												UC.SelectedItemId = subSubItem.id;
												UC.ItemClick();
											}
										}
										
									}
								}
							}
						}
					});
				
					tree.addEventListener("toggleIconClicked", function(e){
					});
				
		}


		this.onItemClickHandler = function (e) {
			if (e) {
				var target = e.currentTarget;
				e.preventDefault();
				 
				 
				 this.TreeviewItemsCurrentIndex = (parseInt($(target).attr('data-items-index'), 10) || 1);  
				 
				 
				 
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