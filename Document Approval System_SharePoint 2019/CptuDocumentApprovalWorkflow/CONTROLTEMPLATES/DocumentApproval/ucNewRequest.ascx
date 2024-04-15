<%@ Assembly Name="$SharePoint.Project.AssemblyFullName$" %>
<%@ Assembly Name="Microsoft.Web.CommandUI, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register Tagprefix="SharePoint" Namespace="Microsoft.SharePoint.WebControls" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register Tagprefix="Utilities" Namespace="Microsoft.SharePoint.Utilities" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register Tagprefix="asp" Namespace="System.Web.UI" Assembly="System.Web.Extensions, Version=3.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" %>
<%@ Import Namespace="Microsoft.SharePoint" %> 
<%@ Register Tagprefix="WebPartPages" Namespace="Microsoft.SharePoint.WebPartPages" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ucNewRequest.ascx.cs" Inherits="CPTUDocumentApprovalWorkflow.CONTROLTEMPLATES.DocumentApproval.ucNewRequest" %>
<%@ Register Assembly="Microsoft.SharePoint, Version=12.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c"
  Namespace="Microsoft.SharePoint.WebControls" TagPrefix="SharepointControls" %>
<style type="text/css">
    .radioButtonList input {
    display:inline;
}
     .btnSubmit input[type="submit"], button {
        min-width: 12em;
        padding: 0px 10px;
        border: 1px solid #08AF39;
        background-color: #08AF39;
        margin-right: 50px;
        margin-bottom: 20px;
        font-family: "Segoe UI","Segoe",Tahoma,Helvetica,Arial,sans-serif;
        font-size: 13px;
        color: #ffffff;
        height: 35px;
        border-radius: 4px;
        font-weight: bold; 
   }
    .btnSubmit input[type="submit"]:hover{
     	background: #038D2C;
	   color: #fff;
    }

    .lbl-comment {
       color: #234595;
       font-family: ui-sans-serif;
       padding-top: 7px;
    }
    .btn-red input[type="submit"], button {
       min-width: 5.3em;
        padding: 0px 10px;
        border: 1px solid #ce0707;
        background-color: #ce0707;        
        font-family: "Segoe UI","Segoe",Tahoma,Helvetica,Arial,sans-serif;
        font-size: 13px;
        color: #ffffff;
        height: 31px;
        border-radius: 4px;
        font-weight: bold; 
   }
    .btn-red input[type="submit"]:hover{
	background: #aa0707;
	color: #fff;
    }
   
    .file-upload:hover {
        background: -webkit-gradient(linear, left top, left bottom, color-stop(0.05, #0061a7), color-stop(1, #007dc1));
        background: -moz-linear-gradient(top, #0061a7 5%, #007dc1 100%);
        background: -webkit-linear-gradient(top, #0061a7 5%, #007dc1 100%);
        background: -o-linear-gradient(top, #0061a7 5%, #007dc1 100%);
        background: -ms-linear-gradient(top, #0061a7 5%, #007dc1 100%);
        background: linear-gradient(to bottom, #0061a7 5%, #007dc1 100%);
        filter: progid:DXImageTransform.Microsoft.gradient(startColorstr='#0061a7', endColorstr='#007dc1',GradientType=0);
        background-color: #0061a7;
    }

/* The button size */
.file-upload {
    height: 30px;
}

    .file-upload, .file-upload span {
        width: 90px;
    }

        .file-upload input {
            top: 0;
            left: 0;
            margin: 0;
            font-size: 11px;
            font-weight: bold;
            /* Loses tab index in webkit if width is set to 0 */
            opacity: 0;
            filter: alpha(opacity=0);
        }

        .file-upload strong {
            font: normal 12px Tahoma,sans-serif;
            text-align: center;
            vertical-align: middle;
        }
         .small {
                width:10px;
                min-width:10px;
                max-width:10px;

            }
        .file-upload span {
            top: 0;
            left: 0;
            display: inline-block;
            /* Adjust button text vertical alignment */
            padding-top: 5px;
        }
    .table-bordered {
        margin-right: 0px;
    }
      .RadioButtonWidth label {  margin-left:3px; } 
</style>
<%--<asp:Label ID="lblMessage" runat="server" Text=""></asp:Label>--%>
<div class="header-title">Document Approval</div>
<div class="frm-content">
    <div class="container">       
          <table  class="table table-bordered table-sm form-inline" style="width:100%; table-layout: fixed;font-family: ui-sans-serif; font-size: 15px; background:#ffffff; ">                         
          <asp:Panel ID="pnlShow" Visible="false" runat="server">
            <tr>
                <td>Request No.:</td>
                <td>                                   
                    <asp:Label ID="txtRequestNo" runat="server" Text=""></asp:Label>
                </td>
             
                <td>Request Date:</td>
                <td>
                    <asp:Label ID="txtRequestDate" runat="server" Text=""></asp:Label>        
                </td>
            </tr>
           
          </asp:Panel>
            <tr>
                <td>Requester Name:</td>
                <td>                                   
                    <asp:Label ID="txtRequesterName" runat="server" Text=""></asp:Label>
                </td>
               
                <td>Department/Division:</td>
                <td>                                   
                    <asp:Label ID="txtRequesterDepartment" runat="server" Text=""></asp:Label>
                </td>
           </tr>
           <asp:Panel ID="pnlShow2" Visible="false" runat="server">
               <tr>
                  <td>Designation:</td>
                <td>
                    <asp:Label ID="txtRequesterDesignation" runat="server" Text=""></asp:Label>        
                </td>
             
               <td>Email:</td>
                <td>
                    <asp:Label ID="txtRequesterEmail" runat="server" Text=""></asp:Label>        
                </td>
            </tr>
           </asp:Panel>
             
        </table>
      
        
      <%--<div class="row">
          <div class="col-6">
            <div class="form-group">
              <label class="lbl"> Request No.</label>
                <asp:TextBox ID="txtRequestNo" CssClass="form-control form-control-sm"  runat="server" ReadOnly="true"></asp:TextBox>  
            </div>
         </div>
         <div class="col-6">
           <div class="form-group">
              <label class="lbl"> Request Date</label>
               <asp:TextBox ID="txtRequestDate" CssClass="form-control form-control-sm" runat="server" ReadOnly="true"></asp:TextBox>           
           </div>
         </div>
       </div>--%>
         
       <%--<div class="row">
            <div class="col-6">
                <div class="form-group">
                  <label class="lbl"> Requester Name</label>
                     <asp:TextBox ID="txtRequesterName" CssClass="form-control form-control-sm" runat="server" ReadOnly="true"></asp:TextBox>                    
                </div>
            </div>
            <div class="col-6">
               <div class="form-group">
                  <label class="lbl"> Email</label>
                     <asp:TextBox ID="txtRequesterEmail" CssClass="form-control form-control-sm" runat="server" ReadOnly="true"></asp:TextBox>                    
               </div>
            </div>   
      </div>
       <div class="row">
            <div class="col-6">
                <div class="form-group">
                  <label class="lbl"> Department/Division</label>
                    <asp:TextBox ID="txtRequesterDepartment" CssClass="form-control form-control-sm" runat="server" ReadOnly="true"></asp:TextBox>                    
      
                </div>
            </div>
            <div class="col-6">
               <div class="form-group">
                  <label class="lbl"> Designation</label>
                  <asp:TextBox ID="txtRequesterDesignation" CssClass="form-control form-control-sm" runat="server" ReadOnly="true"></asp:TextBox>                     
               </div>
            </div>   
      </div>--%>
       <div class="row">
            <div class="col-12">
                <div class="form-group">
                  <label class="lbl">Comment<span style="color:red;">*</span></label>
                    <asp:TextBox ID="txtComment" TextMode="MultiLine" BackColor="White" Rows="2" CssClass="form-control form-control-sm" runat="server"></asp:TextBox>    
                </div>
            </div>        
      </div>
       <div class="spacer"></div>
       <div class="row">
            <div class="col-12">
                <asp:Panel ID="pblUploadAttachment" runat="server" Visible="true">
                    <div class="form-group">
                        <div style="display:inline;float:left">
                             <%--<label class="lbl">Documents</label>--%>
                            <span><strong>Upload Documents<span style="color:red;">*</span></strong></span>
                            <asp:FileUpload ID="fileUploadControl" runat="server" /> 
                           
                        </div>
                       <div style="display:inline;float:left; margin-right:5px;" class="btn-red">
                           <asp:Button CssClass ="btn btn-sm" ID="btnAttachment" runat="server" OnClick="btnAttachment_Click" Text="UPLOAD" />
                       </div> 
                        <div style="color:darkred" >
                            <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="fileUploadControl" ErrorMessage="Please select a .docx file" ValidationExpression="^([a-zA-Z].*|[1-9].*)\.(((d|D)(o|O)(c|C)(x|X)))$"></asp:RegularExpressionValidator>
                        </div>
                         
                    </div>
                </asp:Panel>
               
                     
            </div> 
           <div class="col-6" style="margin-top:10px;">
                <asp:GridView ID="gvAttachments" BackColor="White" runat="server" AutoGenerateColumns="false" Width="395" OnRowCommand="gvAttachments_RowCommand" CssClass="table table-bordered" >
                    <Columns>
                        <asp:BoundField DataField="ID" HeaderText="ID" Visible="False" />
                        <asp:TemplateField HeaderText="Attachments">
                            <ItemTemplate>                               
                                <a href="<%#Eval("FileUrl")%>" target="_blank"> <%#Eval("FileName")%></a>
                            </ItemTemplate>
                        </asp:TemplateField>
                                       
                        <asp:TemplateField HeaderText="" ItemStyle-Width="10" >
                              <HeaderStyle CssClass="small" />
                            <ItemTemplate>                               
                            <asp:LinkButton ID="deleteAttachment" OnClientClick="return confirm('Are you sure you want to delete this?');" CommandName="deleteAttachment" CommandArgument='<%# Eval("ID") %>' runat="server"><img src="../../../_layouts/15/Content/img/delete-img.png" width="18" height="18" /></asp:LinkButton>
                            </ItemTemplate>
                         </asp:TemplateField>                                       
                    </Columns>
                </asp:GridView>  
           </div>
      </div>
         <asp:Panel ID="pnlReviewerShow" Visible="true" runat="server">      
            <div class="row" style="margin-top:18px;">         
               <div class="col-6">   
                <div style="display:inline; float:left;">
                      <div class="form-group">
                            <label class="lbl-pd">Reviewer<span style="color:red;">*</span></label>                  
                            <SharePoint:PeopleEditor ID="reviewerPeoplePicker"  runat="server" MultiSelect="false" SelectionSet="User" Width="186px"  DoPostBackOnResolve="true" />
                        </div>   
                </div>
                <div style="display:inline; float:left;" class="btn-red">
                    <label></label>     
                <asp:Button ID="btnAddReviewer" CssClass ="btn btn-sm" runat="server"  Text="ADD" OnClick="btnAddReviewer_Click" />
                </div>
               
                 <asp:GridView ID="gvReviewerList" runat="server" AutoGenerateColumns="False" OnRowCommand="gvReviewerList_RowCommand" Width="245" CssClass="table table-bordered">
                   <Columns>
                    <asp:BoundField DataField="ID" HeaderText="ID" Visible="False" />
                                                                                                            
                    <asp:BoundField DataField="Name" HeaderText="Name" >
                
                    </asp:BoundField> 

                   <asp:TemplateField HeaderText=""  ItemStyle-Width="10px" >
                       
                    <ItemTemplate>                               
                        <asp:LinkButton ID="deleteReviewer" OnClientClick="return confirm('Are you sure you want to delete this?');" CommandName="deleteReviewer" CommandArgument='<%# Eval("ID") %>' runat="server"><img src="../../../_layouts/15/Content/img/delete-img.png" width="18" height="18" /></asp:LinkButton>
                    </ItemTemplate>
                    </asp:TemplateField>
                                               
                </Columns>
                 </asp:GridView>
            </div>         
           <div class="col-6">              
              <div class="form-group">
                <label class="lbl-pd">Approver<span style="color:red;">*</span></label>                  
                <SharePoint:PeopleEditor ID="approverPeoplePicker" runat="server" MultiSelect="false" SelectionSet="User" Width="186px"  DoPostBackOnResolve="true" />
              </div>              
            </div>                   
            </div> 
          </asp:Panel>  
        
          <asp:Panel ID="pnlReviewerViewMode" Visible="false" runat="server">
               <table  class="table table-bordered table-sm form-inline" style="width:100%; table-layout: fixed;font-family: ui-sans-serif; font-size: 15px; background:#ffffff; ">                                  
               <tr>               
                 <td>                                   
                   <asp:GridView ID="gvReviewerView" runat="server" AutoGenerateColumns="False" OnRowCommand="gvReviewerList_RowCommand" CssClass="table table-bordered">
                       <Columns>                                                                                                                               
                            <asp:BoundField DataField="Name" HeaderText="Reviewer" > </asp:BoundField>                                                                                                  
                       </Columns>
                   </asp:GridView>
                </td>
             
                <td>
                    <table class="table table-bordered ">                      
                        <thead>
                            <tr>
                                 <th scope="col">Approver</th>                             
                            </tr>
                         </thead>
                        <tbody>
                           <tr>
                               <td>
                                    <asp:Label ID="lblApproverName" runat="server" Text=""></asp:Label> 
                               </td>
                           </tr>
                        </tbody>                        
                    </table>                                      
                </td>               
            </tr>               
        </table>
       </asp:Panel>
       
       <div class="spacer"></div>
      
       
       <div class="row">
          <asp:Panel ID="pnlApprovalHistory" runat="server" Visible ="false">
          <div class="col-12">
              <div style="text-align:center; border-bottom: 2px solid #efc611;">
                  <h5 style="color:#080aac;">Approval History</h5>
              </div>
             
              <div style="margin-top:25px;"> 
               <asp:GridView ID="gvApprovalHistory" runat="server" AutoGenerateColumns="False" CssClass="table table-bordered">
               <Columns>
                <asp:BoundField DataField="ID" HeaderText="ID" Visible="False" />                                                                                                            
                <asp:BoundField DataField="ApprovalLevel" HeaderText="Approval Level" > </asp:BoundField> 
                <asp:BoundField DataField="Approval" HeaderText="Action" > </asp:BoundField>
                <asp:BoundField DataField="ApprovedBy" HeaderText="Approved By" > </asp:BoundField>
                <asp:BoundField DataField="ApprovalDate" HeaderText="Approval Date" > </asp:BoundField>   
                <asp:BoundField DataField="Comment" HeaderText="Comment" > </asp:BoundField>    
            </Columns>
             </asp:GridView>
              </div>
            
        </div>   
          </asp:Panel>
                       
     </div>
        <div class="spacer"></div>
        <div class="row">
            <div class="col-12 btnSubmit">
                <asp:Button ID="btnSubmit" CssClass="btn btn-d btn-sm" OnClick="btnSubmit_Click1" runat="server" Text="Submit for Approval" />
            </div>        
      </div>

       <div class="row">
           <asp:Panel ID="pnlReviewer" Visible="false" runat="server">
               <div class="panel-header">
                        <p>Reviewer Panel</p> 
                   </div>
               <div class="approval-panel">
               <div class="col-12">
                 <div class="row">
                     <div class="col-8">
                        <%-- <asp:DropDownList ID="ddlReviewerApproval" runat="server">
                            <asp:ListItem Value="Approved" Text="Accepted"></asp:ListItem>  
                            <asp:ListItem Value="Returned" Text="Returned"></asp:ListItem>
                            <asp:ListItem Value="Rejected" Text="Rejected"></asp:ListItem>
                         </asp:DropDownList>--%>
                        
                         <asp:RadioButtonList ID="ddlReviewerApproval" CssClass="RadioButtonWidth"  RepeatColumns="3" CellPadding="3" CellSpacing="2" RepeatDirection="Horizontal" runat="server" Width="250px" >
                                <asp:ListItem  Text="Approved" Value="Approved" />
                                <asp:ListItem Text="Returned" Value="Returned" />
                                <asp:ListItem Text="Rejected" Value="Rejected" />
                            </asp:RadioButtonList>
                         
                     </div>                   
                 </div>  
                   <div class="row">
                     <div class="col-12">
                         <div class="form-group">
                            <label class="lbl-comment">Comment<span style="color:red;">*</span></label>
                            <asp:TextBox ID="txtReviewerComment" TextMode="MultiLine" Rows="2" CssClass="form-control form-control-sm" runat="server"></asp:TextBox>    
                        </div>
                     </div>                   
                 </div> 
                 <div class="spacer"></div>
                   <div class="row">
                       <div class="col-12">
                           <asp:Button ID="btnSubmitApproval" CssClass="btn btn-primary btn-d  btn-sm" OnClick="btnSubmitApproval_Click" runat="server" Text="Submit" />
                       </div>
                   </div>
                 </div> 
               </div>
               
           </asp:Panel>                   
      </div>
       <div class="row">
           <asp:Panel ID="pnlApprover" Visible="false" runat="server">
               <div class="panel-header">
                        <p>Approver Panel</p> 
                </div>
               <div class="approval-panel">
                   
                       <div class="row">
                         <div class="col-8">
                       <%--      <asp:DropDownList ID="ddlApproverApproval" runat="server">
                                <asp:ListItem Value="Approved" Text="Approved"></asp:ListItem>  
                                <asp:ListItem Value="Returned" Text="Returned"></asp:ListItem>
                                <asp:ListItem Value="Rejected" Text="Rejected"></asp:ListItem>
                             </asp:DropDownList>--%>
                             
                             <asp:RadioButtonList ID="ddlApproverApproval" CssClass="RadioButtonWidth" RepeatColumns="3" RepeatDirection="Horizontal"  Width="250px" runat="server" >
                                <asp:ListItem Text="Approved" Value="Approved" />
                                <asp:ListItem Text="Returned" Value="Returned" />
                                <asp:ListItem Text="Rejected" Value="Rejected" />
                            </asp:RadioButtonList>
                            
                         </div>                   
                     </div>  
                       <div class="row">
                         <div class="col-12">
                             <div class="form-group">
                                <label class="lbl-comment">Comment<span style="color:red;">*</span></label>
                                <asp:TextBox ID="txtApproverComment" TextMode="MultiLine" Rows="2" CssClass="form-control form-control-sm" runat="server"></asp:TextBox>    
                            </div>
                         </div>                   
                     </div> 
                   
                   <div class="spacer"></div>
                    <div class="row">
                       <div class="col-12">
                            <asp:Button ID="Button1" CssClass="btn btn-primary btn-d  btn-sm" OnClick="btnSubmitApproval_Click" runat="server" Text="Submit" />
                       </div>
                   </div>
               </div>
              
             
           </asp:Panel>                   
      </div>
    </div>
 </div>


<script type="text/javascript">

    function RequiredComment() {
        swal({
            title: 'Required !',
            text: 'Please Enter Comment !!',
            type: 'info'
        });
    }

    function RequiredAttachment() {
        swal({
            title: 'Required !',
            text: 'Please Add Attachments !!',
            type: 'info'
        });
    }

    function RequiredReviewer() {
        swal({
            title: 'Required !',
            text: 'Please Add Reviewer !!',
            type: 'info'
        });
    }

    function RequiredApprover() {
        swal({
            title: 'Required !',
            text: 'Please Add Approver !!',
            type: 'info'
        });
    }
    function ExistsReviewer() {
        swal({
            title: 'Validation !',
            text: 'Same reviewer already exists !!',
            type: 'warning'
        });
    }
    
    function AlertSubmittedSuccessfully() {
        //swal({
        //    title: 'Succcessfully Submitted !!',
        //    text: '',
        //    type: 'success'
        //});

        swal({
            title: 'Successfully Submitted',
            type: 'success'
        },
            function () {
                document.location.href = "/sites/dms/_layouts/15/DocumentApproval/MyRequest.aspx";
            });
    }
    function AlertApprovalSubmittedSuccessfully() {      
        swal({
            title: 'Successfully Submitted',
            type: 'success'
        },
        function () {
            document.location.href = "/sites/dms/_layouts/15/DocumentApproval/MyPendingTask.aspx";
        });
    }

    function RequiredApprovalStatus() {
        swal({
            title: 'Required !',
            text: 'Please Select Approval Status !!',
            type: 'info'
        });
    }

    function RequiredComment() {
        swal({
            title: 'Required !',
            text: 'Please Enter Comment !!',
            type: 'info'
        });
    }
</script>