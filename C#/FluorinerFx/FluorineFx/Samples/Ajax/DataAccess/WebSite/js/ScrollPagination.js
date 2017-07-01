//create ext data store and grid.
var store = null;
var grid = null;

//set default scroll variables.
var currentpos = 0;
var currentpage = 1;
var totalCount = 0;
//set constants.
//itemsperpage: number of records that appear on each page of content.
var itemsperpage = 14;
//number of pages of content to fetch when offset reached.
var pagestofetch = 2;
//number of pixel interval to fire handler request.
var scrolloffset = 263;
var scrolloffsetinterval = 263;
    
//event fires when ext is loaded and ready to display controls.
Ext.onReady(function(){
    
    //set current count.
    var x = document.getElementById("currentcount");
    x.innerHTML = (itemsperpage*pagestofetch);
    
    //create reference to the Service
    var s = new MyDataService();
    
    //call handler
    //to retrieve total records in collection.
    totalCount = s.GetCountriesCount();
    var x = document.getElementById("totalcount");
    x.innerHTML = totalCount;
    
    //set ext state provider.
    Ext.state.Manager.setProvider(new Ext.state.CookieProvider());
    
    //initialize temp data store for handler response.
    var myData
    
    var offset = (currentpage-1) * itemsperpage * pagestofetch;
	var limit = itemsperpage * pagestofetch;
	if(offset + limit > totalCount ) limit = totalCount - offset;

    //call service to retrieve data.
    s.GetCountries(
    offset,
    limit,
    function(response) { //async callback fires when handler returns data.
        //store response.
        myData = response;
        
        //create jason data store and load data.
        store = new Ext.data.JsonStore({data: myData,
                                        root: 'result',
                                        fields: ['ID', 
                                                 'Country', 
                                                 'Capital']
                                        });
              
          
            //create the ext Grid and load data store
            grid = new Ext.grid.GridPanel({
                store: store,
                columns: [
                    {header: "ID", width: 40, sortable: false, dataIndex: 'ID'},
                    {header: "Country", width: 160, sortable: false, dataIndex: 'Country'},
                    {header: "Capital", width: 160, sortable: false, dataIndex: 'Capital'}
                ],
                viewConfig: {
                    forceFit: true
                },
                stripeRows: true,
                height:350,
                width:360,
                header: false,
                title:'Countries'
            });
            //render grid
            grid.render('countrygrid');
            //add scroll event listener
            grid.addListener('bodyscroll',scrollListener);

        }); //end Json call listener.
          
   });  //end ext ready listener
       
//scrollListener
//fires when grid is scrolled.
function scrollListener(scrollLeft, scrollTop){
    //only handle scroll downs past highest position.
    if ( scrollTop > currentpos )
    {
        //check if we should get more data
        if ( scrollTop >  scrolloffset )
        {
            //store current grid scroll state.
            var state = grid.getView().getScrollState();
            
            //adjust scroll offset
            scrolloffset=scrollTop+scrolloffsetinterval;

            //adjust current page
            currentpage=currentpage+1;
            
            var offset = (currentpage-1) * itemsperpage * pagestofetch;
	        var limit = itemsperpage * pagestofetch;
	        if(offset + limit > totalCount ) limit = totalCount - offset;
            if( limit > 0 )
            {
                //initialize temp data store for handler response.
                var myData
                //call GetZipCodes.ashx to retrieve zip code data for next pages of data
                var s = new MyDataService();            
                s.GetCountries(
                offset,
                limit,
                function(response) {  //async callback fires when handler returns data.
                    //store response.
                    myData = response;
                    //append items to json store.
                    store.loadData(myData,true);
                        
                    //restore grid scroll state.
                    grid.getView().restoreScroll(state);
                    
                    //set current count.
                    var x = document.getElementById("currentcount");
                    x.innerHTML = store.getCount();
                    
                     }); //end: async callback fires when handler returns data.
                     
            }  //end: check if we should get more data
         }        
        //reset current scroll position
        currentpos=scrollTop;
        
    } //end last position test.
    
} //end: scrollListener