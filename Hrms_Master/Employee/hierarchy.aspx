<%@ Page Language="C#" AutoEventWireup="true" CodeFile="hierarchy.aspx.cs" Inherits="Hrms_Master_Employee_hierarchy" %>


<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <link href="../../js/bootstrap.css" rel="stylesheet" />
    <link href="../../js/bootstrap.min.css" rel="stylesheet" />
    <script type="text/javascript" src="../../packages/jquery/jquery-3.3.1.min.js"></script>
    <script type="text/javascript" src="../../packages/jquery-ui/jquery-ui.min.js"></script>
    <link href="../../Scripts/Index.css" rel="stylesheet" />
    <!-- jQuery UI Layout -->
    <script type="text/javascript" src="../../packages/jquerylayout/jquery.layout-latest.min.js"></script>
    <%--<link rel="stylesheet" type="text/css" href="../../packages/jquerylayout/layout-default-latest.css" />--%>

    <!-- header -->
    <!--<script type="text/javascript" src="../../packages/touch/jquery.ui.touch-punch.js"></script>-->

    <link href="../../min/primitives.latest.css?5100" media="screen" rel="stylesheet" type="text/css" />
    <!-- # include file="../../src.primitives/src.primitives.html"-->
    <script type="text/javascript" src="../../min/primitives.min.js?5100"></script>
    <script type="text/javascript" src="../../min/primitives.jquery.min.js?5100"></script>  
    </head>

<body>
    <form runat="server">
   
    <div id="contentpanel">
        <!--bpcontent-->

        <div id="centerpanel">

            <div id="orgdiagram">
                 <table>
                     <tr>
        <td id="row_branch" runat="server">Select Branch :   </td>
            <td id="row_branch1" runat="server">
                  <asp:DropDownList ID="ddl_Branch" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddl_Branch_SelectedIndexChanged"
                    CssClass="form-control">
                </asp:DropDownList>
            </td>
      <td>Select Department :</td>
            <td> <asp:DropDownList ID="ddl_department" runat="server"
                                            class="form-control" >
                                        </asp:DropDownList>

            </td>
             <td>
                  <input type="button" ID="btn_show" Class="btn btn-success" value="Show" OnClick="lodeh()" />
             </td>          
        </tr>
    </table>
            </div>
        </div>

        <!--/bpcontent-->
    </div>
        </form>
    <script src="../../Scripts/Base.js"></script>
</body>
    <script type="text/javascript">
        var orgdiagram = null;


        var counter = 0;
        var m_timer = null;
        var fromValue = null;
        var fromChart = null;
        var toValue = null;
        var toChart = null;
        var items = {};

        function lodeh() {
            jQuery(document).ready(function () {
                jQuery('body').layout(
                  {
                      center__paneSelector: "#contentpanel"
                  });
            });

            jQuery(document).ready(function () {
                jQuery('#contentpanel').layout(
                  {
                      center__paneSelector: "#centerpanel"

                    , west__paneSelector: "#westpanel"
                    , west__resizable: true
                    , center__onresize: function () {
                        if (orgdiagram != null) {
                            ResizePlaceholder();
                            jQuery("#orgdiagram").orgDiagram("update", primitives.common.UpdateMode.Refresh);

                        }
                    }
                  });

                ResizePlaceholder();
                orgdiagram = SetupWidget(jQuery("#orgdiagram"), "orgdiagram");

            });

            function SetupWidget(element, name) {
                //{ "d": [{ "Id": 1, "name": "Kumar", "desi": "Managing Director", "img": "0", "parent": 0 }, { "Id": 2, "name": "Sri", "desi": "Team Leader", "img": "1", "parent": 1 }, { "Id": 3, "name": "Shira", "desi": "Team Leader", "img": "2", "parent": 1 }, { "Id": 4, "name": "Jon", "desi": "Programmer", "img": "3", "parent": 2 }, { "Id": 5, "name": "Linnea", "desi": "Tester", "img": "4", "parent": 2 }, { "Id": 6, "name": "Jon", "desi": "Programmer", "img": "5", "parent": 3 }, { "Id": 7, "name": "Linnea", "desi": "Tester", "img": "6", "parent": 3 }, { "Id": 8, "name": "Steffan", "desi": "Programmer", "img": "7", "parent": 3 }] };
                var allcookies = document.cookie;
                var cookiearray = allcookies.split(';');
                for (var c = 0; c < cookiearray.length; c++) {
                    if (cookiearray[c].split('=')[0] == " Login_temp_Role") {
                        var RoleId = cookiearray[c].split('=')[1];
                    }
                    if (cookiearray[c].split('=')[0] == " Login_temp_BranchID") {
                        var Branch = cookiearray[c].split('=')[1];
                    }
                }

                if (RoleId == 'a')
                {
                    var b = document.getElementById("ddl_Branch");
                     Branch = d.options[d.selectedIndex].value;
                }
              
                var d = document.getElementById("ddl_department");
                var did = d.options[d.selectedIndex].value;
                if (did != 0) {

                    $.ajax({
                        type: 'Post',
                        url: 'hierarchy.aspx/Emp_select',
                        data: '{"did":' + did + ',"Branch":' + Branch + '}',
                        dataType: "json",
                        contentType: "application/json; charset=utf-8",
                       
                        success: function (msg) {
                            var result;
                            var options = new primitives.orgdiagram.Config();
                            var itemsToAdd = [];

                            var data = msg.d;

                            for (var i = 0; i < 1; i++) {

                                var newItem = new primitives.orgdiagram.ItemConfig({
                                    id: data[i].Id,
                                    parent: null,
                                    title: data[i].name,
                                    description: data[i].desi,
                                    phone: data[i].phone,
                                    email: data[i].email,
                                    href: "mailto:"+data[i].email,
                                    image: "../../Photo/" + data[i].img
                                });
                                itemsToAdd.push(newItem);
                                items[newItem.id] = newItem;

                                if (options.cursorItem == null) {
                                    options.cursorItem = newItem.id;
                                }

                                for (var J = 1; J <= data.length - 1; J++) {

                                    var newSubItem = new primitives.orgdiagram.ItemConfig({
                                        id: data[J].Id,
                                        parent: data[J].parent,
                                        title: data[J].name,
                                        description: data[J].desi,
                                        phone: data[J].phone,
                                        email: data[J].email,
                                        href: "mailto:" + data[J].email,
                                        image: "../../Photo/" + data[J].img
                                    });
                                    itemsToAdd.push(newSubItem);
                                    items[newSubItem.id] = newSubItem;
                                }
                            }
                            options.items = itemsToAdd;
                            options.normalLevelShift = 20;
                            options.dotLevelShift = 10;
                            options.lineLevelShift = 10;
                            options.normalItemsInterval = 20;
                            options.dotItemsInterval = 10;
                            options.lineItemsInterval = 5;
                            options.buttonsPanelSize = 48;

                            options.pageFitMode = primitives.common.PageFitMode.None;
                            options.graphicsType = primitives.common.GraphicsType.Auto;
                            options.hasButtons = primitives.common.Enabled.True;
                            options.templates = [getContactTemplate()];
                            options.defaultTemplateName = "contactTemplate";
                            options.onItemRender = (name == "orgdiagram") ? onOrgDiagramTemplateRender : onOrgDiagram2TemplateRender;


                            /* chart uses mouse drag to pan items, disable it in order to avoid conflict with drag & drop */
                            options.enablePanning = false;

                            result = element.orgDiagram(options);

                            element.droppable({
                                greedy: true,
                                drop: function (event, ui) {
                                    /* Check drop event cancelation flag
                                    * This fixes following issues:
                                    * 1. The same event can be received again by updated chart
                                    * so you changed hierarchy, updated chart and at the same drop position absolutly
                                    * irrelevant item receives again drop event, so in order to avoid this use primitives.common.stopPropagation
                                    * 2. This particlular example has nested drop zones, in order to
                                    * suppress drop event processing by nested droppable and its parent we have to set "greedy" to false,
                                    * but it does not work.
                                    * In this example items can be droped to other items (except immidiate children in order to avoid looping)
                                    * and to any free space in order to make them rooted.
                                    * So we need to cancel drop  event in order to avoid double reparenting operation.
                                    */
                                    if (!event.cancelBubble) {
                                        toValue = null;
                                        toChart = name;

                                        Reparent(fromChart, fromValue, toChart, toValue);

                                        primitives.common.stopPropagation(event);
                                    }
                                }
                            });

                            return result;

                        },
                        error: function (msg) {


                        }
                    });

                }
                else {
                    alert("Select Department!")
                }
            }

            function getContactTemplate() {
                var result = new primitives.orgdiagram.TemplateConfig();
                result.name = "contactTemplate";

                result.itemSize = new primitives.common.Size(200, 100);
                result.minimizedItemSize = new primitives.common.Size(4, 4);
                result.highlightPadding = new primitives.common.Thickness(2, 2, 2, 2);


                var itemTemplate = jQuery(
                  '<div class="bp-item bp-corner-all bt-item-frame">'
                    + '<div name="titleBackground" class="bp-item bp-corner-all bp-title-frame" style="top: 2px; left: 2px; width: 195px; height: 20px; ">'
                  + '<div name="title" class="bp-item bp-title" style="top: 3px; left: 6px; width: 175px; height: 18px;">'
                  + '</div>'
                  + '</div>'
                    + '<div class="bp-item bp-photo-frame" style="top: 26px; left: 2px; width: 60px; height: 70px; border-radius: 5px;">'
                  + '<img name="photo" style="height:70px; width:60px;" />'
                  + '</div>'
                    + '<div name="description" class="bp-item" style="top: 26px; left: 70px; width: 120px; height: 23px; font-size: 13px;"></div>'
                    + '<div name="phone" class="bp-item" style="top: 49px; left: 70px; width: 120px; height: 23px; font-size: 13px;"></div>'
                    + '<a href="" target="_blank" id="email" name="email" class="bp-item" style="top: 72px; left: 70px; width: 120px; height: 23px; font-size: 13px;"></a>'
                  + '</div>'
                ).css({
                    width: result.itemSize.width + "px",
                    height: result.itemSize.height + "px"
                }).addClass("bp-item bp-corner-all bt-item-frame");
                result.itemTemplate = itemTemplate.wrap('<div>').parent().html();

                return result;
            }

            function onOrgDiagramTemplateRender(event, data) {
                switch (data.renderingMode) {
                    case primitives.common.RenderingMode.Create:
                        data.element.draggable({
                            revert: "invalid",
                            containment: "document",
                            appendTo: "body",
                            helper: "clone",
                            cursor: "move",
                            "zIndex": 10000,
                            delay: 300,
                            distance: 10,
                            start: function (event, ui) {
                                fromValue = parseInt(jQuery(this).attr("data-value"), 10);
                                fromChart = "orgdiagram";
                            }
                        });
                        data.element.droppable({
                            /* this option is supposed to suppress event propagation from nested droppable to its parent
                            *  but it does not work
                            */
                            greedy: true,
                            drop: function (event, ui) {
                                if (!event.cancelBubble) {
                                    alert("Drop accepted!");
                                    toValue = parseInt(jQuery(this).attr("data-value"), 10);
                                    toChart = "orgdiagram";

                                    Reparent(fromChart, fromValue, toChart, toValue);

                                    primitives.common.stopPropagation(event);
                                } else {
                                    alert("Drop ignored!");
                                }
                            },
                            over: function (event, ui) {
                                toValue = parseInt(jQuery(this).attr("data-value"), 10);
                                toChart = "orgdiagram";

                                /* this is needed in order to update highlighted item in chart,
                                * so this creates consistent mouse over feed back
                                */
                                jQuery("#orgdiagram").orgDiagram({ "highlightItem": toValue });
                                jQuery("#orgdiagram").orgDiagram("update", primitives.common.UpdateMode.PositonHighlight);
                            },
                            accept: function (draggable) {
                                /* be carefull with this event it is called for every available droppable including invisible items on every drag start event.
                                * don't varify parent child relationship between draggable & droppable here it is too expensive.
                                */
                                return (jQuery(this).css("visibility") == "visible");
                            }
                        });
                        /* Initialize widgets here */
                        break;
                    case primitives.common.RenderingMode.Update:
                        /* Update widgets here */
                        break;
                }

                var itemConfig = data.context;

                /* Set item id as custom data attribute here */
                data.element.attr("data-value", itemConfig.id);

                RenderField(data, itemConfig);
            }


            function Reparent(fromChart, value, toChart, toParent) {
                /* following verification needed in order to avoid conflict with jQuery Layout widget */
                if (fromChart != null && value != null && toChart != null) {
                    alert("Reparent fromChart:" + fromChart + ", value:" + value + ", toChart:" + toChart + ", toParent:" + toParent);
                    var item = items[value];
                    var fromItems = jQuery("#" + fromChart).orgDiagram("option", "items");
                    var toItems = jQuery("#" + toChart).orgDiagram("option", "items");
                    if (toParent != null) {
                        var toParentItem = items[toParent];
                        if (!isParentOf(item, toParentItem)) {
                            if (value > 3) {
                                $.ajax({
                                    type: 'Post',
                                    url: 'hierarchy.aspx/Emp_update',
                                    contentType: "application/json; charset=utf-8",
                                    data: ' {id:"' + value + '",toParent:"' + toParent + '"}',
                                    success: function (msg) {
                                        var children = getChildrenForParent(item);
                                        children.push(item);
                                        for (var index = 0; index < children.length; index++) {
                                            var child = children[index];
                                            fromItems.splice(primitives.common.indexOf(fromItems, child), 1);
                                            toItems.push(child);
                                        }
                                        item.parent = toParent;
                                    },
                                    error: function (msg) {
                                        alert(msg);
                                    }
                                });
                            }
                        }
                        else {
                            alert("Droped to own child!");
                        }
                    }
                    else {

                    }
                    jQuery("#orgdiagram").orgDiagram("update", primitives.common.UpdateMode.Refresh);

                }
            }


            function getChildrenForParent(parentItem) {
                var children = {};
                for (var id in items) {
                    var item = items[id];
                    if (children[item.parent] == null) {
                        children[item.parent] = [];
                    }
                    children[item.parent].push(id);
                }
                var newChildren = children[parentItem.id];
                var result = [];
                if (newChildren != null) {
                    while (newChildren.length > 0) {
                        var tempChildren = [];
                        for (var index = 0; index < newChildren.length; index++) {
                            var item = items[newChildren[index]];
                            result.push(item);
                            if (children[item.id] != null) {
                                tempChildren = tempChildren.concat(children[item.id]);
                            }
                        }
                        newChildren = tempChildren;
                    }
                }
                return result;
            }

            function isParentOf(parentItem, childItem) {
                var result = false,
                  index,
                  len,
                  itemConfig;
                if (parentItem.id == childItem.id) {
                    result = true;
                } else {
                    while (childItem.parent != null) {
                        childItem = items[childItem.parent];
                        if (childItem.id == parentItem.id) {
                            result = true;
                            break;
                        }
                    }
                }
                return result;
            };

            function RenderField(data, itemConfig) {
                if (data.templateName == "contactTemplate") {
                    data.element.find("[name=photo]").attr({ "src": itemConfig.image, "alt": itemConfig.title });
                    data.element.find("[name=titleBackground]").css({ "background": itemConfig.itemTitleColor });

                    var fields = ["title", "description", "phone", "email"];
                    for (var index = 0; index < fields.length; index++) {
                        var field = fields[index];

                        var element = data.element.find("[name=" + field + "]");
                        if (element.text() != itemConfig[field]) {
                            element.text(itemConfig[field]);
                            if (field == "email")
                            {
                                element.attr('href', itemConfig.href);
                            }
                        }
                    }
                }
            }

            function ResizePlaceholder() {
                var panel = jQuery("#centerpanel");
                var panelSize = new primitives.common.Rect(0, 0, panel.innerWidth(), panel.innerHeight());
                var position = new primitives.common.Rect(0, 0, panelSize.width / 1, panelSize.height);

                jQuery("#orgdiagram").css(position.getCSS());

            }
        }
    </script>
    <!-- /header -->

</html>
