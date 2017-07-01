
if(typeof(_10powertalk_float_load_status) == 'undefined' || !_10powertalk_float_load_status)
{ 
	var _10powertalk_float_load_status = true;

    if( $10powertalk_get("_10powertalk_Icon_container") ){
	    _10powertalk_imFloat = new _10powertalk_Float(); 
	    _10powertalk_imFloat.layer_div = "_10powertalk_serviceIcon";
	    _10powertalk_imFloat.container_div = "_10powertalk_Icon_container";
	    _10powertalk_imFloat.floatHtml = _10powertalk_Inner_Cont;
	    _10powertalk_imFloat.padding = _10powertalk_l_icon_padding;
	    _10powertalk_imFloat.top = _10powertalk_l_icon_top;
	    _10powertalk_imFloat.width = _10powertalk_l_icon_width;
	    _10powertalk_imFloat.align = _10powertalk_l_icon_align;
	    _10powertalk_imFloat.valign = _10powertalk_l_icon_valign;
	    _10powertalk_imFloat.drag_status = true;
	    _10powertalk_imFloat.move_status = true;

	    _10powertalk_imFloat.init("_10powertalk_imFloat");
    	
	    if( typeof(_10powertalk_l_icon_hide) == 'undefined' || _10powertalk_l_icon_hide != 1 )
           _10powertalk_imFloat.show();
    }    
	_10powertalk_inviteFloat = new _10powertalk_Float(); 
	_10powertalk_inviteFloat.padding = _10powertalk_l_invite_padding;
	_10powertalk_inviteFloat.top = _10powertalk_l_invite_top;
	_10powertalk_inviteFloat.width = _10powertalk_l_invite_width;
	_10powertalk_inviteFloat.align = _10powertalk_l_invite_align;
	_10powertalk_inviteFloat.valign = _10powertalk_l_invite_valign;
	_10powertalk_inviteFloat.layer_div = "_10powertalk_inviteIcon";
	_10powertalk_inviteFloat.container_div = "_10powertalk_Invite_container";
	_10powertalk_inviteFloat.floatHtml = _10powertalk_Inner_Invite;
	_10powertalk_inviteFloat.drag_status = true;
	_10powertalk_inviteFloat.move_status = true;	
	_10powertalk_inviteFloat.init("_10powertalk_inviteFloat");	
    function _10powertalk_hidePanel(){
       if( _10powertalk_imFloat )
           _10powertalk_imFloat.hide();
    }
    function _10powertalk_hideInvite(){
       _10powertalk_inviteFloat.hide();
	   _10powertalk_Invite_Send = 0;
    }    
    function _10powertalk_collapse(listGroup){
        lg = $10powertalk_get(listGroup);
        
        if( lg )
            lg.style.display = lg.style.display == "none"?"":"none";
    }    
	if( typeof(_10powertalk_l_ifInvite) == 'undefined' || _10powertalk_l_ifInvite == 1 ){
	    if( typeof(_10powertalk_l_inviteWay) == 'undefined' || _10powertalk_l_inviteWay == 1 || window.name!="10powertalk"){ 
	        _10powertalk_Invite_Send = 1;
            window.setTimeout("_10powertalk_inviteFloat.show()", 5000);
            window.name="10powertalk";
        }
    }
	function _10powertalk_receiveApply(Invite_UserID, CallCount){
	    _10powertalk_Invite_Send = 1;
	    _10powertalk_Invite_UserID = Invite_UserID;
	    _365groups_CallCount = CallCount;
	     window.focus();
	    _10powertalk_inviteFloat.show();
	}
}
