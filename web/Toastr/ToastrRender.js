function Toastr($)
{
	this.CloseButton;
	this.ProgressBar;
	this.PreventDuplicate;
	this.NewestOnTop;
	this.ShowEasing;
	this.HideEasing;
	this.ShowMethod;
	this.HideMethod;
	this.ShowDuration;
	this.HideDuration;
	this.TimeOut;
	this.ExtendedTimeOut;
	this.Position;

	this.show = function()
	{
		///UserCodeRegionStart:[show] (do not remove this comment.)
		
		toastr.options = {
			  "closeButton": this.CloseButton,
			  "progressBar": this.ProgressBar,
			  "preventDuplicates": this.PreventDuplicate,
			  "newestOnTop": this.NewestOnTop,
			  "positionClass": this.Position,
			  "debug": false,
			  "onclick": null,
			  "showDuration": this.ShowDuration,
			  "hideDuration": this.HideDuration,
			  "timeOut": this.TimeOut,
			  "extendedTimeOut": this.ExtendedTimeOut,
			  "hideEasing": this.HideEasing,
			  "showMethod": this.ShowMethod,
			  "hideMethod": this.HideMethod
			}
		
		
		
		
		
		
		
		
		
		
		
		///UserCodeRegionEnd: (do not remove this comment.)
	}
	///UserCodeRegionStart:[User Functions] (do not remove this comment.)
	this.Msg = function(toast_type, message, title){
		
		toastr[toast_type](message, title);
	}
	
	
	
	
	
	
	
	
	
	
	
	
	
	///UserCodeRegionEnd: (do not remove this comment.):
}
