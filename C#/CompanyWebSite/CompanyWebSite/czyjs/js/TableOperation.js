function addRow(obj)
{
    var clickedRow = obj;

    while(clickedRow.tagName != "TR")
    {
       clickedRow = clickedRow.parentNode;
  }

  var clickedTable = clickedRow.parentNode;

 var newRow = clickedTable.insertRow(clickedRow.rowIndex + 1);

  newRow.className = clickedRow.className;

 for(var i = 0; i < clickedRow.childNodes.length; i++)
    {
      if (clickedRow.childNodes[i].tagName != "TD")
    {
         continue;
     }

  var newCell = clickedRow.childNodes[i].cloneNode(true);

  for(var j = 0; j < newCell.childNodes.length; j++)
{
        if (newCell.childNodes[j].value)
          {
               newCell.childNodes[j].value = "";
           }
       }

        newRow.appendChild(newCell);
    }
 }

function removeRow(obj)
 {
     var clickedRow = obj;

    while(clickedRow.tagName != "TR")
    {
       clickedRow = clickedRow.parentNode;
     }

     clickedRow.parentNode.removeChild(clickedRow);
 }


//调用方式：摘要在单元格里找一个载体，加上事件就可以了：如：onclick="addRow(this)" / onclick="removeRow(this)"

//代码兼容IE/Firefox。

//==========================================

//补充一个如何删除表格中的列：

 function removeCol(col_index){
     for (var i = 0; i < document.getElementById("table").rows.length; i++) {
         document.getElementById("table").rows[i].deleteCell(col_index);
     }
}
