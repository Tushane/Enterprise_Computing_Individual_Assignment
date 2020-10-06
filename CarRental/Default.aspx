<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="CarRental._Default" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server" onload="showSlides1()">

   <div >
      
                <!-- Slideshow container -->
        <div class="slideshow-container">

          <!-- Full-width images with number and caption text -->
          <div class="mySlides fade">
            <%--<div class="numbertext">1 / 3</div>--%>
            <img src="../images/oversee.jpg" style="width:100%">
            <div class="text"><h3>Variety is Our Speciality!</h3></div>
          </div>

          <div class="mySlides fade">
            <%--<div class="numbertext">2 / 3</div>--%>
            <img src="../images/banner.jpg" style="width:100%">
            <div class="text"><h3>Variety is Our Speciality!</h3></div>
          </div>

          <div class="mySlides fade">
            <%--<div class="numbertext">3 / 3</div>--%>
            <img src="../images/car1.jpg" style="width:100%">
            <div class="text"><h3>Variety is Our Speciality!</h3></div>
          </div>

           <div class="mySlides fade">
            <%--<div class="numbertext">3 / 3</div>--%>
            <img src="../images/car2.jpg" style="width:100%">
            <div class="text"><h3>Variety is Our Speciality!</h3></div>
          </div>

           <div class="mySlides fade">
            <%--<div class="numbertext">3 / 3</div>--%>
            <img src="../images/car3.jpg" style="width:100%">
            <div class="text"><h3>Variety is Our Speciality!</h3></div>
          </div>
     
        </div>
        <br>

        <!-- The dots/circles -->
        <div style="text-align:center">
          <span class="dot" onclick="currentSlide(1)"></span>
          <span class="dot" onclick="currentSlide(2)"></span>
          <span class="dot" onclick="currentSlide(3)"></span>
           <span class="dot" onclick="currentSlide(4)"></span>
            <span class="dot" onclick="currentSlide(5)"></span>
            <span class="dot" onclick="currentSlide(6)"></span>
        </div>
        
    </div>
   
    <div class="row">
         <hr style="background-color:black" />
        <asp:PlaceHolder ID ="maindivs" runat="server">    </asp:PlaceHolder>
        
        <br />
        <div id ="VIEWALL">
            <asp:Button ID="but" class='btn btn-default' runat="server" Text="VIEW ALL"  PostBackUrl="~/Product.aspx" />

        </div>

    </div>

<script>
var slideIndex = 0;
showSlides();

function showSlides() {
  var i;
  var slides = document.getElementsByClassName("mySlides");
  var dots = document.getElementsByClassName("dot");
  for (i = 0; i < slides.length; i++) {
    slides[i].style.display = "none";  
  }
  slideIndex++;
  if (slideIndex > slides.length) {slideIndex = 1}    
  for (i = 0; i < dots.length; i++) {
    dots[i].className = dots[i].className.replace(" active", "");
  }
  slides[slideIndex-1].style.display = "block";  
  dots[slideIndex-1].className += " active";
  setTimeout(showSlides, 4000); // Change image every 4 seconds
}
</script>

</asp:Content>
