# **VAMPIRE LIKE ARENA**
------------------------
## Полезные ссылки
### [ГДД](https://docs.google.com/document/d/1j1ctWSqk-kUHAkp3YIXL3ebgs01GfNUnDoHt4SzfsZ4/edit)
### [Баланс док](https://docs.google.com/document/d/16uc-Qz0FHBVUnEztY0kLAwtftrwytGjM7lXRgvbQFBc/edit#heading=h.ma9nrfwugno)

-----------------------

## Организация проекта

### Сцены
| Название | Что происходит |
|----------|----------------|
|StartScene|Сцена с которой должна стартовать игра|
|SampleScene|Сцена на которой происходит основной игровой процесс|
-----------------------
### Пространства имён `namespace`
Для того чтоб взаимодействовать с классами игры через код надо подключить нужный вам `namespace`
|Название|За что отвечает|
|-----------------|-----------------|
|`VampireLike`|Просто обозначает прстраство игры|
|`VampireLike.StartScenes`|Содержит логику для стартовой сцены|
|`VampireLike.Core`|Содержит основную логику игры, а также содержит все `interface`'ы|
|`VampireLike.Core.Characters`|Содежит логику связанную с персонажами|
|`VampireLike.Core.Cameras`|Содержит всё что связанно с камерой|
|`VampireLike.Core.Characters.Enemies`|Содежит логику связанную с врагами|
|`VampireLike.Core.General`|Содержит класс который связывает между собой основные игровые компоненты|
|`VampireLike.Core.Input`|Содержит классы которые отвечают за пользовательский ввод|
|`VampireLike.Core.Levels`|Содержит классы которые отвечают за построенние уровня, а так же за чанки|
|`VampireLike.Core.Players`|Содержит логику которая завязана на игроке и его данных|
|`VampireLike.Core.Trees`|Содержит логику отвечающую за построенние графа|
|`VampireLike.Core.Weapons`|Содержит логику отвечающая за оружие|
-----------------------
### Файловая структура
- Arts
    - Materials
- Plugins
- Prefabs
    - Chunks
    - Enemies
    - Level
    - Projectile
    - Weapons
- Resources
- Scenes
- ScriptableObjects
- Scripts
    - Core
        - Camera
        - Character
        - General
        - Input
        - Interfaces
        - Levels
        - Player
        - Tree
        - Weapons
    - General
    - StartScene
-----------------------
### Сторонние плагины
- [DOTween](https://assetstore.unity.com/packages/tools/animation/dotween-hotween-v2-27676)
- [Joystick Pack](https://assetstore.unity.com/packages/tools/input-management/joystick-pack-107631)
-----------------------
## Кратко про работу
Игра запускается со `StartScene`. На `StartScene` запускаеться её собственный инитер - он инициализирует единственный `singelton` - `PlayerController`. После этого запускается следующая сцена `SampleScene`.

На `SampleScene` всё начинается с класса `EntryPoint`. Он запускает инициализацию класса `ControllerManager`. `ControllerManager` - прокидывает связи между всеми остальныими контролерами, а так же их инициализирует и запускает игровой цикл.

За каждой сущностью игры стоит её контроллер. На сцене ни находяться под объектом `Controllers`.

1. `ControllerManager` - отвечает за другие контроллеры
2. `PlayerInput` - отвечает за ввод игрока
3. `EnemiesController` - отвечает за врагов
4. `MainCharacterController` - отвечает за ГГ
5. `LevelControlelr` - отвечает за повидение уровня
6. `WeaponController` - отвечает за оружие

### Персонажи

Для ГГ и врагов есть общий класс `GameCharacterBehaviour`.

За оружие в руках персонажей отвечает класс `CharacterWeapon`(Тут акуратно так как этот класс для ГГ находиться в котролере, а у врагов в классе `EnemyCharacter`).

За данные которые содержит персонаж отвечает класс `CharacterData`. На момент написание *README* для персонажей не был написан конфиг для настройки.

### Оружие

Для оружия есть общий *абстрактный* класс `WeaponBehaviour`. И каждое оружие в игре наследуеться от него. Список всех типов оружия которые пресутствуют в игре находяться в `enum` `WeaponType`, если хотите добавить новое оружие то вносите туда ещё одно перечисление. Сами оружия это префабы в которых храняться тип оружия.

Оружия в игре настраиваються через `WeaponConfig`.

Для каждого оружия есть `Projectile`, они так же представлены в виде префаба и так же настраиваються в `WeaponConfig`. Все типы `Projectile` хранятся в `ProjectileType`.

В игре реализовна три оружия: 
- Ближнего боя
- Оружие дальнего боя
- Оружие каторое касается

### Уровни

У уровней устроенно так. Есть `Arena` - это просто платформа, между уровнями прокладывается `Road`, которая состоит из `Platform`. Враги и препятствия находятся в `Chunk` (обязательно надо прокинуть врагов в список врагов).

У `Chunk` есть свой тир, а так же должен быть реализован множитель ХП, но на момент написание *README.md* он не реализован.

`Chunk`'и настраиваються через `ChunkConfig`, там настраиваються данные для тиров, а также лежат все префабы.

## Классы
|Название|Что делает|
|---------|----------|
|`FollowerCamera`|Перемищение камеры|
|`EnemyConfig`|*Не реализован*|
|`EnemeisController`|Котроллер врагов|
|`EnemyCharacter`|Класс который отвечает за врагов|
|`EnemyMovement`|Перемищение врага|
|`MainCharacter`|Класс ГГ|
|`MainCharacterController`|Контроллер ГГ|
|`CharacterData`|Данные персонажей|
|`CharacterMovement`|Передвижение персонажа|
|`GameCharacterBehaviour`|Общий класс для персонажа|
|`ControllerManager`|Общий контроллер для контроллеров|
|`EntryPoint`|Точка входа на `SampleScene`|
|`MISCController`|Контроллер для *хлама*|
|`OnColiderEnterComponent`|Вызывает события по касанию колайдера|
|`PlayerInput`|Обработка ввода игрока|
|`IAttaching`|Итерфейс для получения Target(transform)|
|`IBuilder`|Для паттерна Builder|
|`IHero`|Интерфейс пустишка для пометки о том что это ГГ|
|`IIniting`|Интерфейс для инитов|
|`IMoving`|Интерфейс для перемищения|
|`INeeding`|Интерфейс для установки запрашиваемого Generic|
|`INeedingWeapon`|Интерфейс для получения оружия|
|`IRepelled`|Интерфейс для реализации оталкивания|
|`IRotating`|Интерфейс для поворотов|
|`ITakingDamage`|Интерфейс для получения урона|
|`IWeapon`|Интерфейс для оружия|
|`ChunkConfig`|Конфиг для чанков|
|`ChunkConfigurator`|Предаставляет работу на сцене для конфигов чанков|
|`Arena`|Арена|
|`Chunk`|Чанк|
|`DeadGround`|Класс который отвечает за убийство скидыванием|
|`Level`|Уровень|
|`LevelController`|Контроллер для уровней|
|`Road`|Дорога между `Arena`|
|`Platform`|Платформы из которых состоит `Road`|
|`Player`|Данные игрока|
|`PlayerContrller`|Контроллер игрока|
|`LevelTree`|Пустышка|
|`Node`|Вершина графа|
|`Tree`|Древо графа|
|`WeaponConfigurator`|Конфигуратор оружия|
|`WeaponDTO`|Data Transfer Object|
|`WeaponProjectileBuilder`|Builder оружия|
|`WeaponConfig`|Конфиг для оружия|
|`Projectile`|Общий класс для снарядов|
|`ProjectileMovement`|Класс для перемищения снарядов|
|`DirectMovement`|Оружие которое должно стрелять в сторону в кторую смотрит игрок (**Не сделано**)|
|`DirectProjectile`|Оружие которое должно стрелять в сторону в кторую смотрит игрок(**Не сделано**)|
|`DirectWeapon`|Оружие которое должно стрелять в сторону в кторую смотрит игрок(**Не сделано**)|
|`DirectedProjectile`|Снаряд который летит в сторону врага|
|`ProjectileWeapon`|Оружие которое стреляет в сторону врага|
|`TangentWeapon`|Оружие которое наносит урон касанием|
|`CharacterWeapon`|Контроллер для оружия персонажа|
|`WeaponBehaviour`|Общий класс для оружия|
|`WeaponData`|Данные для оружия|
|`WeaponsController`|Контроллер оружия(выдает оружия всем нуждающимся)|
|`WeaponType`|Перечисления все оружий|
|`Pool`|Заготовка для реализации паттерна Pool|
|`StartSceneIniter`|Инитер для стартовой сцены|