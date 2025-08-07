function Wallet_UC_Clipboard_v1($) {
	  

	var template = '<Script></Script>';
	var partials = {  }; 
	Mustache.parse(template);
	var _iOnAfterGetValueData = 0; 
	var $container;
	this.show = function() {
			$container = $(this.getContainerControl());

			// Raise before show scripts

			_iOnAfterGetValueData = 0; 

			//if (this.IsPostBack)
				this.setHtml(Mustache.render(template, this, partials));
			this.renderChildContainers();

			$(this.getContainerControl())
				.find("[data-event='AfterGetValueData']")
				.on('aftergetvaluedata', this.onAfterGetValueDataHandler.closure(this))
				.each(function (i) {
					this.setAttribute("data-items-index", i + 1);
				}); 

			// Raise after show scripts

	}

	this.Scripts = [];

		this.setText = function(textData ) {

				if (!navigator.clipboard) {
					console.log('This browser does not support clipboard.'); 
				} else {	
					navigator.clipboard.writeText(textData).then(
					() => {
						//console.log('clipboard set ok'); 
					},
					() => {
						//console.log('clipboard set ng'); 
					});
				}

		}
		this.getText = function() {

			 	const UC = this;
				this.ValueData ="";
				if (!navigator.clipboard) {
					console.log('This browser does not support clipboard.'); 
				} else {	
					getClipboardText().then(pastedText => {
					this.ValueData = pastedText;
					// Here you can use asynchronously retrieved text.
					// ここで非同期的に取得したテキストを使える
					if (UC.AfterGetValueData){
						UC.AfterGetValueData()
					}
					console.log(pastedText);
					});
				}

				async function getClipboardText() {
					try {
						const text = await navigator.clipboard.readText();
						//console.log("pastedText:", text);
						return text;
					} catch (err) {
						//console.error('error:', err);
					}
				}

		}


		this.onAfterGetValueDataHandler = function (e) {
			if (e) {
				var target = e.currentTarget;
				e.preventDefault();
				 
			}

			if (this.AfterGetValueData) {
				this.AfterGetValueData();
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