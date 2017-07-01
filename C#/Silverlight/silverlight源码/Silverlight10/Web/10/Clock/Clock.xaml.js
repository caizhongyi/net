if (!window.Clock)
	window.Clock = {};

Clock.Page = function() 
{
}

Clock.Page.prototype =
{
	handleLoad: function(control, userContext, rootElement) 
	{
		this.control = control;
		
		// Sample event hookup:	
		rootElement.addEventListener("MouseLeftButtonDown", Silverlight.createDelegate(this, this.handleMouseDown));
	},
	
	// Sample event handler
	handleMouseDown: function(sender, eventArgs) 
	{
		// The following line of code shows how to find an element by name and call a method on it.
		// this.control.content.findName("Timeline1").Begin();
	}
}

// TextBlock��MouseLeftButtonDown���õķ���
function MouseLeftButtonDown(sender, args) 
{
    window.open("http://webabcd.cnblogs.com");
}

// TextBlock��MouseMove���õķ���
function MouseMove(sender, args) 
{
    // TextBlock.foreground
    sender.foreground = "red";
    // TextBlock.textDecorations
    sender.textDecorations = "underline";
}

// TextBlock��MouseLeave���õķ���
function MouseLeave(sender, args) 
{
    // TextBlock.foreground
    sender.foreground = "#FF100888";
    // TextBlock.textDecorations
    sender.textDecorations = "none";
}