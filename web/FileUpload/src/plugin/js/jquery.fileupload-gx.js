
(function (factory) {
    'use strict';
    if (typeof define === 'function' && define.amd) {
        // Register as an anonymous AMD module:
        define([
            'jquery',
			'./jquery.fileupload-ui'
        ], factory);
    } else if (typeof exports === 'object') {
        // Node/CommonJS:
        factory(
            require('jquery')
        );
    } else {
        // Browser globals:
        factory(window.jQuery);
    }
}(function ($) {
    'use strict';

    // The GX version customizes the file upload UI widget
    $.widget('blueimp.fileupload', $.blueimp.fileupload, {
		_initSpecialOptions: function () {
            this._super();

            var options = this.options;
			options.dropZone = this.element.find('.dropzone');
		},
		
		_defineDropZone: function () {
			$(document).bind('dragover', function (e) {
			var dropZones = $('.dropzone'),
				timeout = window.dropZoneTimeout;
			if (timeout) {
				clearTimeout(timeout);
			} else {
				dropZones.addClass('in');
			}
			var hoveredDropZone = $(e.target).closest(dropZones);
			dropZones.not(hoveredDropZone).removeClass('hover');
			hoveredDropZone.addClass('hover');
			window.dropZoneTimeout = setTimeout(function () {
					window.dropZoneTimeout = null;
					dropZones.removeClass('in hover');
				}, 100);
			});
		},
			
		_create: function () {
            this._super();
			
			var $element = this.element;
			var widget = this;

			var toggleButtons = function () {
				var $files = $element.find('.files'),
					filesCount = $files.children().length,
					maxFilesReached = widget.options.maxNumberOfFiles > 0
										&& filesCount >= widget.options.maxNumberOfFiles,
					pending = $files.children('.template-upload').not('.processing').length,
					processing = $files.children('.template-upload.processing').length,
					uploaded = $files.children('.template-download').not('.processing').length,
					selected = $files.children('.template-download').not('.processing').find('.toggle:checked').length,
					$buttonBar = $element.find('.fileupload-buttonbar');


				$buttonBar.find('.start').prop('disabled', pending === 0);
				$buttonBar.find('.cancel').prop('disabled', pending === 0 && processing === 0);
				$buttonBar.find('.delete').prop('disabled', selected === 0);
				$buttonBar.find('.toggle').prop('disabled', uploaded === 0);

				if (widget.options.autoDisableAdd) {
					if (maxFilesReached) {
						widget._disableFileInputButton();
						widget.options.dropZone.hide();
					}
					else {
						widget._enableFileInputButton();
						widget.options.dropZone.show();
					}
				}
			};
			$element.bind('fileuploadfailed fileuploadadded fileuploaddestroyed fileuploadcompleted', toggleButtons);
			$element.on('click', '.toggle', toggleButtons);

			$element.bind('fileuploadcompleted', function (e, data) {
				var fileUpload = $element.data('blueimp-fileupload') ||
								$element.data('fileupload')
				fileUpload._renderPreviews(data);
			});

			toggleButtons();
			
			this._defineDropZone();
		},
		
		_destroy: function () {
			$(document).unbind( 'dragover' ).undelegate( 'dragover' );
		}
    });

}));
