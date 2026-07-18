---
name: Azure Scholastic Enterprise
colors:
  surface: '#0b1326'
  surface-dim: '#0b1326'
  surface-bright: '#31394d'
  surface-container-lowest: '#060e20'
  surface-container-low: '#131b2e'
  surface-container: '#171f33'
  surface-container-high: '#222a3d'
  surface-container-highest: '#2d3449'
  on-surface: '#dae2fd'
  on-surface-variant: '#c2c6d6'
  inverse-surface: '#dae2fd'
  inverse-on-surface: '#283044'
  outline: '#8c909f'
  outline-variant: '#424754'
  surface-tint: '#adc6ff'
  primary: '#adc6ff'
  on-primary: '#002e6a'
  primary-container: '#4d8eff'
  on-primary-container: '#00285d'
  inverse-primary: '#005ac2'
  secondary: '#bcc7de'
  on-secondary: '#263143'
  secondary-container: '#3e495d'
  on-secondary-container: '#aeb9d0'
  tertiary: '#a4c9ff'
  on-tertiary: '#00315d'
  tertiary-container: '#4c93e7'
  on-tertiary-container: '#002a51'
  error: '#ffb4ab'
  on-error: '#690005'
  error-container: '#93000a'
  on-error-container: '#ffdad6'
  primary-fixed: '#d8e2ff'
  primary-fixed-dim: '#adc6ff'
  on-primary-fixed: '#001a42'
  on-primary-fixed-variant: '#004395'
  secondary-fixed: '#d8e3fb'
  secondary-fixed-dim: '#bcc7de'
  on-secondary-fixed: '#111c2d'
  on-secondary-fixed-variant: '#3c475a'
  tertiary-fixed: '#d4e3ff'
  tertiary-fixed-dim: '#a4c9ff'
  on-tertiary-fixed: '#001c39'
  on-tertiary-fixed-variant: '#004883'
  background: '#0b1326'
  on-background: '#dae2fd'
  surface-variant: '#2d3449'
typography:
  display-lg:
    fontFamily: Poppins
    fontSize: 32px
    fontWeight: '600'
    lineHeight: '1.2'
    letterSpacing: -0.02em
  display-md:
    fontFamily: Poppins
    fontSize: 24px
    fontWeight: '600'
    lineHeight: '1.3'
  headline-sm:
    fontFamily: Poppins
    fontSize: 18px
    fontWeight: '500'
    lineHeight: '1.4'
  body-lg:
    fontFamily: Inter
    fontSize: 16px
    fontWeight: '400'
    lineHeight: '1.5'
  body-md:
    fontFamily: Inter
    fontSize: 14px
    fontWeight: '400'
    lineHeight: '1.5'
  body-sm:
    fontFamily: Inter
    fontSize: 13px
    fontWeight: '400'
    lineHeight: '1.5'
  label-bold:
    fontFamily: Inter
    fontSize: 12px
    fontWeight: '600'
    lineHeight: '1'
    letterSpacing: 0.05em
  label-muted:
    fontFamily: Inter
    fontSize: 12px
    fontWeight: '400'
    lineHeight: '1'
  data-numeric:
    fontFamily: Inter
    fontSize: 20px
    fontWeight: '700'
    lineHeight: '1'
    letterSpacing: -0.01em
rounded:
  sm: 0.25rem
  DEFAULT: 0.5rem
  md: 0.75rem
  lg: 1rem
  xl: 1.5rem
  full: 9999px
spacing:
  base: 4px
  xs: 8px
  sm: 12px
  md: 16px
  lg: 24px
  xl: 32px
  gutter: 20px
  sidebar_width: 260px
---

## Brand & Style

The design system is engineered for high-stakes school administration within the Angolan educational sector. It projects an image of **stability, precision, and technological prestige**. The target audience—administrative directors and bursars—requires a tool that feels like professional fintech software rather than a casual web app.

### Design Style: Modern Enterprise Neo-Skeuomorphism
The visual language balances the efficiency of **Minimalism** with the depth of **Neo-skeuomorphism**. It utilizes a sophisticated dark theme to reduce eye strain during long administrative sessions. 
- **Subtle Depth:** Elements are not flat; they use inner glows and soft drop shadows to appear physically "pressed" into or "floating" above the surface.
- **Glassmorphism Accents:** Light-refracting borders and semi-transparent overlays provide a sense of layered hierarchy.
- **Windows Native Feel:** The layout follows the structural conventions of Windows desktop software, emphasizing robust side navigation and dense data density.

## Colors

The palette is a strictly disciplined range of deep indigos and vibrant blues. By replacing traditional warning colors (reds/oranges) with varying intensities of blue and teal, the interface maintains a calm, focused atmosphere even when displaying financial debts.

- **Primary Blue (#3B82F6):** Used for active states, primary buttons, and positive growth indicators.
- **Surface Layering:** 
    - `Level 0`: #0F172A (Main app canvas)
    - `Level 1`: #111827 (Sidebar navigation)
    - `Level 2`: #1E293B (Interactive cards and data containers)
- **Semantic Accents:** 
    - **Success/Positive:** Primary Blue or Vibrant Teal.
    - **Neutral/Debt:** Muted Blue-Greys or Deep Navy to signify "attention required" without triggering alarm.

## Typography

This design system uses a dual-font strategy to balance character with utility. 

- **Poppins** is reserved for structural headings and dashboard titles. Its geometric nature provides a premium, modern feel to the "high-level" views.
- **Inter** is the workhorse for all UI elements, data tables, and forms. It is chosen for its exceptional legibility in small sizes and its neutral, systematic aesthetic.

**Numeric Styling:** For financial figures (Kz), use the `data-numeric` style with tabular lining figures to ensure that columns of numbers align perfectly in financial reports.

## Layout & Spacing

As a Windows Desktop application, the layout is designed for high information density and precise mouse interactions.

- **Fixed Sidebar:** A 260px left-hand navigation remains constant, providing quick access to core modules (Recibos, Entradas, Saídas).
- **The Dashboard Grid:** A 12-column grid system is used within the main content area. Standard financial cards typically span 3 columns, while data-heavy charts span 6 to 8 columns.
- **Spacing Rhythm:** A 4px baseline grid ensures consistency. Components are separated by `lg` (24px) spacing to maintain the "premium" feel and avoid visual clutter.
- **Safe Areas:** A 32px margin is maintained around the entire application window to prevent the UI from feeling cramped against the OS frame.

## Elevation & Depth

Visual hierarchy is established through **Tonal Layering** and **Neo-skeuomorphic** depth effects.

- **The "Inner" Depth:** Cards use a subtle `1px` inner stroke (top and left) with a lighter blue tint (#FFFFFF at 5% opacity) to simulate a light source hitting the edge.
- **Soft Shadows:** Surfaces do not use harsh black shadows. Instead, use an extra-diffused shadow with a deep indigo tint (`#000000` at 40% opacity, `20px` blur, `10px` spread).
- **Active State Elevation:** When a menu item or button is selected, it should appear "pressed" (concave) or "illuminated" with a vibrant blue glow, rather than just changing color.
- **Backdrop Blurs:** Use 12px background blurs on modal overlays to maintain the sense of a continuous, deep workspace.

## Shapes

The shape language is characterized by "Elegant Softness." 

- **Containers:** Dashboard cards and the main sidebar container use a `1rem` (16px) corner radius to soften the technical nature of the software.
- **Input Fields/Buttons:** Smaller elements use a `0.5rem` (8px) radius to maintain a professional, sharp appearance while still feeling modern.
- **Icons:** Use linear icons with a 2px stroke weight and slightly rounded terminals to match the typography.

## Components

### Financial KPI Cards
These are the centerpiece of the system. Each card includes:
- A high-contrast icon container (Blue background).
- The primary metric in `data-numeric` styling.
- A "Growth/Trend" indicator at the bottom with a subtle glass effect background.

### Data Grids (Windows Style)
Data tables should be dense but readable. 
- **Row Hover:** Use a subtle background shift to `#2D3748`.
- **Zebra Striping:** Avoid traditional zebra stripes; use thin `1px` borders in `#1E293B` to separate entries.

### Primary Buttons
Buttons should feel tactile. 
- **Background:** Gradient from `#3B82F6` to `#2563EB`.
- **Shadow:** A soft blue glow (`0px 4px 12px rgba(59, 130, 246, 0.3)`).

### Side Navigation
- **Inactive:** Muted text with no background.
- **Active:** A solid blue background with a white icon, slightly rounded on the right-hand side to "point" towards the content area.

### Form Inputs
Inputs should appear "recessed" into the card surface.
- **Background:** `#111827` (slightly darker than the card).
- **Border:** `1px` solid `#334155`.
- **Focus State:** `2px` solid `#3B82F6` with a soft outer glow.