function get_param(param_name)
{
    var query = location.search.substring(1);
    var pairs = query.split('&');
    for (var i = 0; i < pairs.length; i++)
    {
        var pos = pairs[i].indexOf('=');
        if (pos == -1) continue;
        var argname = pairs[i].substring(0,pos);
        if(argname.toLowerCase() == param_name.toLowerCase())
        {
            var value = pairs[i].substring(pos+1);
            value = decodeURIComponent(value);
            return value;
        }
    }
    return null;
}

function getCookie_union(name)
{
  var cookieValue = "";
  var search = name + "=";
  if(document.cookie.length > 0)
  {
    offset = document.cookie.indexOf(search);
    if (offset != -1)
    {	
      offset += search.length;
      end = document.cookie.indexOf(";", offset);
      if (end == -1) end = document.cookie.length;
      cookieValue = unescape(document.cookie.substring(offset, end))
    }
  }
  return cookieValue;
}

function setCookie_union(cookieName,cookieValue,DayValue)
{
    var expire = "";
    var day_value=1;
    if(DayValue!=null)
    {
	    day_value=DayValue;
    }
    expire = new Date((new Date()).getTime() + day_value * 86400000);
    expire = "; expires=" + expire.toGMTString();
    document.cookie = cookieName + "=" + escape(cookieValue) +";domain=8mmo.com;path=/"+ expire;
}

function get_counter_url()
{
    var source = get_param('sourceid');
    var isnet=get_param('isnet');
    
    if (source!=null)
    {
        var cookiename;
        if(isnet==0 || isnet==null)
        {
            cookiename="union_visited";//外部广告
            isnet=0;
        }
        else
            cookiename="union_visited_net";//内部广告
        
        var visited = getCookie_union(cookiename);
        var isnew;
        var counter_url ="";
        if(visited == source)
        {
            isnew = 0;
        }
        else
        {
            setCookie_union(cookiename,source, 1);
            isnew = 1;
            counter_url='http://click.8mmo.com/mpclick/counter.aspx?p1=' + source+ '&isnew=' + isnew+'&net='+isnet;// + '&p2=' + escape(location.href) + '&p3=' + isnew + '&p4=' + escape(document.referrer);
        }        
        return counter_url;
    }
    return "";
}