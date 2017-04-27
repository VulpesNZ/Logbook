<%@ Page Language="C#" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<script runat="server">

</script>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Title</title>
</head>
<body onload="GetActivitiesAsync()">
<form id="HtmlForm" runat="server">
    <div>
        
    </div>
</form>
</body>
</html>

<script type='text/javascript'>
    function GetActivitiesAsync() {
        var userId = '8C0EE471-23D3-454B-93AC-69FC9E94AC77';
        var url = 'http://localhost:53279/api/activities/' + userId;
        return fetch(url)
            .then(function(data) { return data.json(); })
            .then(function(actualData) {
                // Get all activities
                var activities = [];
                for (i = 0; i < actualData.length; i++) {
                    if (activities.indexOf(actualData[i].ActivityId) == -1) {
                        activities.push(actualData[i].ActivityId);
                        alert(actualData[i].ActivityId);
                        
                    }
                }
                // Store in AsyncStorage as JSON objects by activity

            }.bind(this))
            .catch(function(error) {
                alert(error);
                // If there is any error you will catch them here
            });
    }

    function GetActivitiesAsync2 (userId) {
        return fetch('http://www.theoutdoorlogbook.com/api/api/getactivities')
                .then(function (data) { return data.json(); })
                           .then (function (actualData) {
                               var activities = ['Select...'];
                               for (i = 0; i < actualData.length; i++)
                               {
                                   activities.push(actualData[i].Name);
                               }
                               this.setState({ activities:activities });
                           }.bind(this))
                       .catch(function (error) {
                           alert(error);
                           // If there is any error you will catch them here
                       });
    }
</script>