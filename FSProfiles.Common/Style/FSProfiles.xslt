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
          table {text-align: center; border: 1px solid; border-collapse: collapse; width:90%; margin-left:5%; margin-right:5%; margin-bottom: 25px}
          div {margin-left:5%; margin-right: 5%; margin-top: 10px; margin-bottom: 0px}
          th {border: 1px solid dimgrey}
          th.tableTitle {font-size: 18pt}
          td {border: 1px solid darkgrey}
          td.section {font-size: 15pt}
          .Primary {color: mediumblue}
          .Secondary {color: purple}
        </style>
      </head>
      <body>
        <h1>Microsoft Flight Simulator 2020 Bindings Report</h1>
        <xsl:variable name="controllers" select="BindingList/SelectedControllers" />
        <xsl:copy>
          <xsl:call-template  name="selected">
            <xsl:with-param name="controllers" select = "$controllers" />
          </xsl:call-template>
        </xsl:copy>
        <br />
        <xsl:variable name="profileCount" select="count($controllers/SelectedController)" />
        <p>Binding priority colors in the table below: <span class="Primary"> Primary</span> <span class="Secondary"> Secondary</span>
        </p>
        <table>
          <tr bgcolor="lightgrey">
            <th class="tableTitle">
              <xsl:attribute name="colspan">
                <xsl:value-of  select="$profileCount + 1" />
              </xsl:attribute> 
              Controller Bindings</th>
          </tr>
          <tr bgcolor="lightgrey">
            <th rowspan="2">Action</th>
            <th>
              <xsl:attribute name="colspan">
                <xsl:value-of  select="$profileCount" />
              </xsl:attribute>
              Binding Information</th>
          </tr>
          <tr bgcolor="whitesmoke">
            <xsl:copy>
              <xsl:call-template name="profile">
                <xsl:with-param name="controllers" select = "$controllers" />
              </xsl:call-template>
            </xsl:copy>
          </tr>

          <xsl:for-each select="BindingList/Contexts/FSContext">
            <tr>
              <xsl:attribute name="bgcolor">
                <xsl:value-of select="@BackColor" />
              </xsl:attribute>
              <td class="section" align="center">
                <xsl:attribute name="colspan">
                  <xsl:value-of  select="$profileCount + 1" />
                </xsl:attribute>
                <xsl:value-of select="@ContextName"/>
              </td>
            </tr>
            <xsl:for-each select="Action">
              <xsl:variable name="action" select="."/>
              <tr>
                <xsl:attribute name="bgcolor">
                  <xsl:value-of select="$action/@BackColor" />
                </xsl:attribute>
                <td>
                  <xsl:value-of select="$action/@ActionName"/>
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
                        <td />
                      </xsl:otherwise>
                    </xsl:choose>
                  </xsl:for-each>
                </xsl:copy>
              </tr>
            </xsl:for-each>
          </xsl:for-each>
        </table>
        <div>
          <p>
            Copyright 2024, Ian Darroch<br/>
            Source repository: <a href="https://github.com/iadarroch/FSProfiles" target="_blank">https://github.com/iadarroch/FSProfiles</a>
          </p>
        </div>
      </body>
    </html>
  </xsl:template>

  <xsl:template name="selected">
    <xsl:param name="controllers" />
    <table>
      <tr bgcolor="antiquewhite">
        <th class="tableTitle" colspan="3">Selected Controller Profiles</th>
      </tr>
      <tr bgcolor="antiquewhite">
        <th>Controller</th>
        <th>Profile</th>
        <th>Folder</th>
      </tr>
      <xsl:for-each select="$controllers/SelectedController">
        <tr bgcolor="floralwhite">
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
        <xsl:value-of select="DeviceName"/>
        <br />
        <xsl:value-of select="ProfileName"/>
      </td>
    </xsl:for-each>
  </xsl:template>

</xsl:stylesheet>