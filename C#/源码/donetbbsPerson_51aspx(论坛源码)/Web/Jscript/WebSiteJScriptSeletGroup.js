<!--
			function deleteOption(object,index) {
				object.options[index] = null;
			}

			function addOption(object,text,value) {
				var defaultSelected = true;
				var selected = true;
				var optionName = new Option(text, value, defaultSelected, selected)
				object.options[object.length] = optionName;
			}

			function copySelected(fromObject,toObject) {
				for (var i=0, l=fromObject.options.length;i<l;i++) {
					if (fromObject.options[i].selected)
						addOption(toObject,fromObject.options[i].text,fromObject.options[i].value);
				}
				for (var i=fromObject.options.length-1;i>-1;i--) {
					if (fromObject.options[i].selected)
						deleteOption(fromObject,i);
				}
			}

			function copyAll(fromObject,toObject) {
				for (var i=0, l=fromObject.options.length;i<l;i++) {
					addOption(toObject,fromObject.options[i].text,fromObject.options[i].value);
				}
				for (var i=fromObject.options.length-1;i>-1;i--) {
					deleteOption(fromObject,i);
				}
			}
			function SetlistAssignedRoles()
			{
				var moveIndex="";
				if (document.all.GroupList.value=="")
				{
					return;
				}
				else
				{
				for(var j=0;j<listAvailableRoles.options.length;j++)
				{
					for(var i=0;i<document.all.GroupList.value.split(',').length;i++)
					{
							if (document.all.GroupList.value.split(',')[i]==listAvailableRoles.options[j].value)
							{
								addOption(document.all.listAssignedRoles,document.all.listAvailableRoles.options[j].text,listAvailableRoles.options[j].value);
					//deleteOption(document.all.listAvailableRoles,j);
								if (moveIndex=="")
								{
									moveIndex +=j;
								}
								else
								{
									moveIndex +=","+j;
								}
								//
							}
						}					
					}//
				}
				for(var i=0;i<moveIndex.split(',').length;i++)
				{
					deleteOption(document.all.listAvailableRoles,moveIndex.split(',')[i]-i);
				}
				//alert()
			}//
			
			
			function JsSubmitSelectGroupOk()
			{
				document.all.GroupList.value="";
				for (var i=0;i<listAssignedRoles.options.length;i++)
				{
					if (document.all.GroupList.value=="")
					{
						document.all.GroupList.value=listAssignedRoles.options[i].value;
					}
					else
					{
						document.all.GroupList.value+=","+listAssignedRoles.options[i].value;
					}
				}			
				eval("document.all."+document.all.GroupListName.value).value=document.all.GroupList.value;
				JsSubmitSelectGroupCancle();	
			}
			
			function JsSubmitSelectGroupCancle()
			{
				document.all.XmlQueryID.innerHTML-"";
				document.all.XmlQueryID.style.display="none";
				ScreenClear("document");
			}
			
		//-->