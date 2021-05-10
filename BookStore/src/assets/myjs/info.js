
function myFun(){
    alert("Alret myfunc");
}
//alert("Just Alert");
// document.addEventListener('click',function(){
//     alert("work")
// });

function myalfa(){

}
function myFunction() {
    document.getElementById("myDropdown").classList.toggle("show");
  }
  
  function myalfa(){
    //  document.getElementById("sidebar").style.width="0";
    //  document.getElementById("sidebar").style.marginLeft = "0";
     if(document.getElementsByClassName("statusopen")[0]!=null){
      document.getElementById("sidebar").classList.remove('active');
      document.getElementById("sidebar").classList.remove('statusopen');
     }
     else{
      document.getElementById("sidebar").classList.add('active');
      document.getElementById("sidebar").classList.add('statusopen');
     }
      
    }


