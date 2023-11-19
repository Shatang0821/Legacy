# 代码命名规范

本项目遵循以下代码命名规范：

## 类名和结构体名

- 使用 **PascalCase**。
- 例：`PlayerController`, `GameManager`, `EnemyAI`。

## 方法名

- 使用 **PascalCase**。
- 方法应清晰表明其功能。
- 例：`CalculateDamage`, `MovePlayer`, `ShowMenu`。

## 变量名

- 私有变量使用 **camelCase**，前加下划线 `_`。
- 公共变量使用 **PascalCase**。
- 例：私有 `_health`, 公共 `PlayerSpeed`。

## 局部变量和参数

- 使用 **camelCase**。
- 例：`initialHealth`, `moveSpeed`。

## 常量和静态只读字段

- 使用 **PascalCase** 或 **ALL_CAPS**。
- 例：`const int MaxHealth = 100;`, `static readonly int DefaultSpeed = 5;`。

## 属性

- 使用 **PascalCase**。
- 例：`public int Health { get; private set; }`, `public bool IsDead { get; }`。

## 事件

- 使用 **PascalCase**。
- 事件命名通常是动词或动词短语。
- 例：`public event Action OnDeath;`, `public event Action<int> OnScoreChanged;`。

## 接口

- 名称以大写字母 **I** 开头，后跟 **PascalCase**。
- 例：`interface IMovable`, `interface IHealthSystem`。

## 枚举

- 类型和成员使用 **PascalCase**。
- 例：`enum GameState { Running, Paused, Ended }`。
