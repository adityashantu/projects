<%@ Assembly Name="$SharePoint.Project.AssemblyFullName$" %>
<%@ Assembly Name="Microsoft.Web.CommandUI, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register Tagprefix="SharePoint" Namespace="Microsoft.SharePoint.WebControls" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register Tagprefix="Utilities" Namespace="Microsoft.SharePoint.Utilities" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register Tagprefix="asp" Namespace="System.Web.UI" Assembly="System.Web.Extensions, Version=3.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" %>
<%@ Import Namespace="Microsoft.SharePoint" %> 
<%@ Register Tagprefix="WebPartPages" Namespace="Microsoft.SharePoint.WebPartPages" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ucMyRequest.ascx.cs" Inherits="CPTUDocumentApprovalWorkflow.CONTROLTEMPLATES.DocumentApproval.ucMyRequest" %>
<SharePoint:ScriptLink name="clienttemplates.js" runat="server" LoadAfterUI="true" Localizable="false"/>
<SharePoint:ScriptLink name="clientforms.js" runat="server" LoadAfterUI="true" Localizable="false"/>
<SharePoint:ScriptLink name="clientpeoplepicker.js" runat="server" LoadAfterUI="true" Localizable="false"/>
<SharePoint:ScriptLink name="autofill.js" runat="server" LoadAfterUI="true" Localizable="false"/>
<SharePoint:ScriptLink name="sp.js" runat="server" LoadAfterUI="true" Localizable="false"/>
<SharePoint:ScriptLink name="sp.runtime.js" runat="server" LoadAfterUI="true" Localizable="false"/>
<SharePoint:ScriptLink name="sp.core.js" runat="server" LoadAfterUI="true" Localizable="false"/>
  <script type="text/javascript">
      <!--Datatable-->
      $(document).ready(function () {
          $("#gvMyrequests").prepend($("<thead></thead>").append($(this).find("tr:first"))).dataTable({
              aaSorting: [[0, 'desc']]
          });
      });
  </script>


<%--    <div class="my-content">
        <asp:TextBox ID="TextBox1" runat="server"></asp:TextBox>
        <div class="p-title"><h6>My Requests</h6></div> 
   
  </div> 
   <asp:Button ID="btnSubmit" CssClass="btn btn-d btn-sm" OnClick="btnSubmit_Click" runat="server" Text="Submit for Approval" />
     <div id="peoplePickerDiv" style="height: 22px" >
 
      <div>
            <br/>
            <input type="button" value="Get User Info" onclick="getUserInfo()">Button</input>
            <br/>
            <h1>User info:</h1>
            <p id="resolvedUsers"></p>
            <h1>User keys:</h1>
            <p id="userKeys"></p>
            <h1>User ID:</h1>
            <p id="userId"></p>
      </div>
         <script type="text/javascript">
             $(document).ready(function () {

                 // Specify the unique ID of the DOM element where the// picker will render.
                 initializePeoplePicker('peoplePickerDiv');
             });

             function initializePeoplePicker(peoplePickerElementId) {

                 var schema = {};
                 schema['PrincipalAccountType'] = 'User,DL,SecGroup,SPGroup';
                 schema['SearchPrincipalSource'] = 15;
                 schema['ResolvePrincipalSource'] = 15;
                 schema['AllowMultipleValues'] = false;
                 schema['MaximumEntitySuggestions'] = 50;
                 schema['Width'] = '280px';
                 schema['Height'] = '55px';

                 this.SPClientPeoplePicker_InitStandaloneControlWrapper(peoplePickerElementId, null, schema);
                 getUserInfo();
             }

             function getUserInfo() {

                 var peoplePicker = this.SPClientPeoplePicker.SPClientPeoplePickerDict.peoplePickerDiv_TopSpan;

                 var users = peoplePicker.GetAllUserInfo();
                 var userInfo = '';
                 for (var i = 0; i < users.length; i++) {
                     var user = users[i];
                     for (var userProperty in user) {
                         userInfo += userProperty + ':  ' + user[userProperty] + '<br>';
                     }
                 }
                 // $('#resolvedUsers').html(userInfo);

                 var keys = peoplePicker.GetAllUserKeys();
                 console.log(userInfo);
                 // $('#userKeys').html(keys);
                 document.getElementById('<%=TextBox1.ClientID%>').value = keys;
             }




         </script>
--%>





















       
         <asp:GridView ID="gvMyrequests" Class="gvMyRequest"  EnableModelValidation="True" style="clear:none; overflow-y:auto;height: auto" runat="server" ClientIDMode="Static" CssClass="table table-bordered"  AutoGenerateColumns="False" ForeColor="#979797" GridLines="None"  >
            <AlternatingRowStyle BackColor="White" />
            <FooterStyle BackColor="#990000" Font-Bold="True" ForeColor="White" HorizontalAlign="Center"/>
            <HeaderStyle BackColor="#4647B8" Font-Bold="True" ForeColor="White" />
            <PagerStyle BackColor="#FFCC66" ForeColor="#333333" HorizontalAlign="Center" />
            <RowStyle HorizontalAlign="Center" />
            <SelectedRowStyle BackColor="#FFCC66" Font-Bold="True" ForeColor="Navy" />               
                <Columns>
                <asp:BoundField DataField="ID" HeaderText="ID" Visible="False" />
                <asp:BoundField DataField="FormLink" HeaderText="Form Link" Visible="False" /> 
                         
               
                <%-- <asp:TemplateField HeaderText="Request Number">
                    <ItemTemplate>                 
                    <asp:HyperLink ID="HyperLink1" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"ReqNo") %>' NavigateUrl='<%# Eval("ReqLink", "{0}") %>'></asp:HyperLink>
                    </ItemTemplate>           
                </asp:TemplateField>--%>
                    <asp:BoundField DataField="ReqNo" HeaderText="Request No." >
                </asp:BoundField> 
                <asp:BoundField DataField="Title" HeaderText="Title" >
                </asp:BoundField> 
                    <asp:BoundField DataField="RequestDate" HeaderText="Request Date" >    
                </asp:BoundField>
                    <asp:BoundField DataField="RequesterName" HeaderText="Requester Name" >    
                </asp:BoundField>                                                                                                  
                <asp:BoundField DataField="RequestStatus" HeaderText="Request Status" >
                </asp:BoundField>  
                <asp:TemplateField HeaderText="">
                    <ItemTemplate>                 
                        <asp:HyperLink ID="HyperLink1" runat="server" Text='View Details' NavigateUrl='<%# Eval("ViewLink", "{0}") %>'></asp:HyperLink>
                    </ItemTemplate> 
                    </asp:TemplateField>
            </Columns>                 
        </asp:GridView>  

   
     
   

            
     
