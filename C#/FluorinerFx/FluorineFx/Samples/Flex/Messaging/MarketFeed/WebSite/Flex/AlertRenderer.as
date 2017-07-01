package
{
    import mx.controls.Label;
    import flash.display.Graphics;
	import flash.geom.Matrix;
	import flash.display.GradientType;
	
	public class AlertRenderer extends Label
	{
		override protected function updateDisplayList(unscaledWidth:Number, unscaledHeight:Number):void
	 	{
			super.updateDisplayList(unscaledWidth, unscaledHeight);
	 		
	 		var g:Graphics = graphics;
	 		
			g.clear();

			if (data && data.alertTime != null)
			{
				g.beginFill(0xFF0000, 0.5);
				g.drawRect(0, 0, unscaledWidth, unscaledHeight);
	 			g.endFill();
			}
		}		
	}
}