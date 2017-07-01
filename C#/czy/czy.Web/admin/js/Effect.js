function mouseover(obj)
{    
    
     for(var i=0;i<obj.childNodes.length;i++)
     {
         obj.childNodes[i].style.backgroundColor="#7f99da";
         obj.childNodes[i].style.cursor="pointer";
     }
}
function mouseout(obj)
{
     for(var i=0;i<obj.childNodes.length;i++)
     {
         obj.childNodes[i].style.backgroundColor="#f0f0f0";
      
     }
   
}

function RowClick(url)
{
    //window.open (url,'_self');
   // location.href=url;
}

