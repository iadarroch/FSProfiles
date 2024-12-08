<?xml version="1.0" encoding="utf-8"?>
<xsl:stylesheet version="1.0" 
                xmlns:xsl="http://www.w3.org/1999/XSL/Transform" 
                xmlns:exsl="http://exslt.org/common"
                extension-element-prefixes="exsl">
  <xsl:output method="html" encoding="utf-8" indent="yes" />
  
  <xsl:template match="/">
    <xsl:text disable-output-escaping='yes'>&lt;!DOCTYPE html&gt;</xsl:text>
    <html>
      <head>
        <style>
          body {font-family: Arial, Helvetica, sans-serif}
          h1 {text-align: center;}
          p {text-align: center;}
          table {text-align: center; border: 1px solid; border-collapse: collapse; width:96%; margin-left:2%; margin-right:2%; margin-bottom: 25px}
          div {margin-left:5%; margin-right: 5%; margin-top: 10px; margin-bottom: 0px}
          th {border: 1px solid dimgrey}
          th.tableTitle {font-size: 18pt}
          td {border: 1px solid darkgrey}
          td.section,
          td.context {font-size: 15pt; text-align: left}
          td.sectionAction,
          td.contextAction {font-size: 11pt; text-align: left; padding-left: 2%}
          td.subSection {font-size: 13pt; text-align: left; padding-left: 2%}
          td.subSectionAction {font-size: 11pt; text-align: left; padding-left: 4%}
          .Primary {color: mediumblue}
          .Secondary {color: purple}
        </style>
      </head>
      <body>
        <h1>Microsoft Flight Simulator 2020 Bindings Report - <xsl:value-of select="BindingReport/ContentMode" /></h1>
        <xsl:variable name="controllers" select="BindingReport/SelectedControllers" />
        <xsl:copy>
          <xsl:call-template name="selected">
            <xsl:with-param name="controllers" select = "$controllers" />
          </xsl:call-template>
        </xsl:copy>
        <br />
        <xsl:variable name="profileCount" select="count($controllers/SelectedController)" />
        <p>Binding priority colors in the table below: <span class="Primary"> Primary</span> <span class="Secondary"> Secondary</span>
        </p>
        
        <xsl:call-template name="KnownBindings">
          <xsl:with-param name="bindingList" select = "BindingReport/BindingList" />
          <xsl:with-param name="controllers" select = "$controllers" />
        </xsl:call-template>
        
        <xsl:if test="BindingReport/UnrecognisedContexts/Context">
          <p />
          <xsl:call-template name="UnrecognisedContexts">
            <xsl:with-param name="contexts" select = "BindingReport/UnrecognisedContexts/Context" />
            <xsl:with-param name="controllers" select = "$controllers" />
          </xsl:call-template>
        </xsl:if>
        <div>
          <p>
            Copyright 2024, Ian Darroch<br/>
            Source repository: <a href="https://github.com/iadarroch/FSProfiles" target="_blank">https://github.com/iadarroch/FSProfiles</a>
          </p>
        </div>
      </body>
    </html>
  </xsl:template>

  <xsl:template name="UnrecognisedContexts">
    <xsl:param name="contexts" />
    <xsl:param name="controllers" />
    <xsl:variable name="profileCount" select="count($controllers/SelectedController)" />

    <table>
      <xsl:call-template name="TableHeader">
        <xsl:with-param name="tableTitle">Other Controller Bindings</xsl:with-param>
        <xsl:with-param name="controllers" select = "$controllers" />
      </xsl:call-template>

      <xsl:for-each select="$contexts">
        <tr>
          <xsl:attribute name="bgcolor">
            <xsl:value-of select="@BackColor" />
          </xsl:attribute>
          <td class="context">
            <xsl:attribute name="colspan">
              <xsl:value-of select="$profileCount + 1" />
            </xsl:attribute>
            <xsl:attribute name="bgcolor">
              <xsl:value-of select="@BackColor" />
            </xsl:attribute>
            <xsl:value-of select="@ContextName"/>
          </td>
        </tr>
        <xsl:for-each select="Action">
          <xsl:variable name="action" select="." />
          <tr>
            <xsl:attribute name="bgcolor">
              <xsl:value-of select="@BackColor" />
            </xsl:attribute>
            <td class="contextAction">
              <xsl:value-of select="@ActionName"/>
            </td>
            <xsl:copy>
              <xsl:for-each select="$controllers/SelectedController">
                <xsl:variable name="curControl" select="./DeviceName" />
                <xsl:variable name="curProfile" select="./ProfileName" />
                <xsl:variable name="binding" select="$action/Binding[@Controller = $curControl and @Profile = $curProfile]" />
                <xsl:choose>
                  <xsl:when test="$binding">
                    <td>
                      <xsl:for-each select="$binding">
                        <xsl:if test="position() > 1">
                          <br/>
                        </xsl:if>
                        <span>
                          <xsl:attribute name="class">
                            <xsl:value-of select="@Priority"/>
                          </xsl:attribute>
                          <xsl:value-of select="@KeyCombo"/>
                        </span>
                      </xsl:for-each>
                    </td>
                  </xsl:when>
                  <xsl:otherwise>
                    <td>&#8203;</td>
                  </xsl:otherwise>
                </xsl:choose>
              </xsl:for-each>
            </xsl:copy>

          </tr>
        </xsl:for-each>
      </xsl:for-each>
    </table>
  </xsl:template>
  
  <xsl:template name="KnownBindings">
    <xsl:param name="bindingList" />
    <xsl:param name="controllers" />
    <xsl:variable name="profileCount" select="count($controllers/SelectedController)" />
    <table>
      <xsl:call-template name="TableHeader">
        <xsl:with-param name="tableTitle">Controls Options Bindings</xsl:with-param>
        <xsl:with-param name="controllers" select = "$controllers" />
      </xsl:call-template>

      <xsl:for-each select="$bindingList/Section">
        <tr>
          <xsl:attribute name="bgcolor">
            <xsl:value-of select="@BackColor" />
          </xsl:attribute>
          <td class="section">
            <xsl:attribute name="colspan">
              <xsl:value-of select="$profileCount + 1" />
            </xsl:attribute>
            <xsl:value-of select="@SectionName"/>
          </td>
        </tr>
        <xsl:for-each select="./*">
          <xsl:choose>
            <xsl:when test="name() = 'SubSection'">
              <xsl:call-template name="SubSection">
                <xsl:with-param name="subSection" select = "." />
                <xsl:with-param name="controllers" select = "$controllers" />
              </xsl:call-template>
            </xsl:when>
            <xsl:otherwise>
              <xsl:call-template name="SectionAction">
                <xsl:with-param name="action" select = "." />
                <xsl:with-param name="controllers" select = "$controllers" />
                <xsl:with-param name="styleClass" select = "'sectionAction'" />
              </xsl:call-template>
            </xsl:otherwise>
          </xsl:choose>
        </xsl:for-each>
      </xsl:for-each>
    </table>
  </xsl:template>
  
  <xsl:template name="SubSection">
    <xsl:param name="subSection" />
    <xsl:param name="controllers" />
    <xsl:variable name="profileCount" select="count($controllers/SelectedController)" />
    <tr>
      <xsl:attribute name="bgcolor">
        <xsl:value-of select="$subSection/@BackColor" />
      </xsl:attribute>
      <td class="subSection">
        <xsl:attribute name="colspan">
          <xsl:value-of select="$profileCount + 1" />
        </xsl:attribute>
        <xsl:value-of select="$subSection/@SubSectionName"/>
      </td>
    </tr>
    <xsl:for-each select="$subSection/*">
      <xsl:call-template name="SectionAction">
        <xsl:with-param name="action" select = "." />
        <xsl:with-param name="controllers" select = "$controllers" />
        <xsl:with-param name="styleClass" select = "'subSectionAction'" />
      </xsl:call-template>
    </xsl:for-each>
  </xsl:template>

  <xsl:template name="SectionAction">
    <xsl:param name="action" />
    <xsl:param name="controllers" />
    <xsl:param name="styleClass" />
    <xsl:variable name="inputCount" select="count($action/ActionInput)" />
    <xsl:for-each select="$action/ActionInput">
      <xsl:variable name="actionInput" select="." />
      <tr>
        <xsl:attribute name="class">
          <xsl:value-of select="$styleClass" />
        </xsl:attribute>
        <xsl:attribute name="bgcolor">
          <xsl:value-of select="$action/@BackColor" />
        </xsl:attribute>
        <xsl:if test="position() = 1">
          <td>
            <xsl:attribute name="class">
              <xsl:value-of select="$styleClass" />
            </xsl:attribute>
            <xsl:attribute name="rowspan">
              <xsl:value-of select="$inputCount" />
            </xsl:attribute>
            <xsl:value-of select="$action/@ActionName"/>
          </td>
        </xsl:if>
        <xsl:copy>
          <xsl:for-each select="$controllers/SelectedController">
            <xsl:variable name="curControl" select="./DeviceName" />
            <xsl:variable name="curProfile" select="./ProfileName" />
            <xsl:variable name="binding" select="$actionInput/Binding[@Controller = $curControl and @Profile = $curProfile]" />
            <xsl:choose>
              <xsl:when test="$binding">
                <td>
                  <xsl:for-each select="$binding">
                    <xsl:if test="position() > 1">
                      <br/>
                    </xsl:if>
                    <span>
                      <xsl:attribute name="class">
                        <xsl:value-of select="@Priority"/>
                      </xsl:attribute>
                      <xsl:value-of select="@KeyCombo"/>
                    </span>
                  </xsl:for-each>
                </td>
              </xsl:when>
              <xsl:otherwise>
                <td>&#8203;</td>
              </xsl:otherwise>
            </xsl:choose>
          </xsl:for-each>
        </xsl:copy>
      </tr>
    </xsl:for-each>
  </xsl:template>

  <xsl:template name="TableHeader">
    <xsl:param name="tableTitle" />
    <xsl:param name="controllers" />
    <xsl:variable name="profileCount" select="count($controllers/SelectedController)" />
    <tr bgcolor="lightgrey">
      <th class="tableTitle">
        <xsl:attribute name="colspan">
          <xsl:value-of select="$profileCount + 1" />
        </xsl:attribute>
        <xsl:value-of select="$tableTitle" />
      </th>
    </tr>
    <tr bgcolor="lightgrey">
      <th rowspan="2">Action</th>
      <th>
        <xsl:attribute name="colspan">
          <xsl:value-of select="$profileCount" />
        </xsl:attribute>
        Binding Information
      </th>
    </tr>
    <tr bgcolor="whitesmoke">
      <xsl:copy>
        <xsl:call-template name="profile">
          <xsl:with-param name="controllers" select = "$controllers" />
        </xsl:call-template>
      </xsl:copy>
    </tr>
  </xsl:template>
  
  <xsl:template name="selected">
    <xsl:param name="controllers" />
    <table>
      <tr bgcolor="antiquewhite">
        <th class="tableTitle" colspan="4">Selected Controller Profiles</th>
      </tr>
      <tr bgcolor="antiquewhite">
        <th>Host Version</th>
        <th>Controller</th>
        <th>Profile</th>
        <th>Folder/File</th>
      </tr>
      <xsl:for-each select="$controllers/SelectedController">
        <tr bgcolor="floralwhite">
          <td>
            <xsl:value-of select="HostVersionName"/>
          </td>
          <td>
            <xsl:value-of select="DeviceName"/>
          </td>
          <td>
            <xsl:value-of select="ProfileName"/>
          </td>
          <td>
            <xsl:value-of select="ProfilePath"/>
          </td>
        </tr>
      </xsl:for-each>
    </table>
  </xsl:template>

  <xsl:template name="profile">
    <xsl:param name="controllers" />
    <xsl:for-each select="$controllers/SelectedController">
      <td>
        <xsl:value-of select="HostVersionName"/>
        <br />
        <xsl:value-of select="DeviceName"/>
        <br />
        <xsl:value-of select="ProfileName"/>
      </td>
    </xsl:for-each>
  </xsl:template>

</xsl:stylesheet>