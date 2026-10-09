using System;
using System.Collections;
using GeneXus.Utils;
using GeneXus.Resources;
using GeneXus.Application;
using GeneXus.Metadata;
using GeneXus.Cryptography;
using System.Data;
using GeneXus.Data;
using com.genexus;
using GeneXus.Data.ADO;
using GeneXus.Data.NTier;
using GeneXus.Data.NTier.ADO;
using GeneXus.WebControls;
using GeneXus.Http;
using GeneXus.XML;
using GeneXus.Search;
using GeneXus.Encryption;
using GeneXus.Http.Client;
using System.Xml.Serialization;
using System.Runtime.Serialization;
using System.Text.Json.Serialization;
namespace GeneXus.Programs.wallet.registered {
   public class smartgroups : GXDataArea
   {
      public smartgroups( )
      {
         context = new GxContext(  );
         DataStoreUtil.LoadDataStores( context);
         dsDefault = context.GetDataStore("Default");
         IsMain = true;
         context.SetDefaultTheme("GeneXusUnanimo.UnanimoWeb", true);
      }

      public smartgroups( IGxContext context )
      {
         this.context = context;
         IsMain = false;
         dsDefault = context.GetDataStore("Default");
      }

      public void execute( )
      {
         ExecuteImpl();
      }

      protected override void ExecutePrivate( )
      {
         isStatic = false;
         webExecute();
      }

      protected override void createObjects( )
      {
         cmbavCtlgrouptype = new GXCombobox();
         chkavCtlamigroupowner = new GXCheckbox();
         chkavCtlisactive = new GXCheckbox();
      }

      protected void INITWEB( )
      {
         initialize_properties( ) ;
         if ( nGotPars == 0 )
         {
            entryPointCalled = false;
            gxfirstwebparm = GetNextPar( );
            gxfirstwebparm_bkp = gxfirstwebparm;
            gxfirstwebparm = DecryptAjaxCall( gxfirstwebparm);
            toggleJsOutput = isJsOutputEnabled( );
            if ( context.isSpaRequest( ) )
            {
               disableJsOutput();
            }
            if ( StringUtil.StrCmp(gxfirstwebparm, "dyncall") == 0 )
            {
               setAjaxCallMode();
               if ( ! IsValidAjaxCall( true) )
               {
                  GxWebError = 1;
                  return  ;
               }
               dyncall( GetNextPar( )) ;
               return  ;
            }
            else if ( StringUtil.StrCmp(gxfirstwebparm, "gxajaxEvt") == 0 )
            {
               setAjaxEventMode();
               if ( ! IsValidAjaxCall( true) )
               {
                  GxWebError = 1;
                  return  ;
               }
               gxfirstwebparm = GetNextPar( );
            }
            else if ( StringUtil.StrCmp(gxfirstwebparm, "gxfullajaxEvt") == 0 )
            {
               if ( ! IsValidAjaxCall( true) )
               {
                  GxWebError = 1;
                  return  ;
               }
               gxfirstwebparm = GetNextPar( );
            }
            else if ( StringUtil.StrCmp(gxfirstwebparm, "gxajaxNewRow_"+"Gridgroup") == 0 )
            {
               gxnrGridgroup_newrow_invoke( ) ;
               return  ;
            }
            else if ( StringUtil.StrCmp(gxfirstwebparm, "gxajaxGridRefresh_"+"Gridgroup") == 0 )
            {
               gxgrGridgroup_refresh_invoke( ) ;
               return  ;
            }
            else
            {
               if ( ! IsValidAjaxCall( false) )
               {
                  GxWebError = 1;
                  return  ;
               }
               gxfirstwebparm = gxfirstwebparm_bkp;
            }
            if ( toggleJsOutput )
            {
               if ( context.isSpaRequest( ) )
               {
                  enableJsOutput();
               }
            }
         }
         if ( ! context.IsLocalStorageSupported( ) )
         {
            context.PushCurrentUrl();
         }
      }

      protected void gxnrGridgroup_newrow_invoke( )
      {
         nRC_GXsfl_11 = (int)(Math.Round(NumberUtil.Val( GetPar( "nRC_GXsfl_11"), "."), 18, MidpointRounding.ToEven));
         nGXsfl_11_idx = (int)(Math.Round(NumberUtil.Val( GetPar( "nGXsfl_11_idx"), "."), 18, MidpointRounding.ToEven));
         sGXsfl_11_idx = GetPar( "sGXsfl_11_idx");
         setAjaxCallMode();
         if ( ! IsValidAjaxCall( true) )
         {
            GxWebError = 1;
            return  ;
         }
         gxnrGridgroup_newrow( ) ;
         /* End function gxnrGridgroup_newrow_invoke */
      }

      protected void gxgrGridgroup_refresh_invoke( )
      {
         ajax_req_read_hidden_sdt(GetNextPar( ), AV38groupList);
         setAjaxCallMode();
         if ( ! IsValidAjaxCall( true) )
         {
            GxWebError = 1;
            return  ;
         }
         gxgrGridgroup_refresh( AV38groupList) ;
         AddString( context.getJSONResponse( )) ;
         /* End function gxgrGridgroup_refresh_invoke */
      }

      public override void webExecute( )
      {
         createObjects();
         initialize();
         INITWEB( ) ;
         if ( ! isAjaxCallMode( ) )
         {
            MasterPageObj = (GXMasterPage) ClassLoader.GetInstance("general.ui.masterunanimosidebar", "GeneXus.Programs.general.ui.masterunanimosidebar", new Object[] {context});
            MasterPageObj.setDataArea(this,false);
            ValidateSpaRequest();
            MasterPageObj.webExecute();
            if ( ( GxWebError == 0 ) && context.isAjaxRequest( ) )
            {
               enableOutput();
               if ( ! context.isAjaxRequest( ) )
               {
                  context.GX_webresponse.AppendHeader("Cache-Control", "no-store");
               }
               if ( ! context.WillRedirect( ) )
               {
                  AddString( context.getJSONResponse( )) ;
               }
               else
               {
                  if ( context.isAjaxRequest( ) )
                  {
                     disableOutput();
                  }
                  RenderHtmlHeaders( ) ;
                  context.Redirect( context.wjLoc );
                  context.DispatchAjaxCommands();
               }
            }
         }
         cleanup();
      }

      public override short ExecuteStartEvent( )
      {
         PA1F2( ) ;
         gxajaxcallmode = (short)((isAjaxCallMode( ) ? 1 : 0));
         if ( ( gxajaxcallmode == 0 ) && ( GxWebError == 0 ) )
         {
            START1F2( ) ;
         }
         return gxajaxcallmode ;
      }

      public override void RenderHtmlHeaders( )
      {
         GxWebStd.gx_html_headers( context, 0, "", "", Form.Meta, Form.Metaequiv, true);
      }

      public override void RenderHtmlOpenForm( )
      {
         if ( context.isSpaRequest( ) )
         {
            enableOutput();
         }
         context.WriteHtmlText( "<title>") ;
         context.SendWebValue( Form.Caption) ;
         context.WriteHtmlTextNl( "</title>") ;
         if ( context.isSpaRequest( ) )
         {
            disableOutput();
         }
         if ( StringUtil.Len( sDynURL) > 0 )
         {
            context.WriteHtmlText( "<BASE href=\""+sDynURL+"\" />") ;
         }
         define_styles( ) ;
         if ( nGXWrapped != 1 )
         {
            MasterPageObj.master_styles();
         }
         CloseStyles();
         if ( ( ( context.GetBrowserType( ) == 1 ) || ( context.GetBrowserType( ) == 5 ) ) && ( StringUtil.StrCmp(context.GetBrowserVersion( ), "7.0") == 0 ) )
         {
            context.AddJavascriptSource("json2.js", "?"+context.GetBuildNumber( 1550520), false, true, false);
         }
         context.AddJavascriptSource("jquery.js", "?"+context.GetBuildNumber( 1550520), false, true, false);
         context.AddJavascriptSource("gxgral.js", "?"+context.GetBuildNumber( 1550520), false, true, false);
         context.AddJavascriptSource("gxcfg.js", "?"+GetCacheInvalidationToken( ), false, true, false);
         if ( context.isSpaRequest( ) )
         {
            enableOutput();
         }
         context.WriteHtmlText( Form.Headerrawhtml) ;
         context.CloseHtmlHeader();
         if ( context.isSpaRequest( ) )
         {
            disableOutput();
         }
         FormProcess = " data-HasEnter=\"false\" data-Skiponenter=\"false\"";
         context.WriteHtmlText( "<body ") ;
         if ( StringUtil.StrCmp(context.GetLanguageProperty( "rtl"), "true") == 0 )
         {
            context.WriteHtmlText( " dir=\"rtl\" ") ;
         }
         bodyStyle = "" + "background-color:" + context.BuildHTMLColor( Form.Backcolor) + ";color:" + context.BuildHTMLColor( Form.Textcolor) + ";";
         if ( nGXWrapped == 0 )
         {
            bodyStyle += "-moz-opacity:0;opacity:0;";
         }
         if ( ! ( String.IsNullOrEmpty(StringUtil.RTrim( Form.Background)) ) )
         {
            bodyStyle += " background-image:url(" + context.convertURL( Form.Background) + ")";
         }
         context.WriteHtmlText( " "+"class=\"form-horizontal Form\""+" "+ "style='"+bodyStyle+"'") ;
         context.WriteHtmlText( FormProcess+">") ;
         context.skipLines(1);
         context.WriteHtmlTextNl( "<form id=\"MAINFORM\" autocomplete=\"off\" name=\"MAINFORM\" method=\"post\" tabindex=-1  class=\"form-horizontal Form\" data-gx-class=\"form-horizontal Form\" novalidate action=\""+formatLink("wallet.registered.smartgroups") +"\">") ;
         GxWebStd.gx_hidden_field( context, "_EventName", "");
         GxWebStd.gx_hidden_field( context, "_EventGridId", "");
         GxWebStd.gx_hidden_field( context, "_EventRowId", "");
         context.WriteHtmlText( "<div style=\"height:0;overflow:hidden\"><input type=\"submit\" title=\"submit\"  disabled></div>") ;
         AssignProp("", false, "FORM", "Class", "form-horizontal Form", true);
         toggleJsOutput = isJsOutputEnabled( );
         if ( context.isSpaRequest( ) )
         {
            disableJsOutput();
         }
      }

      protected void send_integrity_footer_hashes( )
      {
         GXKey = Decrypt64( context.GetCookie( "GX_SESSION_ID"), Crypto.GetServerKey( ));
      }

      protected void SendCloseFormHiddens( )
      {
         /* Send hidden variables. */
         /* Send saved values. */
         send_integrity_footer_hashes( ) ;
         if ( context.isAjaxRequest( ) )
         {
            context.httpAjaxContext.ajax_rsp_assign_sdt_attri("", false, "Grouplist", AV38groupList);
         }
         else
         {
            context.httpAjaxContext.ajax_rsp_assign_hidden_sdt("Grouplist", AV38groupList);
         }
         GxWebStd.gx_hidden_field( context, "nRC_GXsfl_11", StringUtil.LTrim( StringUtil.NToC( (decimal)(nRC_GXsfl_11), 8, 0, ".", "")));
         if ( context.isAjaxRequest( ) )
         {
            context.httpAjaxContext.ajax_rsp_assign_sdt_attri("", false, "vGROUPLIST", AV38groupList);
         }
         else
         {
            context.httpAjaxContext.ajax_rsp_assign_hidden_sdt("vGROUPLIST", AV38groupList);
         }
         GxWebStd.gx_boolean_hidden_field( context, "vUSERRESPONSE", AV11UserResponse);
         GxWebStd.gx_hidden_field( context, "vSELECTEDGROUPID", AV39selectedGroupId.ToString());
      }

      public override void RenderHtmlCloseForm( )
      {
         SendCloseFormHiddens( ) ;
         GxWebStd.gx_hidden_field( context, "GX_FocusControl", GX_FocusControl);
         SendAjaxEncryptionKey();
         SendSecurityToken((string)(sPrefix));
         SendComponentObjects();
         SendServerCommands();
         SendState();
         if ( context.isSpaRequest( ) )
         {
            disableOutput();
         }
         context.WriteHtmlTextNl( "</form>") ;
         if ( context.isSpaRequest( ) )
         {
            enableOutput();
         }
         include_jscripts( ) ;
      }

      public override void RenderHtmlContent( )
      {
         gxajaxcallmode = (short)((isAjaxCallMode( ) ? 1 : 0));
         if ( ( gxajaxcallmode == 0 ) && ( GxWebError == 0 ) )
         {
            context.WriteHtmlText( "<div") ;
            GxWebStd.ClassAttribute( context, "gx-ct-body"+" "+(String.IsNullOrEmpty(StringUtil.RTrim( Form.Class)) ? "form-horizontal Form" : Form.Class)+"-fx");
            context.WriteHtmlText( ">") ;
            WE1F2( ) ;
            context.WriteHtmlText( "</div>") ;
         }
      }

      public override void DispatchEvents( )
      {
         EVT1F2( ) ;
      }

      public override bool HasEnterEvent( )
      {
         return false ;
      }

      public override GXWebForm GetForm( )
      {
         return Form ;
      }

      public override string GetSelfLink( )
      {
         return formatLink("wallet.registered.smartgroups")  ;
      }

      public override string GetPgmname( )
      {
         return "Wallet.registered.SmartGroups" ;
      }

      public override string GetPgmdesc( )
      {
         return "Smart Groups" ;
      }

      protected void WB1F0( )
      {
         if ( context.isAjaxRequest( ) )
         {
            disableOutput();
         }
         if ( ! wbLoad )
         {
            if ( nGXWrapped == 1 )
            {
               RenderHtmlHeaders( ) ;
               RenderHtmlOpenForm( ) ;
            }
            GxWebStd.gx_msg_list( context, "", context.GX_msglist.DisplayMode, "", "", "", "false");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "Section", "start", "top", " "+"data-gx-base-lib=\"none\""+" "+"data-abstract-form"+" ", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, divMaintable_Internalname, 1, 0, "px", 0, "px", "Table", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "row", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "col-xs-12 col-sm-6", "start", "top", "", "", "div");
            TempTags = "  onfocus=\"gx.evt.onfocus(this, 6,'',false,'',0)\"";
            ClassString = "Button";
            StyleString = "";
            GxWebStd.gx_button_ctrl( context, bttRecovergroupsfromserver_Internalname, "gx.evt.setGridEvt("+StringUtil.Str( (decimal)(11), 2, 0)+","+"null"+");", "Recover groups from server", bttRecovergroupsfromserver_Jsonclick, 5, "Recover groups from server", "", StyleString, ClassString, 1, 1, "standard", "'"+""+"'"+",false,"+"'"+"E\\'RECOVER GROUPS FROM SERVER\\'."+"'", TempTags, "", context.GetButtonType( ), "HLP_Wallet/registered/SmartGroups.htm");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "col-xs-12 col-sm-6", "end", "top", "", "", "div");
            TempTags = "  onfocus=\"gx.evt.onfocus(this, 8,'',false,'',0)\"";
            ClassString = "Button";
            StyleString = "";
            GxWebStd.gx_button_ctrl( context, bttCreategroup_Internalname, "gx.evt.setGridEvt("+StringUtil.Str( (decimal)(11), 2, 0)+","+"null"+");", "Create Group", bttCreategroup_Jsonclick, 7, "Create Group", "", StyleString, ClassString, 1, 1, "standard", "'"+""+"'"+",false,"+"'"+"e111f1_client"+"'", TempTags, "", 2, "HLP_Wallet/registered/SmartGroups.htm");
            GxWebStd.gx_div_end( context, "end", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "row", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "col-xs-12", "start", "top", "", "", "div");
            /*  Grid Control  */
            GridgroupContainer.SetWrapped(nGXWrapped);
            StartGridControl11( ) ;
         }
         if ( wbEnd == 11 )
         {
            wbEnd = 0;
            nRC_GXsfl_11 = (int)(nGXsfl_11_idx-1);
            if ( GridgroupContainer.GetWrapped() == 1 )
            {
               context.WriteHtmlText( "</table>") ;
               context.WriteHtmlText( "</div>") ;
            }
            else
            {
               AV40GXV1 = nGXsfl_11_idx;
               sStyleString = "";
               context.WriteHtmlText( "<div id=\""+"GridgroupContainer"+"Div\" "+sStyleString+">"+"</div>") ;
               context.httpAjaxContext.ajax_rsp_assign_grid("_"+"Gridgroup", GridgroupContainer, subGridgroup_Internalname);
               if ( ! context.isAjaxRequest( ) && ! context.isSpaRequest( ) )
               {
                  GxWebStd.gx_hidden_field( context, "GridgroupContainerData", GridgroupContainer.ToJavascriptSource());
               }
               if ( context.isAjaxRequest( ) || context.isSpaRequest( ) )
               {
                  GxWebStd.gx_hidden_field( context, "GridgroupContainerData"+"V", GridgroupContainer.GridValuesHidden());
               }
               else
               {
                  context.WriteHtmlText( "<input type=\"hidden\" "+"name=\""+"GridgroupContainerData"+"V"+"\" value='"+GridgroupContainer.GridValuesHidden()+"'/>") ;
               }
            }
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
         }
         if ( wbEnd == 11 )
         {
            wbEnd = 0;
            if ( isFullAjaxMode( ) )
            {
               if ( GridgroupContainer.GetWrapped() == 1 )
               {
                  context.WriteHtmlText( "</table>") ;
                  context.WriteHtmlText( "</div>") ;
               }
               else
               {
                  AV40GXV1 = nGXsfl_11_idx;
                  sStyleString = "";
                  context.WriteHtmlText( "<div id=\""+"GridgroupContainer"+"Div\" "+sStyleString+">"+"</div>") ;
                  context.httpAjaxContext.ajax_rsp_assign_grid("_"+"Gridgroup", GridgroupContainer, subGridgroup_Internalname);
                  if ( ! context.isAjaxRequest( ) && ! context.isSpaRequest( ) )
                  {
                     GxWebStd.gx_hidden_field( context, "GridgroupContainerData", GridgroupContainer.ToJavascriptSource());
                  }
                  if ( context.isAjaxRequest( ) || context.isSpaRequest( ) )
                  {
                     GxWebStd.gx_hidden_field( context, "GridgroupContainerData"+"V", GridgroupContainer.GridValuesHidden());
                  }
                  else
                  {
                     context.WriteHtmlText( "<input type=\"hidden\" "+"name=\""+"GridgroupContainerData"+"V"+"\" value='"+GridgroupContainer.GridValuesHidden()+"'/>") ;
                  }
               }
            }
         }
         wbLoad = true;
      }

      protected void START1F2( )
      {
         wbLoad = false;
         wbEnd = 0;
         wbStart = 0;
         if ( ! context.isSpaRequest( ) )
         {
            if ( context.ExposeMetadata( ) )
            {
               Form.Meta.addItem("generator", "GeneXus .NET 18_0_16-189595", 0) ;
            }
         }
         Form.Meta.addItem("description", "Smart Groups", 0) ;
         context.wjLoc = "";
         context.nUserReturn = 0;
         context.wbHandled = 0;
         if ( StringUtil.StrCmp(context.GetRequestMethod( ), "POST") == 0 )
         {
         }
         wbErr = false;
         STRUP1F0( ) ;
      }

      protected void WS1F2( )
      {
         START1F2( ) ;
         EVT1F2( ) ;
      }

      protected void EVT1F2( )
      {
         if ( StringUtil.StrCmp(context.GetRequestMethod( ), "POST") == 0 )
         {
            if ( ! context.WillRedirect( ) && ( context.nUserReturn != 1 ) && ! wbErr )
            {
               /* Read Web Panel buttons. */
               sEvt = cgiGet( "_EventName");
               EvtGridId = cgiGet( "_EventGridId");
               EvtRowId = cgiGet( "_EventRowId");
               if ( StringUtil.Len( sEvt) > 0 )
               {
                  sEvtType = StringUtil.Left( sEvt, 1);
                  sEvt = StringUtil.Right( sEvt, (short)(StringUtil.Len( sEvt)-1));
                  if ( StringUtil.StrCmp(sEvtType, "M") != 0 )
                  {
                     if ( StringUtil.StrCmp(sEvtType, "E") == 0 )
                     {
                        sEvtType = StringUtil.Right( sEvt, 1);
                        if ( StringUtil.StrCmp(sEvtType, ".") == 0 )
                        {
                           sEvt = StringUtil.Left( sEvt, (short)(StringUtil.Len( sEvt)-1));
                           if ( StringUtil.StrCmp(sEvt, "RFR") == 0 )
                           {
                              context.wbHandled = 1;
                              dynload_actions( ) ;
                           }
                           else if ( StringUtil.StrCmp(sEvt, "'RECOVER GROUPS FROM SERVER'") == 0 )
                           {
                              context.wbHandled = 1;
                              dynload_actions( ) ;
                              /* Execute user event: 'Recover groups from server' */
                              E121F2 ();
                           }
                           else if ( StringUtil.StrCmp(sEvt, "GX.EXTENSIONS.WEB.DIALOGS.ONCONFIRMCLOSED") == 0 )
                           {
                              context.wbHandled = 1;
                              dynload_actions( ) ;
                              E131F2 ();
                           }
                           else if ( StringUtil.StrCmp(sEvt, "LSCR") == 0 )
                           {
                              context.wbHandled = 1;
                              dynload_actions( ) ;
                              dynload_actions( ) ;
                           }
                        }
                        else
                        {
                           sEvtType = StringUtil.Right( sEvt, 4);
                           sEvt = StringUtil.Left( sEvt, (short)(StringUtil.Len( sEvt)-4));
                           if ( ( StringUtil.StrCmp(StringUtil.Left( sEvt, 5), "START") == 0 ) || ( StringUtil.StrCmp(StringUtil.Left( sEvt, 13), "'GO TO GROUP'") == 0 ) || ( StringUtil.StrCmp(StringUtil.Left( sEvt, 14), "'DELETE GROUP'") == 0 ) || ( StringUtil.StrCmp(StringUtil.Left( sEvt, 14), "GRIDGROUP.LOAD") == 0 ) || ( StringUtil.StrCmp(StringUtil.Left( sEvt, 20), "'DECLINE INVITATION'") == 0 ) || ( StringUtil.StrCmp(StringUtil.Left( sEvt, 19), "'ACCEPT INVITATION'") == 0 ) || ( StringUtil.StrCmp(StringUtil.Left( sEvt, 5), "ENTER") == 0 ) || ( StringUtil.StrCmp(StringUtil.Left( sEvt, 6), "CANCEL") == 0 ) || ( StringUtil.StrCmp(StringUtil.Left( sEvt, 13), "'GO TO GROUP'") == 0 ) || ( StringUtil.StrCmp(StringUtil.Left( sEvt, 19), "'ACCEPT INVITATION'") == 0 ) || ( StringUtil.StrCmp(StringUtil.Left( sEvt, 20), "'DECLINE INVITATION'") == 0 ) || ( StringUtil.StrCmp(StringUtil.Left( sEvt, 14), "'DELETE GROUP'") == 0 ) || ( StringUtil.StrCmp(StringUtil.Left( sEvt, 14), "'DELETE GROUP'") == 0 ) )
                           {
                              nGXsfl_11_idx = (int)(Math.Round(NumberUtil.Val( sEvtType, "."), 18, MidpointRounding.ToEven));
                              sGXsfl_11_idx = StringUtil.PadL( StringUtil.LTrimStr( (decimal)(nGXsfl_11_idx), 4, 0), 4, "0");
                              SubsflControlProps_112( ) ;
                              AV40GXV1 = nGXsfl_11_idx;
                              if ( ( AV38groupList.Count >= AV40GXV1 ) && ( AV40GXV1 > 0 ) )
                              {
                                 AV38groupList.CurrentItem = ((GeneXus.Programs.wallet.registered.SdtGroupListItem)AV38groupList.Item(AV40GXV1));
                                 AV13acceptInvitation = cgiGet( edtavAcceptinvitation_Internalname);
                                 AssignAttri("", false, edtavAcceptinvitation_Internalname, AV13acceptInvitation);
                                 AV14declineInvitation = cgiGet( edtavDeclineinvitation_Internalname);
                                 AssignAttri("", false, edtavDeclineinvitation_Internalname, AV14declineInvitation);
                                 AV10delete = cgiGet( edtavDelete_Internalname);
                                 AssignAttri("", false, edtavDelete_Internalname, AV10delete);
                                 AV36deleteImage = cgiGet( edtavDeleteimage_Internalname);
                                 AssignProp("", false, edtavDeleteimage_Internalname, "Bitmap", (String.IsNullOrEmpty(StringUtil.RTrim( AV36deleteImage)) ? AV46Deleteimage_GXI : context.convertURL( context.PathToRelativeUrl( AV36deleteImage))), !bGXsfl_11_Refreshing);
                                 AssignProp("", false, edtavDeleteimage_Internalname, "SrcSet", context.GetImageSrcSet( AV36deleteImage), true);
                              }
                              sEvtType = StringUtil.Right( sEvt, 1);
                              if ( StringUtil.StrCmp(sEvtType, ".") == 0 )
                              {
                                 sEvt = StringUtil.Left( sEvt, (short)(StringUtil.Len( sEvt)-1));
                                 if ( StringUtil.StrCmp(sEvt, "START") == 0 )
                                 {
                                    context.wbHandled = 1;
                                    dynload_actions( ) ;
                                    /* Execute user event: Start */
                                    E141F2 ();
                                 }
                                 else if ( StringUtil.StrCmp(sEvt, "'GO TO GROUP'") == 0 )
                                 {
                                    context.wbHandled = 1;
                                    dynload_actions( ) ;
                                    /* Execute user event: 'go to group' */
                                    E151F2 ();
                                 }
                                 else if ( StringUtil.StrCmp(sEvt, "'DELETE GROUP'") == 0 )
                                 {
                                    context.wbHandled = 1;
                                    dynload_actions( ) ;
                                    /* Execute user event: 'Delete Group' */
                                    E161F2 ();
                                 }
                                 else if ( StringUtil.StrCmp(sEvt, "GRIDGROUP.LOAD") == 0 )
                                 {
                                    context.wbHandled = 1;
                                    dynload_actions( ) ;
                                    /* Execute user event: Gridgroup.Load */
                                    E171F2 ();
                                 }
                                 else if ( StringUtil.StrCmp(sEvt, "'DECLINE INVITATION'") == 0 )
                                 {
                                    context.wbHandled = 1;
                                    dynload_actions( ) ;
                                    /* Execute user event: 'Decline Invitation' */
                                    E181F2 ();
                                 }
                                 else if ( StringUtil.StrCmp(sEvt, "'ACCEPT INVITATION'") == 0 )
                                 {
                                    context.wbHandled = 1;
                                    dynload_actions( ) ;
                                    /* Execute user event: 'Accept Invitation' */
                                    E191F2 ();
                                 }
                                 else if ( StringUtil.StrCmp(sEvt, "ENTER") == 0 )
                                 {
                                    context.wbHandled = 1;
                                    if ( ! wbErr )
                                    {
                                       Rfr0gs = false;
                                       if ( ! Rfr0gs )
                                       {
                                       }
                                       dynload_actions( ) ;
                                    }
                                    /* No code required for Cancel button. It is implemented as the Reset button. */
                                 }
                                 else if ( StringUtil.StrCmp(sEvt, "LSCR") == 0 )
                                 {
                                    context.wbHandled = 1;
                                    dynload_actions( ) ;
                                 }
                              }
                              else
                              {
                              }
                           }
                        }
                     }
                     context.wbHandled = 1;
                  }
               }
            }
         }
      }

      protected void WE1F2( )
      {
         if ( ! GxWebStd.gx_redirect( context) )
         {
            Rfr0gs = true;
            Refresh( ) ;
            if ( ! GxWebStd.gx_redirect( context) )
            {
               if ( nGXWrapped == 1 )
               {
                  RenderHtmlCloseForm( ) ;
               }
            }
         }
      }

      protected void PA1F2( )
      {
         if ( nDonePA == 0 )
         {
            if ( String.IsNullOrEmpty(StringUtil.RTrim( context.GetCookie( "GX_SESSION_ID"))) )
            {
               gxcookieaux = context.SetCookie( "GX_SESSION_ID", Encrypt64( Crypto.GetEncryptionKey( ), Crypto.GetServerKey( )), "", (DateTime)(DateTime.MinValue), "", (short)(context.GetHttpSecure( )));
            }
            GXKey = Decrypt64( context.GetCookie( "GX_SESSION_ID"), Crypto.GetServerKey( ));
            toggleJsOutput = isJsOutputEnabled( );
            if ( context.isSpaRequest( ) )
            {
               disableJsOutput();
            }
            init_web_controls( ) ;
            if ( toggleJsOutput )
            {
               if ( context.isSpaRequest( ) )
               {
                  enableJsOutput();
               }
            }
            if ( ! context.isAjaxRequest( ) )
            {
            }
            nDonePA = 1;
         }
      }

      protected void dynload_actions( )
      {
         /* End function dynload_actions */
      }

      protected void gxnrGridgroup_newrow( )
      {
         GxWebStd.set_html_headers( context, 0, "", "");
         SubsflControlProps_112( ) ;
         while ( nGXsfl_11_idx <= nRC_GXsfl_11 )
         {
            sendrow_112( ) ;
            nGXsfl_11_idx = ((subGridgroup_Islastpage==1)&&(nGXsfl_11_idx+1>subGridgroup_fnc_Recordsperpage( )) ? 1 : nGXsfl_11_idx+1);
            sGXsfl_11_idx = StringUtil.PadL( StringUtil.LTrimStr( (decimal)(nGXsfl_11_idx), 4, 0), 4, "0");
            SubsflControlProps_112( ) ;
         }
         AddString( context.httpAjaxContext.getJSONContainerResponse( GridgroupContainer)) ;
         /* End function gxnrGridgroup_newrow */
      }

      protected void gxgrGridgroup_refresh( GXBaseCollection<GeneXus.Programs.wallet.registered.SdtGroupListItem> AV38groupList )
      {
         initialize_formulas( ) ;
         GxWebStd.set_html_headers( context, 0, "", "");
         GRIDGROUP_nCurrentRecord = 0;
         RF1F2( ) ;
         GXKey = Decrypt64( context.GetCookie( "GX_SESSION_ID"), Crypto.GetServerKey( ));
         send_integrity_footer_hashes( ) ;
         GXKey = Decrypt64( context.GetCookie( "GX_SESSION_ID"), Crypto.GetServerKey( ));
         /* End function gxgrGridgroup_refresh */
      }

      protected void send_integrity_hashes( )
      {
      }

      protected void clear_multi_value_controls( )
      {
         if ( context.isAjaxRequest( ) )
         {
            dynload_actions( ) ;
            before_start_formulas( ) ;
         }
      }

      protected void fix_multi_value_controls( )
      {
      }

      public void Refresh( )
      {
         send_integrity_hashes( ) ;
         RF1F2( ) ;
         if ( isFullAjaxMode( ) )
         {
            send_integrity_footer_hashes( ) ;
         }
      }

      protected void initialize_formulas( )
      {
         /* GeneXus formulas. */
         edtavCtlgroupid_Enabled = 0;
         edtavCtlgroupname_Enabled = 0;
         cmbavCtlgrouptype.Enabled = 0;
         chkavCtlamigroupowner.Enabled = 0;
         chkavCtlisactive.Enabled = 0;
         edtavAcceptinvitation_Enabled = 0;
         edtavDeclineinvitation_Enabled = 0;
         edtavDelete_Enabled = 0;
      }

      protected void RF1F2( )
      {
         initialize_formulas( ) ;
         clear_multi_value_controls( ) ;
         if ( isAjaxCallMode( ) )
         {
            GridgroupContainer.ClearRows();
         }
         wbStart = 11;
         nGXsfl_11_idx = 1;
         sGXsfl_11_idx = StringUtil.PadL( StringUtil.LTrimStr( (decimal)(nGXsfl_11_idx), 4, 0), 4, "0");
         SubsflControlProps_112( ) ;
         bGXsfl_11_Refreshing = true;
         GridgroupContainer.AddObjectProperty("GridName", "Gridgroup");
         GridgroupContainer.AddObjectProperty("CmpContext", "");
         GridgroupContainer.AddObjectProperty("InMasterPage", "false");
         GridgroupContainer.AddObjectProperty("Class", "Grid");
         GridgroupContainer.AddObjectProperty("Cellpadding", StringUtil.LTrim( StringUtil.NToC( (decimal)(1), 4, 0, ".", "")));
         GridgroupContainer.AddObjectProperty("Cellspacing", StringUtil.LTrim( StringUtil.NToC( (decimal)(2), 4, 0, ".", "")));
         GridgroupContainer.AddObjectProperty("Backcolorstyle", StringUtil.LTrim( StringUtil.NToC( (decimal)(subGridgroup_Backcolorstyle), 1, 0, ".", "")));
         GridgroupContainer.PageSize = subGridgroup_fnc_Recordsperpage( );
         gxdyncontrolsrefreshing = true;
         fix_multi_value_controls( ) ;
         gxdyncontrolsrefreshing = false;
         if ( ! context.WillRedirect( ) && ( context.nUserReturn != 1 ) )
         {
            SubsflControlProps_112( ) ;
            /* Execute user event: Gridgroup.Load */
            E171F2 ();
            wbEnd = 11;
            WB1F0( ) ;
         }
         bGXsfl_11_Refreshing = true;
      }

      protected void send_integrity_lvl_hashes1F2( )
      {
      }

      protected int subGridgroup_fnc_Pagecount( )
      {
         return (int)(-1) ;
      }

      protected int subGridgroup_fnc_Recordcount( )
      {
         return (int)(-1) ;
      }

      protected int subGridgroup_fnc_Recordsperpage( )
      {
         return (int)(-1) ;
      }

      protected int subGridgroup_fnc_Currentpage( )
      {
         return (int)(-1) ;
      }

      protected void before_start_formulas( )
      {
         edtavCtlgroupid_Enabled = 0;
         edtavCtlgroupname_Enabled = 0;
         cmbavCtlgrouptype.Enabled = 0;
         chkavCtlamigroupowner.Enabled = 0;
         chkavCtlisactive.Enabled = 0;
         edtavAcceptinvitation_Enabled = 0;
         edtavDeclineinvitation_Enabled = 0;
         edtavDelete_Enabled = 0;
         fix_multi_value_controls( ) ;
      }

      protected void STRUP1F0( )
      {
         /* Before Start, stand alone formulas. */
         before_start_formulas( ) ;
         /* Execute Start event if defined. */
         context.wbGlbDoneStart = 0;
         /* Execute user event: Start */
         E141F2 ();
         context.wbGlbDoneStart = 1;
         /* After Start, stand alone formulas. */
         if ( StringUtil.StrCmp(context.GetRequestMethod( ), "POST") == 0 )
         {
            /* Read saved SDTs. */
            ajax_req_read_hidden_sdt(cgiGet( "Grouplist"), AV38groupList);
            ajax_req_read_hidden_sdt(cgiGet( "vGROUPLIST"), AV38groupList);
            /* Read saved values. */
            nRC_GXsfl_11 = (int)(Math.Round(context.localUtil.CToN( cgiGet( "nRC_GXsfl_11"), ".", ","), 18, MidpointRounding.ToEven));
            nRC_GXsfl_11 = (int)(Math.Round(context.localUtil.CToN( cgiGet( "nRC_GXsfl_11"), ".", ","), 18, MidpointRounding.ToEven));
            nGXsfl_11_fel_idx = 0;
            while ( nGXsfl_11_fel_idx < nRC_GXsfl_11 )
            {
               nGXsfl_11_fel_idx = ((subGridgroup_Islastpage==1)&&(nGXsfl_11_fel_idx+1>subGridgroup_fnc_Recordsperpage( )) ? 1 : nGXsfl_11_fel_idx+1);
               sGXsfl_11_fel_idx = StringUtil.PadL( StringUtil.LTrimStr( (decimal)(nGXsfl_11_fel_idx), 4, 0), 4, "0");
               SubsflControlProps_fel_112( ) ;
               AV40GXV1 = nGXsfl_11_fel_idx;
               if ( ( AV38groupList.Count >= AV40GXV1 ) && ( AV40GXV1 > 0 ) )
               {
                  AV38groupList.CurrentItem = ((GeneXus.Programs.wallet.registered.SdtGroupListItem)AV38groupList.Item(AV40GXV1));
                  AV13acceptInvitation = cgiGet( edtavAcceptinvitation_Internalname);
                  AV14declineInvitation = cgiGet( edtavDeclineinvitation_Internalname);
                  AV10delete = cgiGet( edtavDelete_Internalname);
                  AV36deleteImage = cgiGet( edtavDeleteimage_Internalname);
               }
            }
            if ( nGXsfl_11_fel_idx == 0 )
            {
               nGXsfl_11_idx = 1;
               sGXsfl_11_idx = StringUtil.PadL( StringUtil.LTrimStr( (decimal)(nGXsfl_11_idx), 4, 0), 4, "0");
               SubsflControlProps_112( ) ;
            }
            nGXsfl_11_fel_idx = 1;
            /* Read variables values. */
            /* Read subfile selected row values. */
            /* Read hidden variables. */
            GXKey = Decrypt64( context.GetCookie( "GX_SESSION_ID"), Crypto.GetServerKey( ));
         }
         else
         {
            dynload_actions( ) ;
         }
      }

      protected void GXStart( )
      {
         /* Execute user event: Start */
         E141F2 ();
         if (returnInSub) return;
      }

      protected void E141F2( )
      {
         /* Start Routine */
         returnInSub = false;
         AV9websession.Set("Group_EDIT", "");
         /* Execute user subroutine: 'INIT GROUPS FROM FILE' */
         S112 ();
         if (returnInSub) return;
      }

      protected void E121F2( )
      {
         AV40GXV1 = nGXsfl_11_idx;
         if ( ( AV40GXV1 > 0 ) && ( AV38groupList.Count >= AV40GXV1 ) )
         {
            AV38groupList.CurrentItem = ((GeneXus.Programs.wallet.registered.SdtGroupListItem)AV38groupList.Item(AV40GXV1));
         }
         /* 'Recover groups from server' Routine */
         returnInSub = false;
         GXt_char1 = AV7error;
         new GeneXus.Programs.wallet.registered.recovergroupsfromserver(context ).execute( out  GXt_char1) ;
         AV7error = GXt_char1;
         if ( ! String.IsNullOrEmpty(StringUtil.RTrim( AV7error)) )
         {
            GX_msglist.addItem("error: "+AV7error);
         }
         else
         {
            /* Execute user subroutine: 'INIT GROUPS FROM FILE' */
            S112 ();
            if (returnInSub) return;
         }
         /*  Sending Event outputs  */
         if ( gx_BV11 )
         {
            context.httpAjaxContext.ajax_rsp_assign_sdt_attri("", false, "AV38groupList", AV38groupList);
            nGXsfl_11_bak_idx = nGXsfl_11_idx;
            gxgrGridgroup_refresh( AV38groupList) ;
            nGXsfl_11_idx = nGXsfl_11_bak_idx;
            sGXsfl_11_idx = StringUtil.PadL( StringUtil.LTrimStr( (decimal)(nGXsfl_11_idx), 4, 0), 4, "0");
            SubsflControlProps_112( ) ;
         }
      }

      protected void E151F2( )
      {
         AV40GXV1 = nGXsfl_11_idx;
         if ( ( AV40GXV1 > 0 ) && ( AV38groupList.Count >= AV40GXV1 ) )
         {
            AV38groupList.CurrentItem = ((GeneXus.Programs.wallet.registered.SdtGroupListItem)AV38groupList.Item(AV40GXV1));
         }
         /* 'go to group' Routine */
         returnInSub = false;
         new GeneXus.Programs.wallet.registered.setgroupedit(context ).execute(  ((GeneXus.Programs.wallet.registered.SdtGroupListItem)(AV38groupList.CurrentItem)).gxTpr_Groupid,  ((GeneXus.Programs.wallet.registered.SdtGroupListItem)(AV38groupList.CurrentItem)).gxTpr_Referencegroupid) ;
         CallWebObject(formatLink("wallet.registered.smartgroup", new object[] {UrlEncode(((GeneXus.Programs.wallet.registered.SdtGroupListItem)(AV38groupList.CurrentItem)).gxTpr_Groupid.ToString())}, new string[] {"groupId"}) );
         context.wjLocDisableFrm = 1;
      }

      protected void S112( )
      {
         /* 'INIT GROUPS FROM FILE' Routine */
         returnInSub = false;
         GXt_objcol_SdtGroupListItem2 = AV38groupList;
         new GeneXus.Programs.wallet.registered.getgrouplist(context ).execute( out  GXt_objcol_SdtGroupListItem2) ;
         AV38groupList = GXt_objcol_SdtGroupListItem2;
         gx_BV11 = true;
      }

      protected void E161F2( )
      {
         AV40GXV1 = nGXsfl_11_idx;
         if ( ( AV40GXV1 > 0 ) && ( AV38groupList.Count >= AV40GXV1 ) )
         {
            AV38groupList.CurrentItem = ((GeneXus.Programs.wallet.registered.SdtGroupListItem)AV38groupList.Item(AV40GXV1));
         }
         /* 'Delete Group' Routine */
         returnInSub = false;
         AV39selectedGroupId = ((GeneXus.Programs.wallet.registered.SdtGroupListItem)(AV38groupList.CurrentItem)).gxTpr_Groupid;
         AssignAttri("", false, "AV39selectedGroupId", AV39selectedGroupId.ToString());
         if ( ((GeneXus.Programs.wallet.registered.SdtGroupListItem)(AV38groupList.CurrentItem)).gxTpr_Amigroupowner )
         {
            this.executeExternalObjectMethod("", false, "gx.extensions.web.dialogs", "showConfirm", new Object[] {(string)"Are you sure you want to Delete this Group ?"}, false);
         }
         else
         {
            this.executeExternalObjectMethod("", false, "gx.extensions.web.dialogs", "showConfirm", new Object[] {(string)"Are you sure you want to Remove this Group ?"}, false);
         }
         /*  Sending Event outputs  */
      }

      protected void E131F2( )
      {
         AV40GXV1 = nGXsfl_11_idx;
         if ( ( AV40GXV1 > 0 ) && ( AV38groupList.Count >= AV40GXV1 ) )
         {
            AV38groupList.CurrentItem = ((GeneXus.Programs.wallet.registered.SdtGroupListItem)AV38groupList.Item(AV40GXV1));
         }
         /* Extensions\Web\Dialog_Onconfirmclosed Routine */
         returnInSub = false;
         if ( AV11UserResponse )
         {
            GXt_char1 = AV7error;
            new GeneXus.Programs.wallet.registered.deletegroupfromlist(context ).execute(  AV39selectedGroupId, out  GXt_char1) ;
            AV7error = GXt_char1;
            if ( ! String.IsNullOrEmpty(StringUtil.RTrim( AV7error)) )
            {
               GX_msglist.addItem(AV7error);
            }
            /* Execute user subroutine: 'INIT GROUPS FROM FILE' */
            S112 ();
            if (returnInSub) return;
         }
         /*  Sending Event outputs  */
         if ( gx_BV11 )
         {
            context.httpAjaxContext.ajax_rsp_assign_sdt_attri("", false, "AV38groupList", AV38groupList);
            nGXsfl_11_bak_idx = nGXsfl_11_idx;
            gxgrGridgroup_refresh( AV38groupList) ;
            nGXsfl_11_idx = nGXsfl_11_bak_idx;
            sGXsfl_11_idx = StringUtil.PadL( StringUtil.LTrimStr( (decimal)(nGXsfl_11_idx), 4, 0), 4, "0");
            SubsflControlProps_112( ) ;
         }
      }

      private void E171F2( )
      {
         /* Gridgroup_Load Routine */
         returnInSub = false;
         AV40GXV1 = 1;
         while ( AV40GXV1 <= AV38groupList.Count )
         {
            AV38groupList.CurrentItem = ((GeneXus.Programs.wallet.registered.SdtGroupListItem)AV38groupList.Item(AV40GXV1));
            edtavDeleteimage_gximage = "GeneXusUnanimo_delete_light";
            AV36deleteImage = context.GetImagePath( "db0f63cd-dde8-4bf7-aca2-01cdf8d3c157", "", context.GetTheme( ));
            AssignAttri("", false, edtavDeleteimage_Internalname, AV36deleteImage);
            AV46Deleteimage_GXI = GXDbFile.PathToUrl( context.GetImagePath( "db0f63cd-dde8-4bf7-aca2-01cdf8d3c157", "", context.GetTheme( )), context);
            AV13acceptInvitation = "Accept Invitation";
            AssignAttri("", false, edtavAcceptinvitation_Internalname, AV13acceptInvitation);
            AV14declineInvitation = "Decline Invitation";
            AssignAttri("", false, edtavDeclineinvitation_Internalname, AV14declineInvitation);
            if ( (Guid.Empty==((GeneXus.Programs.wallet.registered.SdtGroupListItem)(AV38groupList.CurrentItem)).gxTpr_Groupid) )
            {
               edtavAcceptinvitation_Visible = 1;
               edtavDeclineinvitation_Visible = 1;
               edtavDeleteimage_Visible = 0;
            }
            else
            {
               edtavAcceptinvitation_Visible = 0;
               edtavDeclineinvitation_Visible = 0;
               edtavDeleteimage_Visible = 1;
            }
            /* Load Method */
            if ( wbStart != -1 )
            {
               wbStart = 11;
            }
            sendrow_112( ) ;
            if ( isFullAjaxMode( ) && ! bGXsfl_11_Refreshing )
            {
               DoAjaxLoad(11, GridgroupRow);
            }
            AV40GXV1 = (int)(AV40GXV1+1);
         }
         /*  Sending Event outputs  */
      }

      protected void E181F2( )
      {
         AV40GXV1 = nGXsfl_11_idx;
         if ( ( AV40GXV1 > 0 ) && ( AV38groupList.Count >= AV40GXV1 ) )
         {
            AV38groupList.CurrentItem = ((GeneXus.Programs.wallet.registered.SdtGroupListItem)AV38groupList.Item(AV40GXV1));
         }
         /* 'Decline Invitation' Routine */
         returnInSub = false;
         GXt_char1 = AV7error;
         new GeneXus.Programs.wallet.registered.declinegroupinvitation(context ).execute(  ((GeneXus.Programs.wallet.registered.SdtGroupListItem)(AV38groupList.CurrentItem)).gxTpr_Referencegroupid, out  GXt_char1) ;
         AV7error = GXt_char1;
         if ( String.IsNullOrEmpty(StringUtil.RTrim( AV7error)) )
         {
            /* Execute user subroutine: 'INIT GROUPS FROM FILE' */
            S112 ();
            if (returnInSub) return;
         }
         else
         {
            GX_msglist.addItem(AV7error);
         }
         /*  Sending Event outputs  */
         context.httpAjaxContext.ajax_rsp_assign_sdt_attri("", false, "AV38groupList", AV38groupList);
         nGXsfl_11_bak_idx = nGXsfl_11_idx;
         gxgrGridgroup_refresh( AV38groupList) ;
         nGXsfl_11_idx = nGXsfl_11_bak_idx;
         sGXsfl_11_idx = StringUtil.PadL( StringUtil.LTrimStr( (decimal)(nGXsfl_11_idx), 4, 0), 4, "0");
         SubsflControlProps_112( ) ;
      }

      protected void E191F2( )
      {
         AV40GXV1 = nGXsfl_11_idx;
         if ( ( AV40GXV1 > 0 ) && ( AV38groupList.Count >= AV40GXV1 ) )
         {
            AV38groupList.CurrentItem = ((GeneXus.Programs.wallet.registered.SdtGroupListItem)AV38groupList.Item(AV40GXV1));
         }
         /* 'Accept Invitation' Routine */
         returnInSub = false;
         GXt_char1 = AV7error;
         new GeneXus.Programs.wallet.registered.acceptgroupinvitation(context ).execute(  ((GeneXus.Programs.wallet.registered.SdtGroupListItem)(AV38groupList.CurrentItem)).gxTpr_Referencegroupid, out  GXt_char1) ;
         AV7error = GXt_char1;
         if ( ! String.IsNullOrEmpty(StringUtil.RTrim( AV7error)) )
         {
            GX_msglist.addItem(AV7error);
         }
         /* Execute user subroutine: 'INIT GROUPS FROM FILE' */
         S112 ();
         if (returnInSub) return;
         /*  Sending Event outputs  */
         context.httpAjaxContext.ajax_rsp_assign_sdt_attri("", false, "AV38groupList", AV38groupList);
         nGXsfl_11_bak_idx = nGXsfl_11_idx;
         gxgrGridgroup_refresh( AV38groupList) ;
         nGXsfl_11_idx = nGXsfl_11_bak_idx;
         sGXsfl_11_idx = StringUtil.PadL( StringUtil.LTrimStr( (decimal)(nGXsfl_11_idx), 4, 0), 4, "0");
         SubsflControlProps_112( ) ;
      }

      public override void setparameters( Object[] obj )
      {
         createObjects();
         initialize();
      }

      public override string getresponse( string sGXDynURL )
      {
         initialize_properties( ) ;
         BackMsgLst = context.GX_msglist;
         context.GX_msglist = LclMsgLst;
         sDynURL = sGXDynURL;
         nGotPars = (short)(1);
         nGXWrapped = (short)(1);
         context.SetWrapped(true);
         PA1F2( ) ;
         WS1F2( ) ;
         WE1F2( ) ;
         cleanup();
         context.SetWrapped(false);
         context.GX_msglist = BackMsgLst;
         return "";
      }

      public void responsestatic( string sGXDynURL )
      {
      }

      protected void define_styles( )
      {
         AddThemeStyleSheetFile("", context.GetTheme( )+".css", "?"+GetCacheInvalidationToken( ));
         bool outputEnabled = isOutputEnabled( );
         if ( context.isSpaRequest( ) )
         {
            enableOutput();
         }
         idxLst = 1;
         while ( idxLst <= Form.Jscriptsrc.Count )
         {
            context.AddJavascriptSource(StringUtil.RTrim( ((string)Form.Jscriptsrc.Item(idxLst))), "?202610714165764", true, true, false);
            idxLst = (int)(idxLst+1);
         }
         if ( ! outputEnabled )
         {
            if ( context.isSpaRequest( ) )
            {
               disableOutput();
            }
         }
         /* End function define_styles */
      }

      protected void include_jscripts( )
      {
         context.AddJavascriptSource("messages.eng.js", "?"+GetCacheInvalidationToken( ), false, true, false);
         context.AddJavascriptSource("wallet/registered/smartgroups.js", "?202610714165764", false, true, false);
         context.AddJavascriptSource("web-extension/gx-web-extensions.js", "", false, true, false);
         /* End function include_jscripts */
      }

      protected void SubsflControlProps_112( )
      {
         edtavCtlgroupid_Internalname = "CTLGROUPID_"+sGXsfl_11_idx;
         edtavCtlgroupname_Internalname = "CTLGROUPNAME_"+sGXsfl_11_idx;
         cmbavCtlgrouptype_Internalname = "CTLGROUPTYPE_"+sGXsfl_11_idx;
         chkavCtlamigroupowner_Internalname = "CTLAMIGROUPOWNER_"+sGXsfl_11_idx;
         chkavCtlisactive_Internalname = "CTLISACTIVE_"+sGXsfl_11_idx;
         edtavAcceptinvitation_Internalname = "vACCEPTINVITATION_"+sGXsfl_11_idx;
         edtavDeclineinvitation_Internalname = "vDECLINEINVITATION_"+sGXsfl_11_idx;
         edtavDelete_Internalname = "vDELETE_"+sGXsfl_11_idx;
         edtavDeleteimage_Internalname = "vDELETEIMAGE_"+sGXsfl_11_idx;
      }

      protected void SubsflControlProps_fel_112( )
      {
         edtavCtlgroupid_Internalname = "CTLGROUPID_"+sGXsfl_11_fel_idx;
         edtavCtlgroupname_Internalname = "CTLGROUPNAME_"+sGXsfl_11_fel_idx;
         cmbavCtlgrouptype_Internalname = "CTLGROUPTYPE_"+sGXsfl_11_fel_idx;
         chkavCtlamigroupowner_Internalname = "CTLAMIGROUPOWNER_"+sGXsfl_11_fel_idx;
         chkavCtlisactive_Internalname = "CTLISACTIVE_"+sGXsfl_11_fel_idx;
         edtavAcceptinvitation_Internalname = "vACCEPTINVITATION_"+sGXsfl_11_fel_idx;
         edtavDeclineinvitation_Internalname = "vDECLINEINVITATION_"+sGXsfl_11_fel_idx;
         edtavDelete_Internalname = "vDELETE_"+sGXsfl_11_fel_idx;
         edtavDeleteimage_Internalname = "vDELETEIMAGE_"+sGXsfl_11_fel_idx;
      }

      protected void sendrow_112( )
      {
         sGXsfl_11_idx = StringUtil.PadL( StringUtil.LTrimStr( (decimal)(nGXsfl_11_idx), 4, 0), 4, "0");
         SubsflControlProps_112( ) ;
         WB1F0( ) ;
         GridgroupRow = GXWebRow.GetNew(context,GridgroupContainer);
         if ( subGridgroup_Backcolorstyle == 0 )
         {
            /* None style subfile background logic. */
            subGridgroup_Backstyle = 0;
            if ( StringUtil.StrCmp(subGridgroup_Class, "") != 0 )
            {
               subGridgroup_Linesclass = subGridgroup_Class+"Odd";
            }
         }
         else if ( subGridgroup_Backcolorstyle == 1 )
         {
            /* Uniform style subfile background logic. */
            subGridgroup_Backstyle = 0;
            subGridgroup_Backcolor = subGridgroup_Allbackcolor;
            if ( StringUtil.StrCmp(subGridgroup_Class, "") != 0 )
            {
               subGridgroup_Linesclass = subGridgroup_Class+"Uniform";
            }
         }
         else if ( subGridgroup_Backcolorstyle == 2 )
         {
            /* Header style subfile background logic. */
            subGridgroup_Backstyle = 1;
            if ( StringUtil.StrCmp(subGridgroup_Class, "") != 0 )
            {
               subGridgroup_Linesclass = subGridgroup_Class+"Odd";
            }
            subGridgroup_Backcolor = (int)(0x0);
         }
         else if ( subGridgroup_Backcolorstyle == 3 )
         {
            /* Report style subfile background logic. */
            subGridgroup_Backstyle = 1;
            if ( ((int)((nGXsfl_11_idx) % (2))) == 0 )
            {
               subGridgroup_Backcolor = (int)(0x0);
               if ( StringUtil.StrCmp(subGridgroup_Class, "") != 0 )
               {
                  subGridgroup_Linesclass = subGridgroup_Class+"Even";
               }
            }
            else
            {
               subGridgroup_Backcolor = (int)(0x0);
               if ( StringUtil.StrCmp(subGridgroup_Class, "") != 0 )
               {
                  subGridgroup_Linesclass = subGridgroup_Class+"Odd";
               }
            }
         }
         if ( GridgroupContainer.GetWrapped() == 1 )
         {
            context.WriteHtmlText( "<tr ") ;
            context.WriteHtmlText( " class=\""+"Grid"+"\" style=\""+""+"\"") ;
            context.WriteHtmlText( " gxrow=\""+sGXsfl_11_idx+"\">") ;
         }
         /* Subfile cell */
         if ( GridgroupContainer.GetWrapped() == 1 )
         {
            context.WriteHtmlText( "<td valign=\"middle\" align=\""+""+"\""+" style=\""+"display:none;"+"\">") ;
         }
         /* Single line edit */
         ROClassString = "Attribute";
         GridgroupRow.AddColumnProperties("edit", 1, isAjaxCallMode( ), new Object[] {(string)edtavCtlgroupid_Internalname,((GeneXus.Programs.wallet.registered.SdtGroupListItem)AV38groupList.Item(AV40GXV1)).gxTpr_Groupid.ToString(),((GeneXus.Programs.wallet.registered.SdtGroupListItem)AV38groupList.Item(AV40GXV1)).gxTpr_Groupid.ToString(),""+" onchange=\""+""+";gx.evt.onchange(this, event)\" ",(string)"'"+""+"'"+",false,"+"'"+""+"'",(string)"",(string)"",(string)"",(string)"",(string)edtavCtlgroupid_Jsonclick,(short)0,(string)"Attribute",(string)"",(string)ROClassString,(string)"",(string)"",(short)0,(int)edtavCtlgroupid_Enabled,(short)0,(string)"text",(string)"",(short)0,(string)"px",(short)17,(string)"px",(short)36,(short)0,(short)0,(short)11,(short)0,(short)0,(short)0,(bool)true,(string)"",(string)"",(bool)false,(string)""});
         /* Subfile cell */
         if ( GridgroupContainer.GetWrapped() == 1 )
         {
            context.WriteHtmlText( "<td valign=\"middle\" align=\""+"start"+"\""+" style=\""+""+"\">") ;
         }
         /* Single line edit */
         TempTags = "  onfocus=\"gx.evt.onfocus(this, 13,'',false,'" + sGXsfl_11_idx + "',11)\"";
         ROClassString = "Attribute";
         edtavCtlgroupname_Link = "";
         AssignProp("", false, edtavCtlgroupname_Internalname, "Link", edtavCtlgroupname_Link, !bGXsfl_11_Refreshing);
         GridgroupRow.AddColumnProperties("edit", 1, isAjaxCallMode( ), new Object[] {(string)edtavCtlgroupname_Internalname,StringUtil.RTrim( ((GeneXus.Programs.wallet.registered.SdtGroupListItem)AV38groupList.Item(AV40GXV1)).gxTpr_Groupname),(string)"",TempTags+" onchange=\""+""+";gx.evt.onchange(this, event)\" "+" onblur=\""+""+";gx.evt.onblur(this,13);\"","'"+""+"'"+",false,"+"'"+"E\\'GO TO GROUP\\'."+sGXsfl_11_idx+"'",(string)edtavCtlgroupname_Link,(string)"",(string)"",(string)"",(string)edtavCtlgroupname_Jsonclick,(short)5,(string)"Attribute",(string)"",(string)ROClassString,(string)"",(string)"",(short)-1,(int)edtavCtlgroupname_Enabled,(short)0,(string)"text",(string)"",(short)0,(string)"px",(short)17,(string)"px",(short)80,(short)0,(short)0,(short)11,(short)0,(short)-1,(short)-1,(bool)true,(string)"",(string)"start",(bool)true,(string)""});
         /* Subfile cell */
         if ( GridgroupContainer.GetWrapped() == 1 )
         {
            context.WriteHtmlText( "<td valign=\"middle\" align=\""+"end"+"\""+" style=\""+""+"\">") ;
         }
         TempTags = "  onfocus=\"gx.evt.onfocus(this, 14,'',false,'" + sGXsfl_11_idx + "',11)\"";
         if ( ( cmbavCtlgrouptype.ItemCount == 0 ) && isAjaxCallMode( ) )
         {
            GXCCtl = "CTLGROUPTYPE_" + sGXsfl_11_idx;
            cmbavCtlgrouptype.Name = GXCCtl;
            cmbavCtlgrouptype.WebTags = "";
            cmbavCtlgrouptype.addItem("0", "Select Group Type", 0);
            cmbavCtlgrouptype.addItem("10", "Wallet Backup", 0);
            cmbavCtlgrouptype.addItem("30", "Delegation Multi-Signature Wallet", 0);
            cmbavCtlgrouptype.addItem("40", "Encrypted Passwords", 0);
            cmbavCtlgrouptype.addItem("20", "Time Encrypted Vault", 0);
            cmbavCtlgrouptype.addItem("50", "Legacy Multi-Signature Wallet", 0);
            if ( cmbavCtlgrouptype.ItemCount > 0 )
            {
               if ( ( AV40GXV1 > 0 ) && ( AV38groupList.Count >= AV40GXV1 ) && (0==((GeneXus.Programs.wallet.registered.SdtGroupListItem)AV38groupList.Item(AV40GXV1)).gxTpr_Grouptype) )
               {
                  ((GeneXus.Programs.wallet.registered.SdtGroupListItem)AV38groupList.Item(AV40GXV1)).gxTpr_Grouptype = (short)(Math.Round(NumberUtil.Val( cmbavCtlgrouptype.getValidValue(StringUtil.Trim( StringUtil.Str( (decimal)(((GeneXus.Programs.wallet.registered.SdtGroupListItem)AV38groupList.Item(AV40GXV1)).gxTpr_Grouptype), 4, 0))), "."), 18, MidpointRounding.ToEven));
               }
            }
         }
         /* ComboBox */
         GridgroupRow.AddColumnProperties("combobox", 2, isAjaxCallMode( ), new Object[] {(GXCombobox)cmbavCtlgrouptype,(string)cmbavCtlgrouptype_Internalname,StringUtil.Trim( StringUtil.Str( (decimal)(((GeneXus.Programs.wallet.registered.SdtGroupListItem)AV38groupList.Item(AV40GXV1)).gxTpr_Grouptype), 4, 0)),(short)1,(string)cmbavCtlgrouptype_Jsonclick,(short)0,(string)"'"+""+"'"+",false,"+"'"+""+"'",(string)"int",(string)"",(short)-1,cmbavCtlgrouptype.Enabled,(short)0,(short)0,(short)0,(string)"px",(short)0,(string)"px",(string)"",(string)"Attribute",(string)"",(string)"",TempTags+" onchange=\""+""+";gx.evt.onchange(this, event)\" "+" onblur=\""+""+";gx.evt.onblur(this,14);\"",(string)"",(bool)true,(short)0});
         cmbavCtlgrouptype.CurrentValue = StringUtil.Trim( StringUtil.Str( (decimal)(((GeneXus.Programs.wallet.registered.SdtGroupListItem)AV38groupList.Item(AV40GXV1)).gxTpr_Grouptype), 4, 0));
         AssignProp("", false, cmbavCtlgrouptype_Internalname, "Values", (string)(cmbavCtlgrouptype.ToJavascriptSource()), !bGXsfl_11_Refreshing);
         /* Subfile cell */
         if ( GridgroupContainer.GetWrapped() == 1 )
         {
            context.WriteHtmlText( "<td valign=\"middle\" align=\""+""+"\""+" style=\""+""+"\">") ;
         }
         /* Check box */
         TempTags = "  onfocus=\"gx.evt.onfocus(this, 15,'',false,'" + sGXsfl_11_idx + "',11)\"";
         ClassString = "Attribute";
         StyleString = "";
         GXCCtl = "CTLAMIGROUPOWNER_" + sGXsfl_11_idx;
         chkavCtlamigroupowner.Name = GXCCtl;
         chkavCtlamigroupowner.WebTags = "";
         chkavCtlamigroupowner.Caption = "";
         AssignProp("", false, chkavCtlamigroupowner_Internalname, "TitleCaption", chkavCtlamigroupowner.Caption, !bGXsfl_11_Refreshing);
         chkavCtlamigroupowner.CheckedValue = "false";
         GridgroupRow.AddColumnProperties("checkbox", 1, isAjaxCallMode( ), new Object[] {(string)chkavCtlamigroupowner_Internalname,StringUtil.BoolToStr( ((GeneXus.Programs.wallet.registered.SdtGroupListItem)AV38groupList.Item(AV40GXV1)).gxTpr_Amigroupowner),(string)"",(string)"",(short)-1,chkavCtlamigroupowner.Enabled,(string)"true",(string)"",(string)StyleString,(string)ClassString,(string)"",(string)"",TempTags+" onclick="+"\"gx.fn.checkboxClick(15, this, 'true', 'false',"+"''"+");"+"gx.evt.onchange(this, event);\""+" onblur=\""+""+";gx.evt.onblur(this,15);\""});
         /* Subfile cell */
         if ( GridgroupContainer.GetWrapped() == 1 )
         {
            context.WriteHtmlText( "<td valign=\"middle\" align=\""+""+"\""+" style=\""+""+"\">") ;
         }
         /* Check box */
         TempTags = "  onfocus=\"gx.evt.onfocus(this, 16,'',false,'" + sGXsfl_11_idx + "',11)\"";
         ClassString = "Attribute";
         StyleString = "";
         GXCCtl = "CTLISACTIVE_" + sGXsfl_11_idx;
         chkavCtlisactive.Name = GXCCtl;
         chkavCtlisactive.WebTags = "";
         chkavCtlisactive.Caption = "";
         AssignProp("", false, chkavCtlisactive_Internalname, "TitleCaption", chkavCtlisactive.Caption, !bGXsfl_11_Refreshing);
         chkavCtlisactive.CheckedValue = "false";
         GridgroupRow.AddColumnProperties("checkbox", 1, isAjaxCallMode( ), new Object[] {(string)chkavCtlisactive_Internalname,StringUtil.BoolToStr( ((GeneXus.Programs.wallet.registered.SdtGroupListItem)AV38groupList.Item(AV40GXV1)).gxTpr_Isactive),(string)"",(string)"",(short)-1,chkavCtlisactive.Enabled,(string)"true",(string)"",(string)StyleString,(string)ClassString,(string)"",(string)"",TempTags+" onclick="+"\"gx.fn.checkboxClick(16, this, 'true', 'false',"+"''"+");"+"gx.evt.onchange(this, event);\""+" onblur=\""+""+";gx.evt.onblur(this,16);\""});
         /* Subfile cell */
         if ( GridgroupContainer.GetWrapped() == 1 )
         {
            context.WriteHtmlText( "<td valign=\"middle\" align=\""+"start"+"\""+" style=\""+((edtavAcceptinvitation_Visible==0) ? "display:none;" : "")+"\">") ;
         }
         /* Single line edit */
         TempTags = "  onfocus=\"gx.evt.onfocus(this, 17,'',false,'" + sGXsfl_11_idx + "',11)\"";
         ROClassString = "Attribute";
         GridgroupRow.AddColumnProperties("edit", 1, isAjaxCallMode( ), new Object[] {(string)edtavAcceptinvitation_Internalname,StringUtil.RTrim( AV13acceptInvitation),(string)"",TempTags+" onchange=\""+""+";gx.evt.onchange(this, event)\" "+" onblur=\""+""+";gx.evt.onblur(this,17);\"","'"+""+"'"+",false,"+"'"+"E\\'ACCEPT INVITATION\\'."+sGXsfl_11_idx+"'",(string)"",(string)"",(string)"",(string)"",(string)edtavAcceptinvitation_Jsonclick,(short)5,(string)"Attribute",(string)"",(string)ROClassString,(string)"",(string)"",(int)edtavAcceptinvitation_Visible,(int)edtavAcceptinvitation_Enabled,(short)0,(string)"text",(string)"",(short)0,(string)"px",(short)17,(string)"px",(short)20,(short)0,(short)0,(short)11,(short)0,(short)-1,(short)-1,(bool)true,(string)"",(string)"start",(bool)true,(string)""});
         /* Subfile cell */
         if ( GridgroupContainer.GetWrapped() == 1 )
         {
            context.WriteHtmlText( "<td valign=\"middle\" align=\""+"start"+"\""+" style=\""+((edtavDeclineinvitation_Visible==0) ? "display:none;" : "")+"\">") ;
         }
         /* Single line edit */
         TempTags = "  onfocus=\"gx.evt.onfocus(this, 18,'',false,'" + sGXsfl_11_idx + "',11)\"";
         ROClassString = "Attribute";
         GridgroupRow.AddColumnProperties("edit", 1, isAjaxCallMode( ), new Object[] {(string)edtavDeclineinvitation_Internalname,StringUtil.RTrim( AV14declineInvitation),(string)"",TempTags+" onchange=\""+""+";gx.evt.onchange(this, event)\" "+" onblur=\""+""+";gx.evt.onblur(this,18);\"","'"+""+"'"+",false,"+"'"+"E\\'DECLINE INVITATION\\'."+sGXsfl_11_idx+"'",(string)"",(string)"",(string)"",(string)"",(string)edtavDeclineinvitation_Jsonclick,(short)5,(string)"Attribute",(string)"",(string)ROClassString,(string)"",(string)"",(int)edtavDeclineinvitation_Visible,(int)edtavDeclineinvitation_Enabled,(short)0,(string)"text",(string)"",(short)0,(string)"px",(short)17,(string)"px",(short)20,(short)0,(short)0,(short)11,(short)0,(short)-1,(short)-1,(bool)true,(string)"",(string)"start",(bool)true,(string)""});
         /* Subfile cell */
         if ( GridgroupContainer.GetWrapped() == 1 )
         {
            context.WriteHtmlText( "<td valign=\"middle\" align=\""+"start"+"\""+" style=\""+""+"\">") ;
         }
         /* Single line edit */
         TempTags = "  onfocus=\"gx.evt.onfocus(this, 19,'',false,'" + sGXsfl_11_idx + "',11)\"";
         ROClassString = "Attribute";
         GridgroupRow.AddColumnProperties("edit", 1, isAjaxCallMode( ), new Object[] {(string)edtavDelete_Internalname,StringUtil.RTrim( AV10delete),(string)"",TempTags+" onchange=\""+""+";gx.evt.onchange(this, event)\" "+" onblur=\""+""+";gx.evt.onblur(this,19);\"","'"+""+"'"+",false,"+"'"+"E\\'DELETE GROUP\\'."+sGXsfl_11_idx+"'",(string)"",(string)"",(string)"",(string)"",(string)edtavDelete_Jsonclick,(short)5,(string)"Attribute",(string)"",(string)ROClassString,(string)"",(string)"",(short)-1,(int)edtavDelete_Enabled,(short)0,(string)"text",(string)"",(short)0,(string)"px",(short)17,(string)"px",(short)20,(short)0,(short)0,(short)11,(short)0,(short)-1,(short)-1,(bool)true,(string)"",(string)"start",(bool)true,(string)""});
         /* Subfile cell */
         if ( GridgroupContainer.GetWrapped() == 1 )
         {
            context.WriteHtmlText( "<td valign=\"middle\" align=\""+""+"\""+" style=\""+((edtavDeleteimage_Visible==0) ? "display:none;" : "")+"\">") ;
         }
         /* Active Bitmap Variable */
         TempTags = "  onfocus=\"gx.evt.onfocus(this, 20,'',false,'',11)\"";
         ClassString = "Image" + " " + ((StringUtil.StrCmp(edtavDeleteimage_gximage, "")==0) ? "" : "GX_Image_"+edtavDeleteimage_gximage+"_Class");
         StyleString = "";
         AV36deleteImage_IsBlob = (bool)((String.IsNullOrEmpty(StringUtil.RTrim( AV36deleteImage))&&String.IsNullOrEmpty(StringUtil.RTrim( AV46Deleteimage_GXI)))||!String.IsNullOrEmpty(StringUtil.RTrim( AV36deleteImage)));
         sImgUrl = (String.IsNullOrEmpty(StringUtil.RTrim( AV36deleteImage)) ? AV46Deleteimage_GXI : context.PathToRelativeUrl( AV36deleteImage));
         GridgroupRow.AddColumnProperties("bitmap", 1, isAjaxCallMode( ), new Object[] {(string)edtavDeleteimage_Internalname,(string)sImgUrl,(string)"",(string)"",(string)"",context.GetTheme( ),(int)edtavDeleteimage_Visible,(short)1,(string)"",(string)"",(short)0,(short)-1,(short)0,(string)"px",(short)0,(string)"px",(short)0,(short)0,(short)5,(string)edtavDeleteimage_Jsonclick,"'"+""+"'"+",false,"+"'"+"E\\'DELETE GROUP\\'."+sGXsfl_11_idx+"'",(string)StyleString,(string)ClassString,(string)"",(string)"",(string)"",(string)"",(string)""+TempTags,(string)"",(string)"",(short)1,(bool)AV36deleteImage_IsBlob,(bool)false,context.GetImageSrcSet( sImgUrl),(string)"none"});
         send_integrity_lvl_hashes1F2( ) ;
         GridgroupContainer.AddRow(GridgroupRow);
         nGXsfl_11_idx = ((subGridgroup_Islastpage==1)&&(nGXsfl_11_idx+1>subGridgroup_fnc_Recordsperpage( )) ? 1 : nGXsfl_11_idx+1);
         sGXsfl_11_idx = StringUtil.PadL( StringUtil.LTrimStr( (decimal)(nGXsfl_11_idx), 4, 0), 4, "0");
         SubsflControlProps_112( ) ;
         /* End function sendrow_112 */
      }

      protected void init_web_controls( )
      {
         GXCCtl = "CTLGROUPTYPE_" + sGXsfl_11_idx;
         cmbavCtlgrouptype.Name = GXCCtl;
         cmbavCtlgrouptype.WebTags = "";
         cmbavCtlgrouptype.addItem("0", "Select Group Type", 0);
         cmbavCtlgrouptype.addItem("10", "Wallet Backup", 0);
         cmbavCtlgrouptype.addItem("30", "Delegation Multi-Signature Wallet", 0);
         cmbavCtlgrouptype.addItem("40", "Encrypted Passwords", 0);
         cmbavCtlgrouptype.addItem("20", "Time Encrypted Vault", 0);
         cmbavCtlgrouptype.addItem("50", "Legacy Multi-Signature Wallet", 0);
         if ( cmbavCtlgrouptype.ItemCount > 0 )
         {
            if ( ( AV40GXV1 > 0 ) && ( AV38groupList.Count >= AV40GXV1 ) && (0==((GeneXus.Programs.wallet.registered.SdtGroupListItem)AV38groupList.Item(AV40GXV1)).gxTpr_Grouptype) )
            {
               ((GeneXus.Programs.wallet.registered.SdtGroupListItem)AV38groupList.Item(AV40GXV1)).gxTpr_Grouptype = (short)(Math.Round(NumberUtil.Val( cmbavCtlgrouptype.getValidValue(StringUtil.Trim( StringUtil.Str( (decimal)(((GeneXus.Programs.wallet.registered.SdtGroupListItem)AV38groupList.Item(AV40GXV1)).gxTpr_Grouptype), 4, 0))), "."), 18, MidpointRounding.ToEven));
            }
         }
         GXCCtl = "CTLAMIGROUPOWNER_" + sGXsfl_11_idx;
         chkavCtlamigroupowner.Name = GXCCtl;
         chkavCtlamigroupowner.WebTags = "";
         chkavCtlamigroupowner.Caption = "";
         AssignProp("", false, chkavCtlamigroupowner_Internalname, "TitleCaption", chkavCtlamigroupowner.Caption, !bGXsfl_11_Refreshing);
         chkavCtlamigroupowner.CheckedValue = "false";
         GXCCtl = "CTLISACTIVE_" + sGXsfl_11_idx;
         chkavCtlisactive.Name = GXCCtl;
         chkavCtlisactive.WebTags = "";
         chkavCtlisactive.Caption = "";
         AssignProp("", false, chkavCtlisactive_Internalname, "TitleCaption", chkavCtlisactive.Caption, !bGXsfl_11_Refreshing);
         chkavCtlisactive.CheckedValue = "false";
         /* End function init_web_controls */
      }

      protected void StartGridControl11( )
      {
         if ( GridgroupContainer.GetWrapped() == 1 )
         {
            context.WriteHtmlText( "<div id=\""+"GridgroupContainer"+"DivS\" data-gxgridid=\"11\">") ;
            sStyleString = "";
            GxWebStd.gx_table_start( context, subGridgroup_Internalname, subGridgroup_Internalname, "", "Grid", 0, "", "", 1, 2, sStyleString, "", "", 0);
            /* Subfile titles */
            context.WriteHtmlText( "<tr") ;
            context.WriteHtmlTextNl( ">") ;
            if ( subGridgroup_Backcolorstyle == 0 )
            {
               subGridgroup_Titlebackstyle = 0;
               if ( StringUtil.Len( subGridgroup_Class) > 0 )
               {
                  subGridgroup_Linesclass = subGridgroup_Class+"Title";
               }
            }
            else
            {
               subGridgroup_Titlebackstyle = 1;
               if ( subGridgroup_Backcolorstyle == 1 )
               {
                  subGridgroup_Titlebackcolor = subGridgroup_Allbackcolor;
                  if ( StringUtil.Len( subGridgroup_Class) > 0 )
                  {
                     subGridgroup_Linesclass = subGridgroup_Class+"UniformTitle";
                  }
               }
               else
               {
                  if ( StringUtil.Len( subGridgroup_Class) > 0 )
                  {
                     subGridgroup_Linesclass = subGridgroup_Class+"Title";
                  }
               }
            }
            context.WriteHtmlText( "<th align=\""+""+"\" "+" nowrap=\"nowrap\" "+" class=\""+"Attribute"+"\" "+" style=\""+"display:none;"+""+"\" "+">") ;
            context.SendWebValue( "Group ID") ;
            context.WriteHtmlTextNl( "</th>") ;
            context.WriteHtmlText( "<th align=\""+"start"+"\" "+" nowrap=\"nowrap\" "+" class=\""+"Attribute"+"\" "+" style=\""+""+""+"\" "+">") ;
            context.SendWebValue( "Group Name") ;
            context.WriteHtmlTextNl( "</th>") ;
            context.WriteHtmlText( "<th align=\""+"end"+"\" "+" nowrap=\"nowrap\" "+" class=\""+"Attribute"+"\" "+" style=\""+""+""+"\" "+">") ;
            context.SendWebValue( "Group Type") ;
            context.WriteHtmlTextNl( "</th>") ;
            context.WriteHtmlText( "<th align=\""+""+"\" "+" nowrap=\"nowrap\" "+" class=\""+"Attribute"+"\" "+" style=\""+""+""+"\" "+">") ;
            context.SendWebValue( "I am group Owner") ;
            context.WriteHtmlTextNl( "</th>") ;
            context.WriteHtmlText( "<th align=\""+""+"\" "+" nowrap=\"nowrap\" "+" class=\""+"Attribute"+"\" "+" style=\""+""+""+"\" "+">") ;
            context.SendWebValue( "It is Active") ;
            context.WriteHtmlTextNl( "</th>") ;
            context.WriteHtmlText( "<th align=\""+"start"+"\" "+" nowrap=\"nowrap\" "+" class=\""+"Attribute"+"\" "+" style=\""+((edtavAcceptinvitation_Visible==0) ? "display:none;" : "")+""+"\" "+">") ;
            context.SendWebValue( "") ;
            context.WriteHtmlTextNl( "</th>") ;
            context.WriteHtmlText( "<th align=\""+"start"+"\" "+" nowrap=\"nowrap\" "+" class=\""+"Attribute"+"\" "+" style=\""+((edtavDeclineinvitation_Visible==0) ? "display:none;" : "")+""+"\" "+">") ;
            context.SendWebValue( "") ;
            context.WriteHtmlTextNl( "</th>") ;
            context.WriteHtmlText( "<th align=\""+"start"+"\" "+" nowrap=\"nowrap\" "+" class=\""+"Attribute"+"\" "+" style=\""+""+""+"\" "+">") ;
            context.SendWebValue( "") ;
            context.WriteHtmlTextNl( "</th>") ;
            context.WriteHtmlText( "<th align=\""+""+"\" "+" nowrap=\"nowrap\" "+" class=\""+"Image"+" "+((StringUtil.StrCmp(edtavDeleteimage_gximage, "")==0) ? "" : "GX_Image_"+edtavDeleteimage_gximage+"_Class")+"\" "+" style=\""+((edtavDeleteimage_Visible==0) ? "display:none;" : "")+""+"\" "+">") ;
            context.SendWebValue( "") ;
            context.WriteHtmlTextNl( "</th>") ;
            context.WriteHtmlTextNl( "</tr>") ;
            GridgroupContainer.AddObjectProperty("GridName", "Gridgroup");
         }
         else
         {
            GridgroupContainer.AddObjectProperty("GridName", "Gridgroup");
            GridgroupContainer.AddObjectProperty("Header", subGridgroup_Header);
            GridgroupContainer.AddObjectProperty("Class", "Grid");
            GridgroupContainer.AddObjectProperty("Cellpadding", StringUtil.LTrim( StringUtil.NToC( (decimal)(1), 4, 0, ".", "")));
            GridgroupContainer.AddObjectProperty("Cellspacing", StringUtil.LTrim( StringUtil.NToC( (decimal)(2), 4, 0, ".", "")));
            GridgroupContainer.AddObjectProperty("Backcolorstyle", StringUtil.LTrim( StringUtil.NToC( (decimal)(subGridgroup_Backcolorstyle), 1, 0, ".", "")));
            GridgroupContainer.AddObjectProperty("CmpContext", "");
            GridgroupContainer.AddObjectProperty("InMasterPage", "false");
            GridgroupColumn = GXWebColumn.GetNew(isAjaxCallMode( ));
            GridgroupColumn.AddObjectProperty("Enabled", StringUtil.LTrim( StringUtil.NToC( (decimal)(edtavCtlgroupid_Enabled), 5, 0, ".", "")));
            GridgroupContainer.AddColumnProperties(GridgroupColumn);
            GridgroupColumn = GXWebColumn.GetNew(isAjaxCallMode( ));
            GridgroupColumn.AddObjectProperty("Enabled", StringUtil.LTrim( StringUtil.NToC( (decimal)(edtavCtlgroupname_Enabled), 5, 0, ".", "")));
            GridgroupColumn.AddObjectProperty("Link", StringUtil.RTrim( edtavCtlgroupname_Link));
            GridgroupContainer.AddColumnProperties(GridgroupColumn);
            GridgroupColumn = GXWebColumn.GetNew(isAjaxCallMode( ));
            GridgroupColumn.AddObjectProperty("Enabled", StringUtil.LTrim( StringUtil.NToC( (decimal)(cmbavCtlgrouptype.Enabled), 5, 0, ".", "")));
            GridgroupContainer.AddColumnProperties(GridgroupColumn);
            GridgroupColumn = GXWebColumn.GetNew(isAjaxCallMode( ));
            GridgroupColumn.AddObjectProperty("Enabled", StringUtil.LTrim( StringUtil.NToC( (decimal)(chkavCtlamigroupowner.Enabled), 5, 0, ".", "")));
            GridgroupContainer.AddColumnProperties(GridgroupColumn);
            GridgroupColumn = GXWebColumn.GetNew(isAjaxCallMode( ));
            GridgroupColumn.AddObjectProperty("Enabled", StringUtil.LTrim( StringUtil.NToC( (decimal)(chkavCtlisactive.Enabled), 5, 0, ".", "")));
            GridgroupContainer.AddColumnProperties(GridgroupColumn);
            GridgroupColumn = GXWebColumn.GetNew(isAjaxCallMode( ));
            GridgroupColumn.AddObjectProperty("Value", GXUtil.ValueEncode( StringUtil.RTrim( AV13acceptInvitation)));
            GridgroupColumn.AddObjectProperty("Enabled", StringUtil.LTrim( StringUtil.NToC( (decimal)(edtavAcceptinvitation_Enabled), 5, 0, ".", "")));
            GridgroupColumn.AddObjectProperty("Visible", StringUtil.LTrim( StringUtil.NToC( (decimal)(edtavAcceptinvitation_Visible), 5, 0, ".", "")));
            GridgroupContainer.AddColumnProperties(GridgroupColumn);
            GridgroupColumn = GXWebColumn.GetNew(isAjaxCallMode( ));
            GridgroupColumn.AddObjectProperty("Value", GXUtil.ValueEncode( StringUtil.RTrim( AV14declineInvitation)));
            GridgroupColumn.AddObjectProperty("Enabled", StringUtil.LTrim( StringUtil.NToC( (decimal)(edtavDeclineinvitation_Enabled), 5, 0, ".", "")));
            GridgroupColumn.AddObjectProperty("Visible", StringUtil.LTrim( StringUtil.NToC( (decimal)(edtavDeclineinvitation_Visible), 5, 0, ".", "")));
            GridgroupContainer.AddColumnProperties(GridgroupColumn);
            GridgroupColumn = GXWebColumn.GetNew(isAjaxCallMode( ));
            GridgroupColumn.AddObjectProperty("Value", GXUtil.ValueEncode( StringUtil.RTrim( AV10delete)));
            GridgroupColumn.AddObjectProperty("Enabled", StringUtil.LTrim( StringUtil.NToC( (decimal)(edtavDelete_Enabled), 5, 0, ".", "")));
            GridgroupContainer.AddColumnProperties(GridgroupColumn);
            GridgroupColumn = GXWebColumn.GetNew(isAjaxCallMode( ));
            GridgroupColumn.AddObjectProperty("Value", context.convertURL( AV36deleteImage));
            GridgroupColumn.AddObjectProperty("Link", StringUtil.RTrim( edtavDeleteimage_Link));
            GridgroupColumn.AddObjectProperty("Visible", StringUtil.LTrim( StringUtil.NToC( (decimal)(edtavDeleteimage_Visible), 5, 0, ".", "")));
            GridgroupContainer.AddColumnProperties(GridgroupColumn);
            GridgroupContainer.AddObjectProperty("Selectedindex", StringUtil.LTrim( StringUtil.NToC( (decimal)(subGridgroup_Selectedindex), 4, 0, ".", "")));
            GridgroupContainer.AddObjectProperty("Allowselection", StringUtil.LTrim( StringUtil.NToC( (decimal)(subGridgroup_Allowselection), 1, 0, ".", "")));
            GridgroupContainer.AddObjectProperty("Selectioncolor", StringUtil.LTrim( StringUtil.NToC( (decimal)(subGridgroup_Selectioncolor), 9, 0, ".", "")));
            GridgroupContainer.AddObjectProperty("Allowhover", StringUtil.LTrim( StringUtil.NToC( (decimal)(subGridgroup_Allowhovering), 1, 0, ".", "")));
            GridgroupContainer.AddObjectProperty("Hovercolor", StringUtil.LTrim( StringUtil.NToC( (decimal)(subGridgroup_Hoveringcolor), 9, 0, ".", "")));
            GridgroupContainer.AddObjectProperty("Allowcollapsing", StringUtil.LTrim( StringUtil.NToC( (decimal)(subGridgroup_Allowcollapsing), 1, 0, ".", "")));
            GridgroupContainer.AddObjectProperty("Collapsed", StringUtil.LTrim( StringUtil.NToC( (decimal)(subGridgroup_Collapsed), 1, 0, ".", "")));
         }
      }

      protected void init_default_properties( )
      {
         bttRecovergroupsfromserver_Internalname = "RECOVERGROUPSFROMSERVER";
         bttCreategroup_Internalname = "CREATEGROUP";
         edtavCtlgroupid_Internalname = "CTLGROUPID";
         edtavCtlgroupname_Internalname = "CTLGROUPNAME";
         cmbavCtlgrouptype_Internalname = "CTLGROUPTYPE";
         chkavCtlamigroupowner_Internalname = "CTLAMIGROUPOWNER";
         chkavCtlisactive_Internalname = "CTLISACTIVE";
         edtavAcceptinvitation_Internalname = "vACCEPTINVITATION";
         edtavDeclineinvitation_Internalname = "vDECLINEINVITATION";
         edtavDelete_Internalname = "vDELETE";
         edtavDeleteimage_Internalname = "vDELETEIMAGE";
         divMaintable_Internalname = "MAINTABLE";
         Form.Internalname = "FORM";
         subGridgroup_Internalname = "GRIDGROUP";
      }

      public override void initialize_properties( )
      {
         context.SetDefaultTheme("GeneXusUnanimo.UnanimoWeb", true);
         if ( context.isSpaRequest( ) )
         {
            disableJsOutput();
         }
         init_default_properties( ) ;
         subGridgroup_Allowcollapsing = 0;
         subGridgroup_Allowselection = 0;
         edtavDeleteimage_Link = "";
         subGridgroup_Header = "";
         edtavDeleteimage_Jsonclick = "";
         edtavDeleteimage_gximage = "";
         edtavDeleteimage_Visible = -1;
         edtavDelete_Jsonclick = "";
         edtavDelete_Enabled = 1;
         edtavDeclineinvitation_Jsonclick = "";
         edtavDeclineinvitation_Enabled = 1;
         edtavDeclineinvitation_Visible = -1;
         edtavAcceptinvitation_Jsonclick = "";
         edtavAcceptinvitation_Enabled = 1;
         edtavAcceptinvitation_Visible = -1;
         chkavCtlisactive.Caption = "";
         chkavCtlisactive.Enabled = 0;
         chkavCtlamigroupowner.Caption = "";
         chkavCtlamigroupowner.Enabled = 0;
         cmbavCtlgrouptype_Jsonclick = "";
         cmbavCtlgrouptype.Enabled = 0;
         edtavCtlgroupname_Jsonclick = "";
         edtavCtlgroupname_Link = "";
         edtavCtlgroupname_Enabled = 0;
         edtavCtlgroupid_Jsonclick = "";
         edtavCtlgroupid_Enabled = 0;
         subGridgroup_Class = "Grid";
         subGridgroup_Backcolorstyle = 0;
         chkavCtlisactive.Enabled = -1;
         chkavCtlamigroupowner.Enabled = -1;
         cmbavCtlgrouptype.Enabled = -1;
         edtavCtlgroupname_Enabled = -1;
         edtavCtlgroupid_Enabled = -1;
         Form.Headerrawhtml = "";
         Form.Background = "";
         Form.Textcolor = 0;
         Form.Backcolor = (int)(0xFFFFFF);
         Form.Caption = "Smart Groups";
         if ( context.isSpaRequest( ) )
         {
            enableJsOutput();
         }
      }

      public override bool SupportAjaxEvent( )
      {
         return true ;
      }

      public override void InitializeDynEvents( )
      {
         setEventMetadata("REFRESH","""{"handler":"Refresh","iparms":[{"av":"GRIDGROUP_nFirstRecordOnPage","type":"int"},{"av":"GRIDGROUP_nEOF","type":"int"},{"av":"AV38groupList","fld":"vGROUPLIST","grid":11,"type":""},{"av":"nGXsfl_11_idx","ctrl":"GRID","prop":"GridCurrRow","grid":11},{"av":"nRC_GXsfl_11","ctrl":"GRIDGROUP","prop":"GridRC","grid":11,"type":"int"}]}""");
         setEventMetadata("'RECOVER GROUPS FROM SERVER'","""{"handler":"E121F2","iparms":[{"av":"AV38groupList","fld":"vGROUPLIST","grid":11,"type":""},{"av":"nGXsfl_11_idx","ctrl":"GRID","prop":"GridCurrRow","grid":11},{"av":"GRIDGROUP_nFirstRecordOnPage","type":"int"},{"av":"nRC_GXsfl_11","ctrl":"GRIDGROUP","prop":"GridRC","grid":11,"type":"int"},{"av":"GRIDGROUP_nEOF","type":"int"}]""");
         setEventMetadata("'RECOVER GROUPS FROM SERVER'",""","oparms":[{"av":"AV38groupList","fld":"vGROUPLIST","grid":11,"type":""},{"av":"nGXsfl_11_idx","ctrl":"GRID","prop":"GridCurrRow","grid":11},{"av":"GRIDGROUP_nFirstRecordOnPage","type":"int"},{"av":"nRC_GXsfl_11","ctrl":"GRIDGROUP","prop":"GridRC","grid":11,"type":"int"}]}""");
         setEventMetadata("'CREATE GROUP'","""{"handler":"E111F1","iparms":[]}""");
         setEventMetadata("'GO TO GROUP'","""{"handler":"E151F2","iparms":[{"av":"AV38groupList","fld":"vGROUPLIST","grid":11,"type":""},{"av":"nGXsfl_11_idx","ctrl":"GRID","prop":"GridCurrRow","grid":11},{"av":"GRIDGROUP_nFirstRecordOnPage","type":"int"},{"av":"nRC_GXsfl_11","ctrl":"GRIDGROUP","prop":"GridRC","grid":11,"type":"int"}]}""");
         setEventMetadata("'DELETE GROUP'","""{"handler":"E161F2","iparms":[{"av":"AV38groupList","fld":"vGROUPLIST","grid":11,"type":""},{"av":"nGXsfl_11_idx","ctrl":"GRID","prop":"GridCurrRow","grid":11},{"av":"GRIDGROUP_nFirstRecordOnPage","type":"int"},{"av":"nRC_GXsfl_11","ctrl":"GRIDGROUP","prop":"GridRC","grid":11,"type":"int"}]""");
         setEventMetadata("'DELETE GROUP'",""","oparms":[{"av":"AV39selectedGroupId","fld":"vSELECTEDGROUPID","type":"guid"}]}""");
         setEventMetadata("GX.EXTENSIONS.WEB.DIALOGS.ONCONFIRMCLOSED","""{"handler":"E131F2","iparms":[{"av":"AV11UserResponse","fld":"vUSERRESPONSE","type":"boolean"},{"av":"AV39selectedGroupId","fld":"vSELECTEDGROUPID","type":"guid"},{"av":"AV38groupList","fld":"vGROUPLIST","grid":11,"type":""},{"av":"nGXsfl_11_idx","ctrl":"GRID","prop":"GridCurrRow","grid":11},{"av":"GRIDGROUP_nFirstRecordOnPage","type":"int"},{"av":"nRC_GXsfl_11","ctrl":"GRIDGROUP","prop":"GridRC","grid":11,"type":"int"},{"av":"GRIDGROUP_nEOF","type":"int"}]""");
         setEventMetadata("GX.EXTENSIONS.WEB.DIALOGS.ONCONFIRMCLOSED",""","oparms":[{"av":"AV38groupList","fld":"vGROUPLIST","grid":11,"type":""},{"av":"nGXsfl_11_idx","ctrl":"GRID","prop":"GridCurrRow","grid":11},{"av":"GRIDGROUP_nFirstRecordOnPage","type":"int"},{"av":"nRC_GXsfl_11","ctrl":"GRIDGROUP","prop":"GridRC","grid":11,"type":"int"}]}""");
         setEventMetadata("GRIDGROUP.LOAD","""{"handler":"E171F2","iparms":[{"av":"AV38groupList","fld":"vGROUPLIST","grid":11,"type":""},{"av":"nGXsfl_11_idx","ctrl":"GRID","prop":"GridCurrRow","grid":11},{"av":"GRIDGROUP_nFirstRecordOnPage","type":"int"},{"av":"nRC_GXsfl_11","ctrl":"GRIDGROUP","prop":"GridRC","grid":11,"type":"int"}]""");
         setEventMetadata("GRIDGROUP.LOAD",""","oparms":[{"av":"AV36deleteImage","fld":"vDELETEIMAGE","type":"bits"},{"av":"AV13acceptInvitation","fld":"vACCEPTINVITATION","type":"char"},{"av":"AV14declineInvitation","fld":"vDECLINEINVITATION","type":"char"},{"av":"edtavAcceptinvitation_Visible","ctrl":"vACCEPTINVITATION","prop":"Visible"},{"av":"edtavDeclineinvitation_Visible","ctrl":"vDECLINEINVITATION","prop":"Visible"},{"av":"edtavDeleteimage_Visible","ctrl":"vDELETEIMAGE","prop":"Visible"}]}""");
         setEventMetadata("'DECLINE INVITATION'","""{"handler":"E181F2","iparms":[{"av":"AV38groupList","fld":"vGROUPLIST","grid":11,"type":""},{"av":"nGXsfl_11_idx","ctrl":"GRID","prop":"GridCurrRow","grid":11},{"av":"GRIDGROUP_nFirstRecordOnPage","type":"int"},{"av":"nRC_GXsfl_11","ctrl":"GRIDGROUP","prop":"GridRC","grid":11,"type":"int"},{"av":"GRIDGROUP_nEOF","type":"int"}]""");
         setEventMetadata("'DECLINE INVITATION'",""","oparms":[{"av":"AV38groupList","fld":"vGROUPLIST","grid":11,"type":""},{"av":"nGXsfl_11_idx","ctrl":"GRID","prop":"GridCurrRow","grid":11},{"av":"GRIDGROUP_nFirstRecordOnPage","type":"int"},{"av":"nRC_GXsfl_11","ctrl":"GRIDGROUP","prop":"GridRC","grid":11,"type":"int"}]}""");
         setEventMetadata("'ACCEPT INVITATION'","""{"handler":"E191F2","iparms":[{"av":"AV38groupList","fld":"vGROUPLIST","grid":11,"type":""},{"av":"nGXsfl_11_idx","ctrl":"GRID","prop":"GridCurrRow","grid":11},{"av":"GRIDGROUP_nFirstRecordOnPage","type":"int"},{"av":"nRC_GXsfl_11","ctrl":"GRIDGROUP","prop":"GridRC","grid":11,"type":"int"},{"av":"GRIDGROUP_nEOF","type":"int"}]""");
         setEventMetadata("'ACCEPT INVITATION'",""","oparms":[{"av":"AV38groupList","fld":"vGROUPLIST","grid":11,"type":""},{"av":"nGXsfl_11_idx","ctrl":"GRID","prop":"GridCurrRow","grid":11},{"av":"GRIDGROUP_nFirstRecordOnPage","type":"int"},{"av":"nRC_GXsfl_11","ctrl":"GRIDGROUP","prop":"GridRC","grid":11,"type":"int"}]}""");
         setEventMetadata("VALIDV_GXV2","""{"handler":"Validv_Gxv2","iparms":[]}""");
         setEventMetadata("VALIDV_GXV4","""{"handler":"Validv_Gxv4","iparms":[]}""");
         setEventMetadata("NULL","""{"handler":"Validv_Deleteimage","iparms":[]}""");
         return  ;
      }

      public override void cleanup( )
      {
         CloseCursors();
         if ( IsMain )
         {
            context.CloseConnections();
         }
      }

      public override void initialize( )
      {
         gxfirstwebparm = "";
         gxfirstwebparm_bkp = "";
         AV38groupList = new GXBaseCollection<GeneXus.Programs.wallet.registered.SdtGroupListItem>( context, "GroupListItem", "distributedcryptography");
         sDynURL = "";
         FormProcess = "";
         bodyStyle = "";
         GXKey = "";
         AV39selectedGroupId = Guid.Empty;
         GX_FocusControl = "";
         Form = new GXWebForm();
         sPrefix = "";
         TempTags = "";
         ClassString = "";
         StyleString = "";
         bttRecovergroupsfromserver_Jsonclick = "";
         bttCreategroup_Jsonclick = "";
         GridgroupContainer = new GXWebGrid( context);
         sStyleString = "";
         sEvt = "";
         EvtGridId = "";
         EvtRowId = "";
         sEvtType = "";
         AV13acceptInvitation = "";
         AV14declineInvitation = "";
         AV10delete = "";
         AV36deleteImage = "";
         AV46Deleteimage_GXI = "";
         AV9websession = context.GetSession();
         AV7error = "";
         GXt_objcol_SdtGroupListItem2 = new GXBaseCollection<GeneXus.Programs.wallet.registered.SdtGroupListItem>( context, "GroupListItem", "distributedcryptography");
         GridgroupRow = new GXWebRow();
         GXt_char1 = "";
         BackMsgLst = new msglist();
         LclMsgLst = new msglist();
         subGridgroup_Linesclass = "";
         ROClassString = "";
         GXCCtl = "";
         sImgUrl = "";
         GridgroupColumn = new GXWebColumn();
         /* GeneXus formulas. */
         edtavCtlgroupid_Enabled = 0;
         edtavCtlgroupname_Enabled = 0;
         cmbavCtlgrouptype.Enabled = 0;
         chkavCtlamigroupowner.Enabled = 0;
         chkavCtlisactive.Enabled = 0;
         edtavAcceptinvitation_Enabled = 0;
         edtavDeclineinvitation_Enabled = 0;
         edtavDelete_Enabled = 0;
      }

      private short nGotPars ;
      private short GxWebError ;
      private short gxajaxcallmode ;
      private short wbEnd ;
      private short wbStart ;
      private short nDonePA ;
      private short gxcookieaux ;
      private short subGridgroup_Backcolorstyle ;
      private short GRIDGROUP_nEOF ;
      private short nGXWrapped ;
      private short subGridgroup_Backstyle ;
      private short subGridgroup_Titlebackstyle ;
      private short subGridgroup_Allowselection ;
      private short subGridgroup_Allowhovering ;
      private short subGridgroup_Allowcollapsing ;
      private short subGridgroup_Collapsed ;
      private int nRC_GXsfl_11 ;
      private int nGXsfl_11_idx=1 ;
      private int AV40GXV1 ;
      private int subGridgroup_Islastpage ;
      private int edtavCtlgroupid_Enabled ;
      private int edtavCtlgroupname_Enabled ;
      private int edtavAcceptinvitation_Enabled ;
      private int edtavDeclineinvitation_Enabled ;
      private int edtavDelete_Enabled ;
      private int nGXsfl_11_fel_idx=1 ;
      private int nGXsfl_11_bak_idx=1 ;
      private int edtavAcceptinvitation_Visible ;
      private int edtavDeclineinvitation_Visible ;
      private int edtavDeleteimage_Visible ;
      private int idxLst ;
      private int subGridgroup_Backcolor ;
      private int subGridgroup_Allbackcolor ;
      private int subGridgroup_Titlebackcolor ;
      private int subGridgroup_Selectedindex ;
      private int subGridgroup_Selectioncolor ;
      private int subGridgroup_Hoveringcolor ;
      private long GRIDGROUP_nCurrentRecord ;
      private long GRIDGROUP_nFirstRecordOnPage ;
      private string gxfirstwebparm ;
      private string gxfirstwebparm_bkp ;
      private string sGXsfl_11_idx="0001" ;
      private string sDynURL ;
      private string FormProcess ;
      private string bodyStyle ;
      private string GXKey ;
      private string GX_FocusControl ;
      private string sPrefix ;
      private string divMaintable_Internalname ;
      private string TempTags ;
      private string ClassString ;
      private string StyleString ;
      private string bttRecovergroupsfromserver_Internalname ;
      private string bttRecovergroupsfromserver_Jsonclick ;
      private string bttCreategroup_Internalname ;
      private string bttCreategroup_Jsonclick ;
      private string sStyleString ;
      private string subGridgroup_Internalname ;
      private string sEvt ;
      private string EvtGridId ;
      private string EvtRowId ;
      private string sEvtType ;
      private string AV13acceptInvitation ;
      private string edtavAcceptinvitation_Internalname ;
      private string AV14declineInvitation ;
      private string edtavDeclineinvitation_Internalname ;
      private string AV10delete ;
      private string edtavDelete_Internalname ;
      private string edtavDeleteimage_Internalname ;
      private string sGXsfl_11_fel_idx="0001" ;
      private string AV7error ;
      private string edtavDeleteimage_gximage ;
      private string GXt_char1 ;
      private string edtavCtlgroupid_Internalname ;
      private string edtavCtlgroupname_Internalname ;
      private string cmbavCtlgrouptype_Internalname ;
      private string chkavCtlamigroupowner_Internalname ;
      private string chkavCtlisactive_Internalname ;
      private string subGridgroup_Class ;
      private string subGridgroup_Linesclass ;
      private string ROClassString ;
      private string edtavCtlgroupid_Jsonclick ;
      private string edtavCtlgroupname_Link ;
      private string edtavCtlgroupname_Jsonclick ;
      private string GXCCtl ;
      private string cmbavCtlgrouptype_Jsonclick ;
      private string edtavAcceptinvitation_Jsonclick ;
      private string edtavDeclineinvitation_Jsonclick ;
      private string edtavDelete_Jsonclick ;
      private string sImgUrl ;
      private string edtavDeleteimage_Jsonclick ;
      private string subGridgroup_Header ;
      private string edtavDeleteimage_Link ;
      private bool entryPointCalled ;
      private bool toggleJsOutput ;
      private bool AV11UserResponse ;
      private bool wbLoad ;
      private bool Rfr0gs ;
      private bool wbErr ;
      private bool bGXsfl_11_Refreshing=false ;
      private bool gxdyncontrolsrefreshing ;
      private bool returnInSub ;
      private bool gx_BV11 ;
      private bool AV36deleteImage_IsBlob ;
      private string AV46Deleteimage_GXI ;
      private string AV36deleteImage ;
      private Guid AV39selectedGroupId ;
      private GXWebGrid GridgroupContainer ;
      private GXWebRow GridgroupRow ;
      private GXWebColumn GridgroupColumn ;
      private IGxSession AV9websession ;
      private GXWebForm Form ;
      private IGxDataStore dsDefault ;
      private GXCombobox cmbavCtlgrouptype ;
      private GXCheckbox chkavCtlamigroupowner ;
      private GXCheckbox chkavCtlisactive ;
      private GXBaseCollection<GeneXus.Programs.wallet.registered.SdtGroupListItem> AV38groupList ;
      private GXBaseCollection<GeneXus.Programs.wallet.registered.SdtGroupListItem> GXt_objcol_SdtGroupListItem2 ;
      private msglist BackMsgLst ;
      private msglist LclMsgLst ;
   }

}
