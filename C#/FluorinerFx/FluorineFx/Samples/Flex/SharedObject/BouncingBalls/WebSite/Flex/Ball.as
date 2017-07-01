package
{
	import flash.display.Shape;
	import flash.events.Event;
	import flash.events.MouseEvent;
	import flash.display.Bitmap;
	import flash.display.Sprite;
	
	public class Ball extends Sprite
	{
		public var radius:Number = 40;
		public var diameter:Number;
		public var vx:Number = 0;
		public var vy:Number = 0;
        private var bounce:Number = -0.7;   
        private var gravity:Number = .86;   
        private var mass:Number = 10;   
        private var springConstant:Number = 1200; // represents 1200 N/m   
        private var springDistance:Number;   
        private var oldX:Number;   
        private var oldY:Number;   
			
		public function Ball ( )
		{
			diameter = radius * 2;
	       this.graphics.beginFill( 0xff9955 , 1 );
	       this.graphics.drawCircle( 0 , 0 , radius );
	       this.graphics.endFill();         
		}                
		
		// initialization, called after parent addChild                
		public function init ( ):void                            
		{                        
			// x , y and stage are inherited properties            
			// watching our width and height not to cross "wall"                        
			x = width / 2 + Math.random( ) * ( parent.width - width / 2 );                    
			y = height / 2 + Math.random( ) * ( parent.height  - height / 2 );                        
			addEventListener( Event.ENTER_FRAME , onEnterFrame );                        
			addEventListener( MouseEvent.MOUSE_DOWN , onMouseDown );            
		}
		
		public function onEnterFrame ( event:Event ):void        
		{                        
            vy += gravity;   
            x += vx;   
            y += vy;   
  
            var ltWall:Number = 0;   
            var rtWall:Number = stage.stageWidth;   
            var topWall:Number = 0;   
            var botWall:Number = stage.stageHeight;   
  
            if(x + radius > rtWall)   
            {   
                x = rtWall - radius;   
                vx *= bounce;   
            }   
            else if(x - radius < ltWall)   
            {   
                x = ltWall + radius;   
                vx *= bounce;   
            }   
            if(y + radius > botWall)   
            {   
                y = botWall - radius;   
                vy *= bounce;   
            }   
            else if(y - radius < topWall)   
            {   
                y = topWall + radius;   
                vy *= bounce;   
            } else {   
            }                  
		}          
				
		private function onMouseDown ( event:MouseEvent ):void        
		{                        
            oldX = x;   
            oldY = y;   
            stage.addEventListener(MouseEvent.MOUSE_UP, onMouseUp);   
            startDrag();   
  
            removeEventListener(Event.ENTER_FRAME, onEnterFrame);   
            addEventListener(Event.ENTER_FRAME, trackVelocity);   			
		}             
		
        private function onMouseUp(event:MouseEvent):void  
        {   
            stage.removeEventListener(MouseEvent.MOUSE_UP, onMouseUp);   
            stopDrag();   
            removeEventListener(Event.ENTER_FRAME, trackVelocity);   
            addEventListener(Event.ENTER_FRAME, onEnterFrame);   
        }   
  
        private function trackVelocity(event:Event):void  
        {   
            vx = x - oldX;   
            vy = y - oldY;   
            oldX = x;   
            oldY = y;   
        }  		   		
	}    	
}