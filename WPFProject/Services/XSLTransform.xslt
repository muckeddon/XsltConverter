<?xml version="1.0" encoding="utf-8"?>
<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform"
    xmlns:msxsl="urn:schemas-microsoft-com:xslt" exclude-result-prefixes="msxsl"
>
  <xsl:output method="xml" indent="yes"/>

  <xsl:template match="list">
    <groups>
      <xsl:apply-templates>
        <xsl:sort select="name" order="ascending"/>
      </xsl:apply-templates>
    </groups>
  </xsl:template>

  <xsl:template match="item">
    <xsl:choose>
      <xsl:when
        test="preceding-sibling::item[@group=current()/@group]"
      />
      <xsl:otherwise>
        <group name="{@group}">
          <xsl:copy-of
            select="self::node() | following-sibling::item[@group=current()/@group]"
          />
        </group>
      </xsl:otherwise>
    </xsl:choose>
  </xsl:template>
</xsl:stylesheet>
