# Fractal Tree Desktop Wallpaper - User Guide

> **Note**: This document is AI-translated from the original Chinese version. If you find any errors or unclear expressions, please refer to the original `help.md` file.

## Introduction

`Fractal Tree Desktop Wallpaper` can randomly generate a beautiful tree. Thanks to its powerful parameter system, it can simulate **almost any type** of tree in the world, including those that **exist in nature** and those that **only exist in your imagination**.

### What is a `Fractal Tree`?

A `Fractal Tree` is a **recursively generated** tree structure — each branch has multiple sub-branches, each sub-branch contains multiple sub-branches, and so on, resulting in a tree-like structure.  
In `Fractal Tree Desktop Wallpaper`, to reduce rendering computation and improve controllability, we only use binary trees, meaning each branch has at most two sub-branches.

To achieve a tree structure that is as realistic as possible, `Fractal Tree Desktop Wallpaper` provides numerous parameters to control the distance and angle between nodes, line colors, and more.

---

## Getting Started with Parameters

If you're new to `Fractal Tree Desktop Wallpaper`, don't be intimidated by the many parameters — you only need to adjust a few core parameters to create a unique tree. Try modifying the `Branch Angle Range` and `Length Decay Range` to see how the tree's shape changes; then adjust the **color** in `Leaf Settings` to add color to your tree. Once you're familiar with the basic operations, you can further explore **Parameter Groups** and **Distribution Curves** to make each tree a true work of art.

Here's some basic parameter tutorials below ↓

### 1. Branch Angle Range

`Branch Angle Range` controls the range of deflection angles (in degrees) of sub-branches relative to their parent branch. The distribution curve controls the probability of angle values within the range (of course, distribution curves are beyond the scope of this basic tutorial, so just keep the default uniform distribution).

- When `Branch Angle Range` is **generally large**, the tree crown appears **sparse and fluffy**.
- When `Branch Angle Range` is **generally small**, the tree crown appears **dense and compact**.
- When `Branch Angle Range` has a **wide interval**, the tree crown appears **chaotic and disorderly**.
- When `Branch Angle Range` has a **narrow interval**, the tree crown appears **neat and orderly**.

### 2. Length Decay Range

`Length Decay Range` controls the range of the ratio of sub-branch length to parent branch length. The distribution curve controls the probability of ratio values within the range (again, distribution curves are beyond the scope of this basic tutorial, so just keep the default uniform distribution).

For example, if a branch gets a random value of **0.7** from the preset range, the sub-branch length will be **70%** of the parent branch length.

- When `Length Decay Range` is **generally large**, the tree crown appears **enormous**.
- When `Length Decay Range` is **generally small**, the tree crown appears **short and sparse**.
- When `Length Decay Range` has a **wide interval**, the tree crown appears **varied and layered**.
- When `Length Decay Range` has a **narrow interval**, the tree crown appears **uniform and dense**.

> Note that unless you have special requirements, the maximum value of `Length Decay Range` should not exceed **0.95**, otherwise the tree crown will be too dense, or even grow infinitely (limited only by `Max Depth`), which not only affects rendering performance but also makes the tree crown look extremely unnatural.

### 3. Leaf Settings

`Leaf Settings` controls the appearance of leaves. By adjusting leaf color, weight, density, and line width, you can achieve different leaf effects. (For basic tutorials, just modify the colors.)

You can start by adding several shades of green with some variation to see the effect.

### 4. Background Color

As the name suggests, `Background Color` controls the canvas background color.  
You can use a light leaf-like tone for a more natural look.

For more professional color suggestions, you can refer to [Color Wheel Tool – Adobe Express](https://color.adobe.com/create/color-wheel).

---

## Advanced Parameters

This section provides detailed definitions and usage of each parameter.

All parameters in pixels represent lengths when the window height is standard 1080 pixels. Actual rendering scales proportionally based on window height.

---

### 1. Global Settings

Global settings are not included in any parameter group and directly affect the result.

#### 1.1 Tree Name

`Tree Name` is used to name your tree.  
It's used for display in the main interface list and as the default file name prefix for exported images. Empty strings are automatically corrected to **"Untitled"**.

#### 1.2 Background Color

`Background Color` controls the canvas background color.  
Used to fill the window background and wallpaper image background.

---

### 2. Basic Shape Parameters

The following parameters directly control the basic shape and termination conditions of the tree.

#### 2.1 Trunk Length

`Trunk Length` is the initial length (in pixels) of the first-level branch emanating from the tree root. The tree root is positioned at 10% above the bottom of the canvas.  
Used to determine the baseline scale of the entire tree, directly affecting the tree's final size.

#### 2.2 Minimum Branch Length

`Minimum Branch Length` is one of the tree's termination conditions. When a branch length falls below this threshold, no more sub-branches are generated.  
Used to control the density of the tree crown's ends and the size of the crown.

#### 2.3 Max Depth

`Max Depth` is one of the tree's termination conditions. When the recursion level of a branch (the root is level 0, sub-branches from the root are level 1, and so on) reaches this value, branching stops.  
Used to prevent the tree crown from being too dense or growing infinitely, which would affect performance or even freeze the application.

> Note that in the early stages of creation, it's recommended to set `Max Depth` to **10 ~ 30**, and adjust later as needed to prevent freezing during testing due to unreasonable parameters.

#### 2.4 Leaf Threshold

`Leaf Threshold` determines whether the current branch should be rendered as a twig or a leaf. Based on branch length to determine visual role. When the current branch length is below this threshold, the branch is rendered as a leaf; above the threshold, it's rendered as a trunk.  
Used to control the visual effect of leaves, affecting the shape of the tree crown.

> Generally, it's not recommended to use this to directly adjust leaf density, as this is related to leaf shape. Other parameters should be adjusted instead.

#### 2.5 Maximum Branch Inclination

`Maximum Branch Inclination` is one of the tree's termination conditions. Controls the upper limit (in degrees) of the absolute angle of sub-branches (the angle relative to the vertical upward direction). Any sub-branch angle exceeding this value will not be created and will terminate directly.  
Used to prevent branches from drooping and maintain the tree's shape.

---

### 3. Branch Angle Range

`Branch Angle Range` is the deflection angle interval range independently sampled for each sub-branch. Two sub-branches deflect to either side of the parent branch respectively. The sampling range is [Min, Max] (degrees).  
Used to control branch shape, affecting branch neatness and growth pattern.

#### 3.1 Angle Probability `Distribution Curve`

Open the bar chart editor through the `Distribution Curve` button: click the bars to adjust bar heights, with a height range of `0.02 ~ 1.0`.

**Sampling method for each sub-branch**: Divide the angle interval equally into 8 sub-intervals, first select a sub-interval using the bar chart heights of the distribution curve as weights, then take a uniformly random value within that sub-interval. If no distribution curve is specified, a uniformly random value is taken from the global interval.

Simply put, the taller the bar, the higher the probability that nearby values will be selected as branch angles.

#### 3.2 Reference Example

The default tree species `Pine` has a branch angle range of `[0, 90]`, with an angle probability distribution curve approximately as follows:

```Bar Chart
[1.00, 0.02, 0.02, 0.02, 0.02, 0.02, 0.02, 0.20] (bar heights)
[0.00, 12.86, 25.71, 38.57, 51.43, 64.29, 77.14, 90.00] (bar labels)
```

It can be seen that the pine tree's branch angles are more likely to be around 0 and 90 degrees. That is, pine tree branches are more likely to be almost parallel or perpendicular to the parent branch, rather than growing diagonally.  
The actual effect is that pine trees mostly grow straight, but sometimes make large turns, providing a form similar to the twisted and vigorous shape of real pine trees.  
Among them, being more likely to be parallel to the parent branch helps maintain the basic shape of the tree crown so it doesn't become too chaotic.

---

### 4. Length Decay Range

`Length Decay Range` is the length decay ratio interval range independently sampled for each sub-branch. The actual sub-branch length = parent branch length × sampled decay ratio. The sampling range is [Min, Max] (decay ratio).

#### 4.1 Decay Probability `Distribution Curve`

Similar to [3.1 Angle Probability Distribution Curve](#31-angle-probability-distribution-curve).

Open the bar chart editor through the `Distribution Curve` button: click the bars to adjust bar heights, with a height range of `0.02 ~ 1.0`.

**Sampling method for each sub-branch**: Divide the angle interval equally into 8 sub-intervals, first select a sub-interval using the bar chart heights of the distribution curve as weights, then take a uniformly random value within that sub-interval. If no distribution curve is specified, a uniformly random value is taken from the global interval.

Simply put, the taller the bar, the higher the probability that nearby values will be selected as decay ratios.

#### 4.2 Reference Example

The default tree species `Green Tree` has a length decay range of `[0.6, 0.94]`, with a decay probability distribution curve approximately as follows:

```Bar Chart
[0.84, 0.02, 0.02, 0.02, 0.02, 0.02, 0.02, 0.70] (bar heights)
[0.60, 0.65, 0.70, 0.75, 0.79, 0.84, 0.89, 0.94] (bar labels)
```

It can be seen that the green tree's decay ratios are more likely to be around 0.60 and 0.94. That is, green tree branches are either almost the same length as the parent branch or noticeably shorter.  
The actual effect is that the green tree sometimes has spreading branches and sometimes gathered leaves, providing a layered and varied shape.  
It's worth noting that if the green tree chooses a short branch too early, it will directly reduce the entire tree's height, resulting in greater variation in tree height.

---

### 5. Trunk/Leaf Settings

`Trunk/Leaf Settings` is a collection of parameters that determine the visual effect of trunks and leaves, without affecting the tree's structural shape.

Trunks and leaves each have independent color lists. Each color entry contains three attributes:

#### 5.1 Color

`Color` determines the color used for rendering trunks or leaves. Supports setting multiple colors for trunks/leaves, randomly selected.

#### 5.2 Weight

`Weight` determines the probability that this color will be selected when drawing a new trunk or leaf. The larger the value, the higher the proportion of this color in trunks/leaves.

#### 5.3 Line Width Ratio

`Line Width Ratio` determines the width coefficient of trunks or leaves. The larger the value, the thicker the trunk/leaf. The actual thickness also depends on branch length.

**Multi-Parameter Group Behavior**: Trunk/leaf settings do not participate in numerical weighting of multiple parameter groups. When multiple parameter groups exist, the trunk/leaf settings of the parameter group with the largest weight are used directly.

---

### 6. Fallen Leaf System

`Fallen Leaf System` is a function to add fallen leaves on the ground. After a leaf is drawn, there is a probability of drawing an additional fallen leaf with the same shape in the ground area.  
Used to enhance the atmosphere of the tree.

#### 6.1 Fallen Leaf Density

`Fallen Leaf Density` determines the probability of leaves being converted to ground fallen leaves. The value range is `0.0 ~ 1.0`. The larger the value, the more fallen leaves.  
The actual number of fallen leaves also depends on the density of the tree crown (the number of leaves on the tree).

#### 6.2 Fallen Leaf Dispersion

`Fallen Leaf Dispersion` determines the distribution range of fallen leaves, specifically the horizontal offset range (in pixels) relative to the original leaf and the vertical offset range (in pixels) relative to the ground position. The larger the value, the wider the distribution of fallen leaves.

The vertical offset relative to the ground position uses a triangular distribution (average of two uniform random values).  
The horizontal offset relative to the original leaf uses a uniform distribution, because the randomness of a large number of leaves already achieves an effect similar to a normal distribution.

---

### 7. Parameter Group System

`Parameter Group` is the core abstraction concept of `Fractal Tree Desktop Wallpaper`. A single tree species configuration can contain one or more parameter groups, each with an independent set of parameters.

#### 7.0 Why Parameter Groups are Needed

Imagine you want to design a tree with a straight trunk but a spreading crown.  
If you only use one set of parameters, the trunk and crown can only have the same shape, making it impossible to achieve complex effects.

In fact, this was the real problem I encountered when designing `Maple`.  
For this, I had two ideas:

The first idea was to incorporate branch y-coordinate into "parameters of parameters", which could directly implement parameters that change with height. In other words, parameters could be written as functions of y.  
This aligns well with programmer thinking and can achieve more complex and precise shape control. However, it's clearly extremely unintuitive. In practice, it requires quite complex abstract thinking, and needs calculators or even tools like Desmos to study function graphs and repeatedly adjust parameters.

Therefore, I chose the second idea — the parameter group system.  
By directly defining multiple sets of parameters and applying different parameter groups at different heights, the shapes of trunks and crowns can be distinguished.

During actual development, for smooth transitions, I chose to apply inverse-weighted mixing of parameters from different parameter groups.

#### 7.1 Basic Operations

In `Multi-Parameter Group Control`:

- **Dropdown:** Select a parameter group to edit. Click to modify the parameter group name.
- **"+":** Add a new parameter group (copies parameters from the currently selected parameter group).
- **"✕":** Delete the currently selected parameter group.

#### 7.2 Typical Effective Height

`Typical Effective Height` is the anchor height (in pixels) of this parameter group, used for weighted mixing calculations of multiple parameter groups.  
Used to control the approximate position where the parameter group takes effect. The closer the branch is to the typical effective height from the ground, the higher the weight of the parameter group.

Simply put, parameters will take effect near the `Typical Effective Height`.

#### 7.3 Weight Calculation

When a branch is at height $h$, the original weight of group $i$ is:

$$w_i = \min\left(\frac{1}{|h - t_i|},\ 128\right)$$

Where $t_i$ is the typical height of group $i$. Weight is fixed at 128 when distance is 0 to avoid division by zero. After normalizing all weights:

- **Ordinary scalar parameters** (angle range, decay range, minimum branch, max depth, leaf threshold, fallen leaf parameters, trunk length, branch inclination): Weighted average.
- **`Trunk/Leaf Settings`**: Use the color list of the maximum weight group directly.
- **`Distribution Curves`** (angle distribution, decay distribution): Weighted average per bar.

When there is only one parameter group, skip mixing calculation and use all parameters of that group directly.

#### 7.4 Reference Example

The default tree species `Maple` has two parameter groups: "Trunk" and "Crown". The only differences between them are `Branch Angle Range`, `Length Decay Range`, and `Typical Effective Height`.

**"Trunk"** parameter group:
- Typical effective height is `0`, meaning the closer to the ground, the higher the parameter group weight.
- Branch angle range is `0 ~ 10` uniform distribution, maintaining the upright form of the trunk.
- Length decay range is `0.8 ~ 0.95` uniform distribution, ensuring the trunk is long and reducing lower branches.

**"Crown"** parameter group:
- Typical effective height is `180`, basically meaning the further from the ground, the higher the parameter group weight.
- Branch angle range is `0 ~ 60` with high probability at both ends, maintaining the fluffy form of the crown.
- Length decay range is `0.3 ~ 0.95` with high probability at the large end and low but still possible probability at the small end, maintaining the layered form of the crown and increasing crown diversity.

---

## Import/Export Tree Species

The program saves all custom tree species to `<Tree Name>.json` files in the `AppData\Local\Fractal Tree Desktop Wallpaper\trees` folder. You can click the `Open Tree Folder` button at the bottom left of the parameter settings window to open it quickly.

- To import a tree species, simply place the `<Tree Name>.json` file you want to import into that folder, then restart the program.
- To export a tree species, simply copy the `<Tree Name>.json` file you want to export.

---

## Render Window

Click any tree species button in the main interface or click Preview in the edit interface to open the fractal tree render window. Supports drag and zoom.

- **Refresh**: Resets the random seed and redraws. Each click produces a different shape.
- **Save as Image**: Renders to PNG at window size, saves to desktop as `<Tree Name>_<Timestamp>.png`. Window remains open after completion.
- **Set as Desktop Wallpaper**: Renders to BMP at 2x screen resolution, calls system API to set as Windows desktop wallpaper. Window closes automatically after completion.