function loadFlashView()
{
SwfView.Add("/library/flash/view.swf", "fpv", "swfDIV", "640", "260", "8.0.0.0", "#000", 
{
bigPhotoList: "/uploads/flash/0627042244379998.jpg,/uploads/flash/0627042321810205.jpg,/uploads/flash/0627042420088657.jpg,/uploads/flash/0627042846686425.jpg,/uploads/flash/0627042935540056.jpg,/uploads/flash/0627042958808312.jpg,/uploads/flash/0627043012571716.jpg,/uploads/flash/0627043146672506.jpg,/uploads/flash/0627043048461246.jpg",
smallPhotoList: "/uploads/flash/0627042244641318.jpg,/uploads/flash/0627042321282725.jpg,/uploads/flash/0627042420923938.jpg,/uploads/flash/0627042846058845.jpg,/uploads/flash/0627042935385337.jpg,/uploads/flash/0627042958643583.jpg,/uploads/flash/0627043012943136.jpg,/uploads/flash/0627043146416787.jpg,/uploads/flash/0627043048833675.jpg",
sourcePhotoList: "http://www.julaa.com,http://www.julaa.com,http://www.jualaa.com,http://www.jualaa.com,http://www.jualaa.com,http://www.jualaa.com,http://www.jualaa.com,http://www.jualaa.com,http://www.jualaa.com", itemOverTime: 100, viewTime: 5000
}
,
{
scale: "noscale", allowScriptAccess: "always", wmode: "transparent"
});
SwfView.Init();
}
