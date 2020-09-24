<%@ Page Title="Contact" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Contact.aspx.cs" Inherits="CarRental.Contact" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <h3>Where to Find US:</h3>
   <div id="map"></div>
    <script>
        // Initialize and add the map
        function initMap() {
          // The location of Uluru
          var uluru = {lat: 18.0182, lng: -76.7441};
          // The map, centered at Uluru
          var map = new google.maps.Map(
              document.getElementById('map'), {zoom: 10, center: uluru});
          // The marker, positioned at Uluru
          var marker = new google.maps.Marker({position: uluru, map: map});
        }
      </script>
 
    <script defer
    src="https://maps.googleapis.com/maps/api/js?key=AIzaSyBf4uB4jfi4hjUStU5_BMIJKeu9xzkhEek&callback=initMap">
    </script>
    
     <%--<iframe width="100%" height="390" frameborder="0" scrolling="no" marginheight="0" marginwidth="0" id="gmap_canvas" src="https://maps.google.com/maps?width=940&amp;height=390&amp;hl=en&amp;q=237%20W.I,%206%20Old%20Hope%20%20Road%20Kingston+(Where%20to%20Find%20Us:)&amp;t=&amp;z=12&amp;ie=UTF8&amp;iwloc=B&amp;output=embed"></iframe> <a href='https://embedmap.org/'>how to embed google map</a> 
    <script type='text/javascript' src='https://maps-generator.com/google-maps-authorization/script.js?id=16e0ac4526c81cef4de25578ae846d273466d8c7'></script>--%>


    <address>
        <h4>OFFIC LOCATION:</h4>
        Jake's Car Rental<br />
        237 W.I, 6 Old Hope Rd,
        <br />Kingston<br />
        <abbr title="Phone">Phone Number:</abbr>
        876-983-2321
        <br />
        <abbr title="Email">Email Address:</abbr>
        tushanemclean@gmail.com
    </address>

    <address>
        <strong>Support:</strong>   <a href="mailto:Support@example.com">support@jakescarrental.com</a><br />
        <strong>Marketing:</strong> <a href="mailto:Marketing@example.com">marketing@jakescarrental.com</a>
    </address>
</asp:Content>
