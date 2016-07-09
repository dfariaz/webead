/**
 * @license Copyright (c) 2003-2015, CKSource - Frederico Knabben. All rights reserved.
 * For licensing, see LICENSE.md or http://ckeditor.com/license
 */

CKEDITOR.editorConfig = function (config) {
    // Define changes to default configuration here. For example:
    // config.language = 'fr';
    // config.uiColor = '#AADC6E';
    config.width = '100%';
    config.extraPlugins = 'simplebutton';
    config.allowedContent = true;

    config.toolbarGroups = [
		{ name: 'clipboard', groups: ['clipboard', 'undo'] },
		{ name: 'editing', groups: ['find', 'selection', 'spellchecker', 'editing'] },
		{ name: 'forms', groups: ['forms'] },
		{ name: 'links', groups: ['links'] },
		{ name: 'insert', groups: ['insert'] },
		{ name: 'tools', groups: ['tools'] },
		{ name: 'document', groups: ['mode', 'document', 'doctools'] },
		'/',
		{ name: 'paragraph', groups: ['list', 'indent', 'blocks', 'align', 'bidi', 'paragraph'] },
		{ name: 'basicstyles', groups: ['basicstyles', 'cleanup'] },
		'/',
		{ name: 'styles', groups: ['styles'] },
		{ name: 'colors', groups: ['colors'] },
		{ name: 'others', groups: ['others', 'simplebutton'] },
		{ name: 'about', groups: ['about'] }
    ];

    config.removeButtons = 'Save,NewPage,Preview,Print,Templates,Find,SelectAll,Replace,Form,Checkbox,Radio,TextField,Textarea,Select,Button,HiddenField,ImageButton,Flash,Iframe,PageBreak,CreateDiv,Language,Anchor,ShowBlocks,Link,Unlink';
};

