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
namespace GeneXus.Programs.wallet.registered {
   public class promptcontac : GXDataArea
   {
      public promptcontac( )
      {
         context = new GxContext(  );
         DataStoreUtil.LoadDataStores( context);
         dsDefault = context.GetDataStore("Default");
         IsMain = true;
         context.SetDefaultTheme("GeneXusUnanimo.UnanimoWeb", true);
      }

      public promptcontac( IGxContext context )
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
            else if ( StringUtil.StrCmp(gxfirstwebparm, "gxajaxNewRow_"+"Contactgrid") == 0 )
            {
               gxnrContactgrid_newrow_invoke( ) ;
               return  ;
            }
            else if ( StringUtil.StrCmp(gxfirstwebparm, "gxajaxGridRefresh_"+"Contactgrid") == 0 )
            {
               gxgrContactgrid_refresh_invoke( ) ;
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

      protected void gxnrContactgrid_newrow_invoke( )
      {
         nRC_GXsfl_6 = (int)(Math.Round(NumberUtil.Val( GetPar( "nRC_GXsfl_6"), "."), 18, MidpointRounding.ToEven));
         nGXsfl_6_idx = (int)(Math.Round(NumberUtil.Val( GetPar( "nGXsfl_6_idx"), "."), 18, MidpointRounding.ToEven));
         sGXsfl_6_idx = GetPar( "sGXsfl_6_idx");
         setAjaxCallMode();
         if ( ! IsValidAjaxCall( true) )
         {
            GxWebError = 1;
            return  ;
         }
         gxnrContactgrid_newrow( ) ;
         /* End function gxnrContactgrid_newrow_invoke */
      }

      protected void gxgrContactgrid_refresh_invoke( )
      {
         ajax_req_read_hidden_sdt(GetNextPar( ), AV27wallet);
         setAjaxCallMode();
         if ( ! IsValidAjaxCall( true) )
         {
            GxWebError = 1;
            return  ;
         }
         gxgrContactgrid_refresh( AV27wallet) ;
         AddString( context.getJSONResponse( )) ;
         /* End function gxgrContactgrid_refresh_invoke */
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
         PA1K2( ) ;
         gxajaxcallmode = (short)((isAjaxCallMode( ) ? 1 : 0));
         if ( ( gxajaxcallmode == 0 ) && ( GxWebError == 0 ) )
         {
            START1K2( ) ;
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
            context.AddJavascriptSource("json2.js", "?"+context.GetBuildNumber( 123260), false, true);
         }
         context.AddJavascriptSource("jquery.js", "?"+context.GetBuildNumber( 123260), false, true);
         context.AddJavascriptSource("gxgral.js", "?"+context.GetBuildNumber( 123260), false, true);
         context.AddJavascriptSource("gxcfg.js", "?"+GetCacheInvalidationToken( ), false, true);
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
         context.WriteHtmlTextNl( "<form id=\"MAINFORM\" autocomplete=\"off\" name=\"MAINFORM\" method=\"post\" tabindex=-1  class=\"form-horizontal Form\" data-gx-class=\"form-horizontal Form\" novalidate action=\""+formatLink("wallet.registered.promptcontac") +"\">") ;
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
         if ( context.isAjaxRequest( ) )
         {
            context.httpAjaxContext.ajax_rsp_assign_sdt_attri("", false, "vWALLET", AV27wallet);
         }
         else
         {
            context.httpAjaxContext.ajax_rsp_assign_hidden_sdt("vWALLET", AV27wallet);
         }
         GxWebStd.gx_hidden_field( context, "gxhash_vWALLET", GetSecureSignedToken( "", AV27wallet, context));
         GXKey = Decrypt64( context.GetCookie( "GX_SESSION_ID"), Crypto.GetServerKey( ));
      }

      protected void SendCloseFormHiddens( )
      {
         /* Send hidden variables. */
         /* Send saved values. */
         send_integrity_footer_hashes( ) ;
         if ( context.isAjaxRequest( ) )
         {
            context.httpAjaxContext.ajax_rsp_assign_sdt_attri("", false, "Allcontacts", AV6allContacts);
         }
         else
         {
            context.httpAjaxContext.ajax_rsp_assign_hidden_sdt("Allcontacts", AV6allContacts);
         }
         GxWebStd.gx_hidden_field( context, "nRC_GXsfl_6", StringUtil.LTrim( StringUtil.NToC( (decimal)(nRC_GXsfl_6), 8, 0, ".", "")));
         if ( context.isAjaxRequest( ) )
         {
            context.httpAjaxContext.ajax_rsp_assign_sdt_attri("", false, "vALLCONTACTS", AV6allContacts);
         }
         else
         {
            context.httpAjaxContext.ajax_rsp_assign_hidden_sdt("vALLCONTACTS", AV6allContacts);
         }
         if ( context.isAjaxRequest( ) )
         {
            context.httpAjaxContext.ajax_rsp_assign_sdt_attri("", false, "vWALLET", AV27wallet);
         }
         else
         {
            context.httpAjaxContext.ajax_rsp_assign_hidden_sdt("vWALLET", AV27wallet);
         }
         GxWebStd.gx_hidden_field( context, "gxhash_vWALLET", GetSecureSignedToken( "", AV27wallet, context));
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
            WE1K2( ) ;
            context.WriteHtmlText( "</div>") ;
         }
      }

      public override void DispatchEvents( )
      {
         EVT1K2( ) ;
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
         return formatLink("wallet.registered.promptcontac")  ;
      }

      public override string GetPgmname( )
      {
         return "Wallet.registered.PromptContac" ;
      }

      public override string GetPgmdesc( )
      {
         return "Prompt Contac" ;
      }

      protected void WB1K0( )
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
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "col-xs-12", "start", "top", "", "", "div");
            /*  Grid Control  */
            ContactgridContainer.SetWrapped(nGXWrapped);
            StartGridControl6( ) ;
         }
         if ( wbEnd == 6 )
         {
            wbEnd = 0;
            nRC_GXsfl_6 = (int)(nGXsfl_6_idx-1);
            if ( ContactgridContainer.GetWrapped() == 1 )
            {
               context.WriteHtmlText( "</table>") ;
               context.WriteHtmlText( "</div>") ;
            }
            else
            {
               AV28GXV1 = nGXsfl_6_idx;
               sStyleString = "";
               context.WriteHtmlText( "<div id=\""+"ContactgridContainer"+"Div\" "+sStyleString+">"+"</div>") ;
               context.httpAjaxContext.ajax_rsp_assign_grid("_"+"Contactgrid", ContactgridContainer, subContactgrid_Internalname);
               if ( ! context.isAjaxRequest( ) && ! context.isSpaRequest( ) )
               {
                  GxWebStd.gx_hidden_field( context, "ContactgridContainerData", ContactgridContainer.ToJavascriptSource());
               }
               if ( context.isAjaxRequest( ) || context.isSpaRequest( ) )
               {
                  GxWebStd.gx_hidden_field( context, "ContactgridContainerData"+"V", ContactgridContainer.GridValuesHidden());
               }
               else
               {
                  context.WriteHtmlText( "<input type=\"hidden\" "+"name=\""+"ContactgridContainerData"+"V"+"\" value='"+ContactgridContainer.GridValuesHidden()+"'/>") ;
               }
            }
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "row", "start", "top", "", "", "div");
            /* Div Control */
            GxWebStd.gx_div_start( context, "", 1, 0, "px", 0, "px", "col-xs-12", "start", "top", "", "", "div");
            TempTags = "  onfocus=\"gx.evt.onfocus(this, 12,'',false,'',0)\"";
            ClassString = "Button";
            StyleString = "";
            GxWebStd.gx_button_ctrl( context, bttClose_Internalname, "gx.evt.setGridEvt("+StringUtil.Str( (decimal)(6), 1, 0)+","+"null"+");", "Close", bttClose_Jsonclick, 5, "Close", "", StyleString, ClassString, 1, 1, "standard", "'"+""+"'"+",false,"+"'"+"E\\'CLOSE\\'."+"'", TempTags, "", context.GetButtonType( ), "HLP_Wallet/registered/PromptContac.htm");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
            GxWebStd.gx_div_end( context, "start", "top", "div");
         }
         if ( wbEnd == 6 )
         {
            wbEnd = 0;
            if ( isFullAjaxMode( ) )
            {
               if ( ContactgridContainer.GetWrapped() == 1 )
               {
                  context.WriteHtmlText( "</table>") ;
                  context.WriteHtmlText( "</div>") ;
               }
               else
               {
                  AV28GXV1 = nGXsfl_6_idx;
                  sStyleString = "";
                  context.WriteHtmlText( "<div id=\""+"ContactgridContainer"+"Div\" "+sStyleString+">"+"</div>") ;
                  context.httpAjaxContext.ajax_rsp_assign_grid("_"+"Contactgrid", ContactgridContainer, subContactgrid_Internalname);
                  if ( ! context.isAjaxRequest( ) && ! context.isSpaRequest( ) )
                  {
                     GxWebStd.gx_hidden_field( context, "ContactgridContainerData", ContactgridContainer.ToJavascriptSource());
                  }
                  if ( context.isAjaxRequest( ) || context.isSpaRequest( ) )
                  {
                     GxWebStd.gx_hidden_field( context, "ContactgridContainerData"+"V", ContactgridContainer.GridValuesHidden());
                  }
                  else
                  {
                     context.WriteHtmlText( "<input type=\"hidden\" "+"name=\""+"ContactgridContainerData"+"V"+"\" value='"+ContactgridContainer.GridValuesHidden()+"'/>") ;
                  }
               }
            }
         }
         wbLoad = true;
      }

      protected void START1K2( )
      {
         wbLoad = false;
         wbEnd = 0;
         wbStart = 0;
         if ( ! context.isSpaRequest( ) )
         {
            if ( context.ExposeMetadata( ) )
            {
               Form.Meta.addItem("generator", "GeneXus .NET 18_0_12-186073", 0) ;
            }
         }
         Form.Meta.addItem("description", "Prompt Contac", 0) ;
         context.wjLoc = "";
         context.nUserReturn = 0;
         context.wbHandled = 0;
         if ( StringUtil.StrCmp(context.GetRequestMethod( ), "POST") == 0 )
         {
         }
         wbErr = false;
         STRUP1K0( ) ;
      }

      protected void WS1K2( )
      {
         START1K2( ) ;
         EVT1K2( ) ;
      }

      protected void EVT1K2( )
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
                           else if ( StringUtil.StrCmp(sEvt, "'CLOSE'") == 0 )
                           {
                              context.wbHandled = 1;
                              dynload_actions( ) ;
                              /* Execute user event: 'Close' */
                              E111K2 ();
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
                           if ( ( StringUtil.StrCmp(StringUtil.Left( sEvt, 5), "START") == 0 ) || ( StringUtil.StrCmp(StringUtil.Left( sEvt, 16), "CONTACTGRID.LOAD") == 0 ) || ( StringUtil.StrCmp(StringUtil.Left( sEvt, 16), "'SELECT CONTACT'") == 0 ) || ( StringUtil.StrCmp(StringUtil.Left( sEvt, 5), "ENTER") == 0 ) || ( StringUtil.StrCmp(StringUtil.Left( sEvt, 6), "CANCEL") == 0 ) || ( StringUtil.StrCmp(StringUtil.Left( sEvt, 16), "'SELECT CONTACT'") == 0 ) )
                           {
                              nGXsfl_6_idx = (int)(Math.Round(NumberUtil.Val( sEvtType, "."), 18, MidpointRounding.ToEven));
                              sGXsfl_6_idx = StringUtil.PadL( StringUtil.LTrimStr( (decimal)(nGXsfl_6_idx), 4, 0), 4, "0");
                              SubsflControlProps_62( ) ;
                              AV28GXV1 = nGXsfl_6_idx;
                              if ( ( AV6allContacts.Count >= AV28GXV1 ) && ( AV28GXV1 > 0 ) )
                              {
                                 AV6allContacts.CurrentItem = ((GeneXus.Programs.wallet.registered.SdtContact_SDT)AV6allContacts.Item(AV28GXV1));
                                 AV25selectContact = cgiGet( edtavSelectcontact_Internalname);
                                 AssignAttri("", false, edtavSelectcontact_Internalname, AV25selectContact);
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
                                    E121K2 ();
                                 }
                                 else if ( StringUtil.StrCmp(sEvt, "CONTACTGRID.LOAD") == 0 )
                                 {
                                    context.wbHandled = 1;
                                    dynload_actions( ) ;
                                    /* Execute user event: Contactgrid.Load */
                                    E131K2 ();
                                 }
                                 else if ( StringUtil.StrCmp(sEvt, "'SELECT CONTACT'") == 0 )
                                 {
                                    context.wbHandled = 1;
                                    dynload_actions( ) ;
                                    /* Execute user event: 'Select Contact' */
                                    E141K2 ();
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

      protected void WE1K2( )
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

      protected void PA1K2( )
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

      protected void gxnrContactgrid_newrow( )
      {
         GxWebStd.set_html_headers( context, 0, "", "");
         SubsflControlProps_62( ) ;
         while ( nGXsfl_6_idx <= nRC_GXsfl_6 )
         {
            sendrow_62( ) ;
            nGXsfl_6_idx = ((subContactgrid_Islastpage==1)&&(nGXsfl_6_idx+1>subContactgrid_fnc_Recordsperpage( )) ? 1 : nGXsfl_6_idx+1);
            sGXsfl_6_idx = StringUtil.PadL( StringUtil.LTrimStr( (decimal)(nGXsfl_6_idx), 4, 0), 4, "0");
            SubsflControlProps_62( ) ;
         }
         AddString( context.httpAjaxContext.getJSONContainerResponse( ContactgridContainer)) ;
         /* End function gxnrContactgrid_newrow */
      }

      protected void gxgrContactgrid_refresh( GeneXus.Programs.wallet.SdtWallet AV27wallet )
      {
         initialize_formulas( ) ;
         GxWebStd.set_html_headers( context, 0, "", "");
         CONTACTGRID_nCurrentRecord = 0;
         RF1K2( ) ;
         GXKey = Decrypt64( context.GetCookie( "GX_SESSION_ID"), Crypto.GetServerKey( ));
         send_integrity_footer_hashes( ) ;
         GXKey = Decrypt64( context.GetCookie( "GX_SESSION_ID"), Crypto.GetServerKey( ));
         /* End function gxgrContactgrid_refresh */
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
         RF1K2( ) ;
         if ( isFullAjaxMode( ) )
         {
            send_integrity_footer_hashes( ) ;
         }
      }

      protected void initialize_formulas( )
      {
         /* GeneXus formulas. */
         edtavCtluserprivatename1_Enabled = 0;
         edtavCtlusername1_Enabled = 0;
         edtavSelectcontact_Enabled = 0;
      }

      protected void RF1K2( )
      {
         initialize_formulas( ) ;
         clear_multi_value_controls( ) ;
         if ( isAjaxCallMode( ) )
         {
            ContactgridContainer.ClearRows();
         }
         wbStart = 6;
         nGXsfl_6_idx = 1;
         sGXsfl_6_idx = StringUtil.PadL( StringUtil.LTrimStr( (decimal)(nGXsfl_6_idx), 4, 0), 4, "0");
         SubsflControlProps_62( ) ;
         bGXsfl_6_Refreshing = true;
         ContactgridContainer.AddObjectProperty("GridName", "Contactgrid");
         ContactgridContainer.AddObjectProperty("CmpContext", "");
         ContactgridContainer.AddObjectProperty("InMasterPage", "false");
         ContactgridContainer.AddObjectProperty("Class", "Grid");
         ContactgridContainer.AddObjectProperty("Cellpadding", StringUtil.LTrim( StringUtil.NToC( (decimal)(1), 4, 0, ".", "")));
         ContactgridContainer.AddObjectProperty("Cellspacing", StringUtil.LTrim( StringUtil.NToC( (decimal)(2), 4, 0, ".", "")));
         ContactgridContainer.AddObjectProperty("Backcolorstyle", StringUtil.LTrim( StringUtil.NToC( (decimal)(subContactgrid_Backcolorstyle), 1, 0, ".", "")));
         ContactgridContainer.PageSize = subContactgrid_fnc_Recordsperpage( );
         gxdyncontrolsrefreshing = true;
         fix_multi_value_controls( ) ;
         gxdyncontrolsrefreshing = false;
         if ( ! context.WillRedirect( ) && ( context.nUserReturn != 1 ) )
         {
            SubsflControlProps_62( ) ;
            /* Execute user event: Contactgrid.Load */
            E131K2 ();
            wbEnd = 6;
            WB1K0( ) ;
         }
         bGXsfl_6_Refreshing = true;
      }

      protected void send_integrity_lvl_hashes1K2( )
      {
         if ( context.isAjaxRequest( ) )
         {
            context.httpAjaxContext.ajax_rsp_assign_sdt_attri("", false, "vWALLET", AV27wallet);
         }
         else
         {
            context.httpAjaxContext.ajax_rsp_assign_hidden_sdt("vWALLET", AV27wallet);
         }
         GxWebStd.gx_hidden_field( context, "gxhash_vWALLET", GetSecureSignedToken( "", AV27wallet, context));
      }

      protected int subContactgrid_fnc_Pagecount( )
      {
         return (int)(-1) ;
      }

      protected int subContactgrid_fnc_Recordcount( )
      {
         return (int)(-1) ;
      }

      protected int subContactgrid_fnc_Recordsperpage( )
      {
         return (int)(-1) ;
      }

      protected int subContactgrid_fnc_Currentpage( )
      {
         return (int)(-1) ;
      }

      protected void before_start_formulas( )
      {
         edtavCtluserprivatename1_Enabled = 0;
         edtavCtlusername1_Enabled = 0;
         edtavSelectcontact_Enabled = 0;
         fix_multi_value_controls( ) ;
      }

      protected void STRUP1K0( )
      {
         /* Before Start, stand alone formulas. */
         before_start_formulas( ) ;
         /* Execute Start event if defined. */
         context.wbGlbDoneStart = 0;
         /* Execute user event: Start */
         E121K2 ();
         context.wbGlbDoneStart = 1;
         /* After Start, stand alone formulas. */
         if ( StringUtil.StrCmp(context.GetRequestMethod( ), "POST") == 0 )
         {
            /* Read saved SDTs. */
            ajax_req_read_hidden_sdt(cgiGet( "Allcontacts"), AV6allContacts);
            ajax_req_read_hidden_sdt(cgiGet( "vALLCONTACTS"), AV6allContacts);
            /* Read saved values. */
            nRC_GXsfl_6 = (int)(Math.Round(context.localUtil.CToN( cgiGet( "nRC_GXsfl_6"), ".", ","), 18, MidpointRounding.ToEven));
            nRC_GXsfl_6 = (int)(Math.Round(context.localUtil.CToN( cgiGet( "nRC_GXsfl_6"), ".", ","), 18, MidpointRounding.ToEven));
            nGXsfl_6_fel_idx = 0;
            while ( nGXsfl_6_fel_idx < nRC_GXsfl_6 )
            {
               nGXsfl_6_fel_idx = ((subContactgrid_Islastpage==1)&&(nGXsfl_6_fel_idx+1>subContactgrid_fnc_Recordsperpage( )) ? 1 : nGXsfl_6_fel_idx+1);
               sGXsfl_6_fel_idx = StringUtil.PadL( StringUtil.LTrimStr( (decimal)(nGXsfl_6_fel_idx), 4, 0), 4, "0");
               SubsflControlProps_fel_62( ) ;
               AV28GXV1 = nGXsfl_6_fel_idx;
               if ( ( AV6allContacts.Count >= AV28GXV1 ) && ( AV28GXV1 > 0 ) )
               {
                  AV6allContacts.CurrentItem = ((GeneXus.Programs.wallet.registered.SdtContact_SDT)AV6allContacts.Item(AV28GXV1));
                  AV25selectContact = cgiGet( edtavSelectcontact_Internalname);
               }
            }
            if ( nGXsfl_6_fel_idx == 0 )
            {
               nGXsfl_6_idx = 1;
               sGXsfl_6_idx = StringUtil.PadL( StringUtil.LTrimStr( (decimal)(nGXsfl_6_idx), 4, 0), 4, "0");
               SubsflControlProps_62( ) ;
            }
            nGXsfl_6_fel_idx = 1;
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
         E121K2 ();
         if (returnInSub) return;
      }

      protected void E121K2( )
      {
         /* Start Routine */
         returnInSub = false;
         GXt_SdtWallet1 = AV27wallet;
         new GeneXus.Programs.wallet.getwallet(context ).execute( out  GXt_SdtWallet1) ;
         AV27wallet = GXt_SdtWallet1;
         AV24websession.Set("Contact_SDT_SELECTED", "");
         /* Execute user subroutine: 'INIT CONACTS FROM FILE' */
         S112 ();
         if (returnInSub) return;
      }

      private void E131K2( )
      {
         /* Contactgrid_Load Routine */
         returnInSub = false;
         AV28GXV1 = 1;
         while ( AV28GXV1 <= AV6allContacts.Count )
         {
            AV6allContacts.CurrentItem = ((GeneXus.Programs.wallet.registered.SdtContact_SDT)AV6allContacts.Item(AV28GXV1));
            AV25selectContact = "Select Contact";
            AssignAttri("", false, edtavSelectcontact_Internalname, AV25selectContact);
            /* Load Method */
            if ( wbStart != -1 )
            {
               wbStart = 6;
            }
            sendrow_62( ) ;
            if ( isFullAjaxMode( ) && ! bGXsfl_6_Refreshing )
            {
               DoAjaxLoad(6, ContactgridRow);
            }
            AV28GXV1 = (int)(AV28GXV1+1);
         }
         /*  Sending Event outputs  */
      }

      protected void E141K2( )
      {
         AV28GXV1 = nGXsfl_6_idx;
         if ( ( AV28GXV1 > 0 ) && ( AV6allContacts.Count >= AV28GXV1 ) )
         {
            AV6allContacts.CurrentItem = ((GeneXus.Programs.wallet.registered.SdtContact_SDT)AV6allContacts.Item(AV28GXV1));
         }
         /* 'Select Contact' Routine */
         returnInSub = false;
         GXt_char2 = AV26networkType;
         new GeneXus.Programs.nbitcoin.getnetworktypefromaddress(context ).execute(  ((GeneXus.Programs.wallet.registered.SdtContact_SDT)(AV6allContacts.CurrentItem)).gxTpr_Username, out  GXt_char2) ;
         AV26networkType = GXt_char2;
         if ( StringUtil.StrCmp(AV27wallet.gxTpr_Networktype, AV26networkType) == 0 )
         {
            AV24websession.Set("Contact_SDT_SELECTED", ((GeneXus.Programs.wallet.registered.SdtContact_SDT)(AV6allContacts.CurrentItem)).ToJSonString(false, true));
            context.setWebReturnParms(new Object[] {});
            context.setWebReturnParmsMetadata(new Object[] {});
            context.wjLocDisableFrm = 1;
            context.nUserReturn = 1;
            returnInSub = true;
            if (true) return;
         }
         else
         {
            GX_msglist.addItem("The contact you are trying to add is on a different network than your current wallet");
            GX_msglist.addItem("You are on "+StringUtil.RTrim( context.localUtil.Format( AV27wallet.gxTpr_Networktype, ""))+" meantime your contact is on "+StringUtil.RTrim( context.localUtil.Format( AV26networkType, "")));
         }
      }

      protected void S112( )
      {
         /* 'INIT CONACTS FROM FILE' Routine */
         returnInSub = false;
         AV6allContacts.Clear();
         gx_BV6 = true;
         AV7allContacts_temp.FromJSonString(new GeneXus.Programs.wallet.readjsonencfile(context).executeUdp(  "contacts.enc", out  AV14error), null);
         AV31GXV4 = 1;
         while ( AV31GXV4 <= AV7allContacts_temp.Count )
         {
            AV9contact = ((GeneXus.Programs.wallet.registered.SdtContact_SDT)AV7allContacts_temp.Item(AV31GXV4));
            if ( ! AV9contact.gxTpr_Invitationdeclined )
            {
               AV6allContacts.Add(AV9contact, 0);
               gx_BV6 = true;
            }
            AV31GXV4 = (int)(AV31GXV4+1);
         }
      }

      protected void E111K2( )
      {
         /* 'Close' Routine */
         returnInSub = false;
         AV24websession.Set("Contact_SDT_SELECTED", "");
         context.setWebReturnParms(new Object[] {});
         context.setWebReturnParmsMetadata(new Object[] {});
         context.wjLocDisableFrm = 1;
         context.nUserReturn = 1;
         returnInSub = true;
         if (true) return;
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
         PA1K2( ) ;
         WS1K2( ) ;
         WE1K2( ) ;
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
            context.AddJavascriptSource(StringUtil.RTrim( ((string)Form.Jscriptsrc.Item(idxLst))), "?202541817522055", true, true);
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
         context.AddJavascriptSource("messages.eng.js", "?"+GetCacheInvalidationToken( ), false, true);
         context.AddJavascriptSource("wallet/registered/promptcontac.js", "?202541817522055", false, true);
         /* End function include_jscripts */
      }

      protected void SubsflControlProps_62( )
      {
         edtavCtluserprivatename1_Internalname = "CTLUSERPRIVATENAME1_"+sGXsfl_6_idx;
         edtavCtlusername1_Internalname = "CTLUSERNAME1_"+sGXsfl_6_idx;
         edtavSelectcontact_Internalname = "vSELECTCONTACT_"+sGXsfl_6_idx;
      }

      protected void SubsflControlProps_fel_62( )
      {
         edtavCtluserprivatename1_Internalname = "CTLUSERPRIVATENAME1_"+sGXsfl_6_fel_idx;
         edtavCtlusername1_Internalname = "CTLUSERNAME1_"+sGXsfl_6_fel_idx;
         edtavSelectcontact_Internalname = "vSELECTCONTACT_"+sGXsfl_6_fel_idx;
      }

      protected void sendrow_62( )
      {
         sGXsfl_6_idx = StringUtil.PadL( StringUtil.LTrimStr( (decimal)(nGXsfl_6_idx), 4, 0), 4, "0");
         SubsflControlProps_62( ) ;
         WB1K0( ) ;
         ContactgridRow = GXWebRow.GetNew(context,ContactgridContainer);
         if ( subContactgrid_Backcolorstyle == 0 )
         {
            /* None style subfile background logic. */
            subContactgrid_Backstyle = 0;
            if ( StringUtil.StrCmp(subContactgrid_Class, "") != 0 )
            {
               subContactgrid_Linesclass = subContactgrid_Class+"Odd";
            }
         }
         else if ( subContactgrid_Backcolorstyle == 1 )
         {
            /* Uniform style subfile background logic. */
            subContactgrid_Backstyle = 0;
            subContactgrid_Backcolor = subContactgrid_Allbackcolor;
            if ( StringUtil.StrCmp(subContactgrid_Class, "") != 0 )
            {
               subContactgrid_Linesclass = subContactgrid_Class+"Uniform";
            }
         }
         else if ( subContactgrid_Backcolorstyle == 2 )
         {
            /* Header style subfile background logic. */
            subContactgrid_Backstyle = 1;
            if ( StringUtil.StrCmp(subContactgrid_Class, "") != 0 )
            {
               subContactgrid_Linesclass = subContactgrid_Class+"Odd";
            }
            subContactgrid_Backcolor = (int)(0x0);
         }
         else if ( subContactgrid_Backcolorstyle == 3 )
         {
            /* Report style subfile background logic. */
            subContactgrid_Backstyle = 1;
            if ( ((int)((nGXsfl_6_idx) % (2))) == 0 )
            {
               subContactgrid_Backcolor = (int)(0x0);
               if ( StringUtil.StrCmp(subContactgrid_Class, "") != 0 )
               {
                  subContactgrid_Linesclass = subContactgrid_Class+"Even";
               }
            }
            else
            {
               subContactgrid_Backcolor = (int)(0x0);
               if ( StringUtil.StrCmp(subContactgrid_Class, "") != 0 )
               {
                  subContactgrid_Linesclass = subContactgrid_Class+"Odd";
               }
            }
         }
         if ( ContactgridContainer.GetWrapped() == 1 )
         {
            context.WriteHtmlText( "<tr ") ;
            context.WriteHtmlText( " class=\""+"Grid"+"\" style=\""+""+"\"") ;
            context.WriteHtmlText( " gxrow=\""+sGXsfl_6_idx+"\">") ;
         }
         /* Subfile cell */
         if ( ContactgridContainer.GetWrapped() == 1 )
         {
            context.WriteHtmlText( "<td valign=\"middle\" align=\""+"start"+"\""+" style=\""+""+"\">") ;
         }
         /* Single line edit */
         TempTags = "  onfocus=\"gx.evt.onfocus(this, 7,'',false,'" + sGXsfl_6_idx + "',6)\"";
         ROClassString = "Attribute";
         ContactgridRow.AddColumnProperties("edit", 1, isAjaxCallMode( ), new Object[] {(string)edtavCtluserprivatename1_Internalname,StringUtil.RTrim( ((GeneXus.Programs.wallet.registered.SdtContact_SDT)AV6allContacts.Item(AV28GXV1)).gxTpr_Userprivatename),(string)"",TempTags+" onchange=\""+""+";gx.evt.onchange(this, event)\" "+" onblur=\""+""+";gx.evt.onblur(this,7);\"",(string)"'"+""+"'"+",false,"+"'"+""+"'",(string)"",(string)"",(string)"",(string)"",(string)edtavCtluserprivatename1_Jsonclick,(short)0,(string)"Attribute",(string)"",(string)ROClassString,(string)"",(string)"",(short)-1,(int)edtavCtluserprivatename1_Enabled,(short)0,(string)"text",(string)"",(short)0,(string)"px",(short)17,(string)"px",(short)250,(short)0,(short)0,(short)6,(short)0,(short)-1,(short)-1,(bool)true,(string)"",(string)"start",(bool)true,(string)""});
         /* Subfile cell */
         if ( ContactgridContainer.GetWrapped() == 1 )
         {
            context.WriteHtmlText( "<td valign=\"middle\" align=\""+"start"+"\""+" style=\""+""+"\">") ;
         }
         /* Single line edit */
         TempTags = "  onfocus=\"gx.evt.onfocus(this, 8,'',false,'" + sGXsfl_6_idx + "',6)\"";
         ROClassString = "Attribute";
         ContactgridRow.AddColumnProperties("edit", 1, isAjaxCallMode( ), new Object[] {(string)edtavCtlusername1_Internalname,StringUtil.RTrim( ((GeneXus.Programs.wallet.registered.SdtContact_SDT)AV6allContacts.Item(AV28GXV1)).gxTpr_Username),(string)"",TempTags+" onchange=\""+""+";gx.evt.onchange(this, event)\" "+" onblur=\""+""+";gx.evt.onblur(this,8);\"",(string)"'"+""+"'"+",false,"+"'"+""+"'",(string)"",(string)"",(string)"",(string)"",(string)edtavCtlusername1_Jsonclick,(short)0,(string)"Attribute",(string)"",(string)ROClassString,(string)"",(string)"",(short)-1,(int)edtavCtlusername1_Enabled,(short)0,(string)"text",(string)"",(short)0,(string)"px",(short)17,(string)"px",(short)250,(short)0,(short)0,(short)6,(short)0,(short)-1,(short)-1,(bool)true,(string)"",(string)"start",(bool)true,(string)""});
         /* Subfile cell */
         if ( ContactgridContainer.GetWrapped() == 1 )
         {
            context.WriteHtmlText( "<td valign=\"middle\" align=\""+"start"+"\""+" style=\""+""+"\">") ;
         }
         /* Single line edit */
         TempTags = "  onfocus=\"gx.evt.onfocus(this, 9,'',false,'" + sGXsfl_6_idx + "',6)\"";
         ROClassString = "Attribute";
         ContactgridRow.AddColumnProperties("edit", 1, isAjaxCallMode( ), new Object[] {(string)edtavSelectcontact_Internalname,StringUtil.RTrim( AV25selectContact),(string)"",TempTags+" onchange=\""+""+";gx.evt.onchange(this, event)\" "+" onblur=\""+""+";gx.evt.onblur(this,9);\"","'"+""+"'"+",false,"+"'"+"E\\'SELECT CONTACT\\'."+sGXsfl_6_idx+"'",(string)"",(string)"",(string)"",(string)"",(string)edtavSelectcontact_Jsonclick,(short)5,(string)"Attribute",(string)"",(string)ROClassString,(string)"",(string)"",(short)-1,(int)edtavSelectcontact_Enabled,(short)0,(string)"text",(string)"",(short)0,(string)"px",(short)17,(string)"px",(short)20,(short)0,(short)0,(short)6,(short)0,(short)-1,(short)-1,(bool)true,(string)"",(string)"start",(bool)true,(string)""});
         send_integrity_lvl_hashes1K2( ) ;
         ContactgridContainer.AddRow(ContactgridRow);
         nGXsfl_6_idx = ((subContactgrid_Islastpage==1)&&(nGXsfl_6_idx+1>subContactgrid_fnc_Recordsperpage( )) ? 1 : nGXsfl_6_idx+1);
         sGXsfl_6_idx = StringUtil.PadL( StringUtil.LTrimStr( (decimal)(nGXsfl_6_idx), 4, 0), 4, "0");
         SubsflControlProps_62( ) ;
         /* End function sendrow_62 */
      }

      protected void init_web_controls( )
      {
         /* End function init_web_controls */
      }

      protected void StartGridControl6( )
      {
         if ( ContactgridContainer.GetWrapped() == 1 )
         {
            context.WriteHtmlText( "<div id=\""+"ContactgridContainer"+"DivS\" data-gxgridid=\"6\">") ;
            sStyleString = "";
            GxWebStd.gx_table_start( context, subContactgrid_Internalname, subContactgrid_Internalname, "", "Grid", 0, "", "", 1, 2, sStyleString, "", "", 0);
            /* Subfile titles */
            context.WriteHtmlText( "<tr") ;
            context.WriteHtmlTextNl( ">") ;
            if ( subContactgrid_Backcolorstyle == 0 )
            {
               subContactgrid_Titlebackstyle = 0;
               if ( StringUtil.Len( subContactgrid_Class) > 0 )
               {
                  subContactgrid_Linesclass = subContactgrid_Class+"Title";
               }
            }
            else
            {
               subContactgrid_Titlebackstyle = 1;
               if ( subContactgrid_Backcolorstyle == 1 )
               {
                  subContactgrid_Titlebackcolor = subContactgrid_Allbackcolor;
                  if ( StringUtil.Len( subContactgrid_Class) > 0 )
                  {
                     subContactgrid_Linesclass = subContactgrid_Class+"UniformTitle";
                  }
               }
               else
               {
                  if ( StringUtil.Len( subContactgrid_Class) > 0 )
                  {
                     subContactgrid_Linesclass = subContactgrid_Class+"Title";
                  }
               }
            }
            context.WriteHtmlText( "<th align=\""+"start"+"\" "+" nowrap=\"nowrap\" "+" class=\""+"Attribute"+"\" "+" style=\""+""+""+"\" "+">") ;
            context.SendWebValue( "Contact  Name") ;
            context.WriteHtmlTextNl( "</th>") ;
            context.WriteHtmlText( "<th align=\""+"start"+"\" "+" nowrap=\"nowrap\" "+" class=\""+"Attribute"+"\" "+" style=\""+""+""+"\" "+">") ;
            context.SendWebValue( "User Name") ;
            context.WriteHtmlTextNl( "</th>") ;
            context.WriteHtmlText( "<th align=\""+"start"+"\" "+" nowrap=\"nowrap\" "+" class=\""+"Attribute"+"\" "+" style=\""+""+""+"\" "+">") ;
            context.SendWebValue( "") ;
            context.WriteHtmlTextNl( "</th>") ;
            context.WriteHtmlTextNl( "</tr>") ;
            ContactgridContainer.AddObjectProperty("GridName", "Contactgrid");
         }
         else
         {
            ContactgridContainer.AddObjectProperty("GridName", "Contactgrid");
            ContactgridContainer.AddObjectProperty("Header", subContactgrid_Header);
            ContactgridContainer.AddObjectProperty("Class", "Grid");
            ContactgridContainer.AddObjectProperty("Cellpadding", StringUtil.LTrim( StringUtil.NToC( (decimal)(1), 4, 0, ".", "")));
            ContactgridContainer.AddObjectProperty("Cellspacing", StringUtil.LTrim( StringUtil.NToC( (decimal)(2), 4, 0, ".", "")));
            ContactgridContainer.AddObjectProperty("Backcolorstyle", StringUtil.LTrim( StringUtil.NToC( (decimal)(subContactgrid_Backcolorstyle), 1, 0, ".", "")));
            ContactgridContainer.AddObjectProperty("CmpContext", "");
            ContactgridContainer.AddObjectProperty("InMasterPage", "false");
            ContactgridColumn = GXWebColumn.GetNew(isAjaxCallMode( ));
            ContactgridColumn.AddObjectProperty("Enabled", StringUtil.LTrim( StringUtil.NToC( (decimal)(edtavCtluserprivatename1_Enabled), 5, 0, ".", "")));
            ContactgridContainer.AddColumnProperties(ContactgridColumn);
            ContactgridColumn = GXWebColumn.GetNew(isAjaxCallMode( ));
            ContactgridColumn.AddObjectProperty("Enabled", StringUtil.LTrim( StringUtil.NToC( (decimal)(edtavCtlusername1_Enabled), 5, 0, ".", "")));
            ContactgridContainer.AddColumnProperties(ContactgridColumn);
            ContactgridColumn = GXWebColumn.GetNew(isAjaxCallMode( ));
            ContactgridColumn.AddObjectProperty("Value", GXUtil.ValueEncode( StringUtil.RTrim( AV25selectContact)));
            ContactgridColumn.AddObjectProperty("Enabled", StringUtil.LTrim( StringUtil.NToC( (decimal)(edtavSelectcontact_Enabled), 5, 0, ".", "")));
            ContactgridContainer.AddColumnProperties(ContactgridColumn);
            ContactgridContainer.AddObjectProperty("Selectedindex", StringUtil.LTrim( StringUtil.NToC( (decimal)(subContactgrid_Selectedindex), 4, 0, ".", "")));
            ContactgridContainer.AddObjectProperty("Allowselection", StringUtil.LTrim( StringUtil.NToC( (decimal)(subContactgrid_Allowselection), 1, 0, ".", "")));
            ContactgridContainer.AddObjectProperty("Selectioncolor", StringUtil.LTrim( StringUtil.NToC( (decimal)(subContactgrid_Selectioncolor), 9, 0, ".", "")));
            ContactgridContainer.AddObjectProperty("Allowhover", StringUtil.LTrim( StringUtil.NToC( (decimal)(subContactgrid_Allowhovering), 1, 0, ".", "")));
            ContactgridContainer.AddObjectProperty("Hovercolor", StringUtil.LTrim( StringUtil.NToC( (decimal)(subContactgrid_Hoveringcolor), 9, 0, ".", "")));
            ContactgridContainer.AddObjectProperty("Allowcollapsing", StringUtil.LTrim( StringUtil.NToC( (decimal)(subContactgrid_Allowcollapsing), 1, 0, ".", "")));
            ContactgridContainer.AddObjectProperty("Collapsed", StringUtil.LTrim( StringUtil.NToC( (decimal)(subContactgrid_Collapsed), 1, 0, ".", "")));
         }
      }

      protected void init_default_properties( )
      {
         edtavCtluserprivatename1_Internalname = "CTLUSERPRIVATENAME1";
         edtavCtlusername1_Internalname = "CTLUSERNAME1";
         edtavSelectcontact_Internalname = "vSELECTCONTACT";
         bttClose_Internalname = "CLOSE";
         divMaintable_Internalname = "MAINTABLE";
         Form.Internalname = "FORM";
         subContactgrid_Internalname = "CONTACTGRID";
      }

      public override void initialize_properties( )
      {
         context.SetDefaultTheme("GeneXusUnanimo.UnanimoWeb", true);
         if ( context.isSpaRequest( ) )
         {
            disableJsOutput();
         }
         init_default_properties( ) ;
         subContactgrid_Allowcollapsing = 0;
         subContactgrid_Allowselection = 0;
         subContactgrid_Header = "";
         edtavSelectcontact_Jsonclick = "";
         edtavSelectcontact_Enabled = 1;
         edtavCtlusername1_Jsonclick = "";
         edtavCtlusername1_Enabled = 0;
         edtavCtluserprivatename1_Jsonclick = "";
         edtavCtluserprivatename1_Enabled = 0;
         subContactgrid_Class = "Grid";
         subContactgrid_Backcolorstyle = 0;
         edtavCtlusername1_Enabled = -1;
         edtavCtluserprivatename1_Enabled = -1;
         Form.Headerrawhtml = "";
         Form.Background = "";
         Form.Textcolor = 0;
         Form.Backcolor = (int)(0xFFFFFF);
         Form.Caption = "Prompt Contac";
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
         setEventMetadata("REFRESH","""{"handler":"Refresh","iparms":[{"av":"CONTACTGRID_nFirstRecordOnPage","type":"int"},{"av":"CONTACTGRID_nEOF","type":"int"},{"av":"AV6allContacts","fld":"vALLCONTACTS","grid":6,"type":""},{"av":"nGXsfl_6_idx","ctrl":"GRID","prop":"GridCurrRow","grid":6},{"av":"nRC_GXsfl_6","ctrl":"CONTACTGRID","prop":"GridRC","grid":6,"type":"int"},{"av":"AV27wallet","fld":"vWALLET","hsh":true,"type":""}]}""");
         setEventMetadata("CONTACTGRID.LOAD","""{"handler":"E131K2","iparms":[]""");
         setEventMetadata("CONTACTGRID.LOAD",""","oparms":[{"av":"AV25selectContact","fld":"vSELECTCONTACT","type":"char"}]}""");
         setEventMetadata("'SELECT CONTACT'","""{"handler":"E141K2","iparms":[{"av":"AV6allContacts","fld":"vALLCONTACTS","grid":6,"type":""},{"av":"nGXsfl_6_idx","ctrl":"GRID","prop":"GridCurrRow","grid":6},{"av":"CONTACTGRID_nFirstRecordOnPage","type":"int"},{"av":"nRC_GXsfl_6","ctrl":"CONTACTGRID","prop":"GridRC","grid":6,"type":"int"},{"av":"AV27wallet","fld":"vWALLET","hsh":true,"type":""}]}""");
         setEventMetadata("'CLOSE'","""{"handler":"E111K2","iparms":[]}""");
         setEventMetadata("NULL","""{"handler":"Validv_Selectcontact","iparms":[]}""");
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
         AV27wallet = new GeneXus.Programs.wallet.SdtWallet(context);
         sDynURL = "";
         FormProcess = "";
         bodyStyle = "";
         GXKey = "";
         AV6allContacts = new GXBaseCollection<GeneXus.Programs.wallet.registered.SdtContact_SDT>( context, "Contact_SDT", "distributedcryptography");
         GX_FocusControl = "";
         Form = new GXWebForm();
         sPrefix = "";
         ContactgridContainer = new GXWebGrid( context);
         sStyleString = "";
         TempTags = "";
         ClassString = "";
         StyleString = "";
         bttClose_Jsonclick = "";
         sEvt = "";
         EvtGridId = "";
         EvtRowId = "";
         sEvtType = "";
         AV25selectContact = "";
         GXt_SdtWallet1 = new GeneXus.Programs.wallet.SdtWallet(context);
         AV24websession = context.GetSession();
         ContactgridRow = new GXWebRow();
         AV26networkType = "";
         GXt_char2 = "";
         AV7allContacts_temp = new GXBaseCollection<GeneXus.Programs.wallet.registered.SdtContact_SDT>( context, "Contact_SDT", "distributedcryptography");
         AV14error = "";
         AV9contact = new GeneXus.Programs.wallet.registered.SdtContact_SDT(context);
         BackMsgLst = new msglist();
         LclMsgLst = new msglist();
         subContactgrid_Linesclass = "";
         ROClassString = "";
         ContactgridColumn = new GXWebColumn();
         /* GeneXus formulas. */
         edtavCtluserprivatename1_Enabled = 0;
         edtavCtlusername1_Enabled = 0;
         edtavSelectcontact_Enabled = 0;
      }

      private short nGotPars ;
      private short GxWebError ;
      private short gxajaxcallmode ;
      private short wbEnd ;
      private short wbStart ;
      private short nDonePA ;
      private short gxcookieaux ;
      private short subContactgrid_Backcolorstyle ;
      private short nGXWrapped ;
      private short subContactgrid_Backstyle ;
      private short subContactgrid_Titlebackstyle ;
      private short subContactgrid_Allowselection ;
      private short subContactgrid_Allowhovering ;
      private short subContactgrid_Allowcollapsing ;
      private short subContactgrid_Collapsed ;
      private short CONTACTGRID_nEOF ;
      private int nRC_GXsfl_6 ;
      private int nGXsfl_6_idx=1 ;
      private int AV28GXV1 ;
      private int subContactgrid_Islastpage ;
      private int edtavCtluserprivatename1_Enabled ;
      private int edtavCtlusername1_Enabled ;
      private int edtavSelectcontact_Enabled ;
      private int nGXsfl_6_fel_idx=1 ;
      private int AV31GXV4 ;
      private int idxLst ;
      private int subContactgrid_Backcolor ;
      private int subContactgrid_Allbackcolor ;
      private int subContactgrid_Titlebackcolor ;
      private int subContactgrid_Selectedindex ;
      private int subContactgrid_Selectioncolor ;
      private int subContactgrid_Hoveringcolor ;
      private long CONTACTGRID_nCurrentRecord ;
      private long CONTACTGRID_nFirstRecordOnPage ;
      private string gxfirstwebparm ;
      private string gxfirstwebparm_bkp ;
      private string sGXsfl_6_idx="0001" ;
      private string sDynURL ;
      private string FormProcess ;
      private string bodyStyle ;
      private string GXKey ;
      private string GX_FocusControl ;
      private string sPrefix ;
      private string divMaintable_Internalname ;
      private string sStyleString ;
      private string subContactgrid_Internalname ;
      private string TempTags ;
      private string ClassString ;
      private string StyleString ;
      private string bttClose_Internalname ;
      private string bttClose_Jsonclick ;
      private string sEvt ;
      private string EvtGridId ;
      private string EvtRowId ;
      private string sEvtType ;
      private string AV25selectContact ;
      private string edtavSelectcontact_Internalname ;
      private string sGXsfl_6_fel_idx="0001" ;
      private string AV26networkType ;
      private string GXt_char2 ;
      private string AV14error ;
      private string edtavCtluserprivatename1_Internalname ;
      private string edtavCtlusername1_Internalname ;
      private string subContactgrid_Class ;
      private string subContactgrid_Linesclass ;
      private string ROClassString ;
      private string edtavCtluserprivatename1_Jsonclick ;
      private string edtavCtlusername1_Jsonclick ;
      private string edtavSelectcontact_Jsonclick ;
      private string subContactgrid_Header ;
      private bool entryPointCalled ;
      private bool toggleJsOutput ;
      private bool wbLoad ;
      private bool Rfr0gs ;
      private bool wbErr ;
      private bool bGXsfl_6_Refreshing=false ;
      private bool gxdyncontrolsrefreshing ;
      private bool returnInSub ;
      private bool gx_BV6 ;
      private GXWebGrid ContactgridContainer ;
      private GXWebRow ContactgridRow ;
      private GXWebColumn ContactgridColumn ;
      private IGxSession AV24websession ;
      private GXWebForm Form ;
      private IGxDataStore dsDefault ;
      private GeneXus.Programs.wallet.SdtWallet AV27wallet ;
      private GXBaseCollection<GeneXus.Programs.wallet.registered.SdtContact_SDT> AV6allContacts ;
      private GeneXus.Programs.wallet.SdtWallet GXt_SdtWallet1 ;
      private GXBaseCollection<GeneXus.Programs.wallet.registered.SdtContact_SDT> AV7allContacts_temp ;
      private GeneXus.Programs.wallet.registered.SdtContact_SDT AV9contact ;
      private msglist BackMsgLst ;
      private msglist LclMsgLst ;
   }

}
