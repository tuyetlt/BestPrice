var path_ckeditor = "/assets";
CKEDITOR.editorConfig = function (config) {
	//config.language = config_language;
	config.indentClasses = ['Indent1', 'Indent2', 'Indent3'];
	config.extraPlugins = 'indent';
	config.extraPlugins = 'codemirror';
	
	config.entities = false;
	config.htmlEncodeOutput = false;
	config.allowedContent = true;
	config.width = "auto";
	config.height = "300";
	config.filebrowserBrowseUrl = path_ckeditor + '/ckfinder/ckfinder.aspx?opener=ckeditor&type=Files';
	config.filebrowserImageBrowseUrl = path_ckeditor + '/ckfinder/ckfinder.aspx?type=Images';
	config.filebrowserFlashBrowseUrl = path_ckeditor + '/ckfinder/ckfinder.aspx?opener=ckeditor&type=Flash';
	config.filebrowserUploadUrl = path_ckeditor + '/ckfinder/ckfinder.aspx?opener=ckeditor&type=Files';
	config.filebrowserImageUploadUrl = path_ckeditor + '/ckfinder/ckfinder.aspx?opener=ckeditor&type=Images';
	config.filebrowserFlashUploadUrl = path_ckeditor + '/ckfinder/ckfinder.aspx?opener=ckeditor&type=flash';

	config.toolbar = [{
			name : 'document',
			groups : ['mode', 'document', 'doctools'],
			items : ['Source', '-', 'Preview', 'Print', '-', 'Templates']
		}, {
			name : 'clipboard',
			groups : ['clipboard', 'undo'],
			items : ['Cut', 'Copy', 'Paste', 'PasteText', 'PasteFromWord', '-', 'Undo', 'Redo']
		}, {
			name : 'insert',
			items : ['Image', 'Flash', 'Table', 'HorizontalRule', 'Smiley', 'SpecialChar', 'PageBreak', 'Iframe']
		}, '/', {
			name : 'basicstyles',
			groups : ['basicstyles', 'cleanup'],
			items : ['Bold', 'Italic', 'Underline', 'Strike', 'Subscript', 'Superscript', '-', 'RemoveFormat']
		}, {
			name : 'paragraph',
			groups : ['list', 'indent', 'blocks', 'align', 'bidi'],
			items : ['NumberedList', 'BulletedList', '-', 'Outdent', 'Indent', '-', 'Blockquote', 'CreateDiv', '-', 'JustifyLeft', 'JustifyCenter', 'JustifyRight', 'JustifyBlock']
		}, {
			name : 'links',
			items : ['Link', 'Unlink', 'Anchor']
		}, '/', {
			name : 'styles',
			items : ['Format', 'Font', 'FontSize']
		}, {
			name : 'colors',
			items : ['TextColor', 'BGColor']
		}, {
			name : 'tools',
			items : ['Maximize', 'ShowBlocks']
		}, {
			name : 'others',
			items : ['-']
		}, {
			name : 'about',
			items : ['About']
		}
	];

	
	config.toolbar_Full =
           [
               { name: 'document', items: ['Source', '-', 'Save', 'NewPage', 'DocProps', 'Preview', 'Print', '-', 'Templates'] },
               { name: 'clipboard', items: ['Cut', 'Copy', 'Paste', 'PasteText', 'PasteFromWord', '-', 'Undo', 'Redo'] },
               { name: 'editing', items: ['Find', 'Replace', '-', 'SelectAll', '-', 'SpellChecker', 'Scayt'] },
               {
                   name: 'forms', items: ['Form', 'Checkbox', 'Radio', 'TextField', 'Textarea', 'Select', 'Button', 'ImageButton',
                     'HiddenField']
               },
               '/',
               { name: 'basicstyles', items: ['Bold', 'Italic', 'Underline', 'Strike', 'Subscript', 'Superscript', '-', 'RemoveFormat'] },
               {
                   name: 'paragraph', items: ['NumberedList', 'BulletedList', '-', 'Outdent', 'Indent', '-', 'Blockquote', 'CreateDiv',
                     '-', 'JustifyLeft', 'JustifyCenter', 'JustifyRight', 'JustifyBlock', '-', 'BidiLtr', 'BidiRtl']
               },
               { name: 'links', items: ['Link', 'Unlink', 'Anchor'] },
               { name: 'insert', items: ['Image', 'Flash', 'Table', 'HorizontalRule', 'Smiley', 'SpecialChar', 'PageBreak', 'Iframe'] },
               '/',
               { name: 'styles', items: ['Styles', 'Format', 'Font', 'FontSize'] },
               { name: 'colors', items: ['TextColor', 'BGColor'] },
               { name: 'tools', items: ['Maximize', 'ShowBlocks', '-', 'About'] }
           ];

	config.toolbar_Basic =
        [
            ['Bold', 'Italic', '-', 'NumberedList', 'BulletedList', '-', 'Link', 'Unlink', '-', 'About']
        ];




	config.codemirror = {

    // Set this to the theme you wish to use (codemirror themes)
    theme: 'default',

    // Whether or not you want to show line numbers
    lineNumbers: true,

    // Whether or not you want to use line wrapping
    lineWrapping: true,

    // Whether or not you want to highlight matching braces
    matchBrackets: true,

    // Whether or not you want to highlight matching tags
    matchTags: true,

    // Whether or not you want tags to automatically close themselves
    autoCloseTags: true,

    // Whether or not you want Brackets to automatically close themselves
    autoCloseBrackets: true,

    // Whether or not to enable search tools, CTRL+F (Find), CTRL+SHIFT+F (Replace), CTRL+SHIFT+R (Replace All), CTRL+G (Find Next), CTRL+SHIFT+G (Find Previous)
    enableSearchTools: true,

    // Whether or not you wish to enable code folding (requires 'lineNumbers' to be set to 'true')
    enableCodeFolding: true,

    // Whether or not to enable code formatting
    enableCodeFormatting: true,

    // Whether or not to automatically format code should be done when the editor is loaded
    autoFormatOnStart: true, 

    // Whether or not to automatically format code which has just been uncommented
    autoFormatOnUncomment: true,

    // Whether or not to highlight the currently active line
    highlightActiveLine: true,

    // Whether or not to highlight all matches of current word/selection
    highlightMatches: true,

     // Define the language specific mode 'htmlmixed' for html  including (css, xml, javascript), 'application/x-httpd-php' for php mode including html, or 'text/javascript' for using java script only 
    mode: 'htmlmixed',

     // Whether or not to show the search Code button on the toolbar
    showSearchButton: true,

     // Whether or not to show Trailing Spaces
    showTrailingSpace: true,

    // Whether or not to show the format button on the toolbar
    showFormatButton: true,

    // Whether or not to show the comment button on the toolbar
    showCommentButton: true,

    // Whether or not to show the uncomment button on the toolbar
    showUncommentButton: true,

     // Whether or not to show the showAutoCompleteButton button on the toolbar
    showAutoCompleteButton: true
};
};

