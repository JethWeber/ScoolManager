# Migração: School Manager (HTML/Tailwind) → Avalonia MVVM (Offline)

Baseado em `DESIGN.md` (design system "Azure Scholastic Enterprise") + **8 telas HTML**:

| Ficheiro | Tela | Função |
|---|---|---|
| `code.html` | **Login** | Autenticação |
| `dashBoard.html` | **Dashboard** | KPIs gerais, devedores, atalhos, resumo do dia |
| `alunosView.html` | **Lista de Alunos** | Grid de alunos, filtros, pesquisa, paginação |
| `cobranca.html` | **Centro de Cobrança** | Gestão de devedores/inadimplência |
| `gestaoPagamentos.html` | **Gestão de Pagamentos** | Registar pagamento de um aluno específico |
| `emitirRecibo.html` | **Emissão de Recibo** | Pré-visualização e emissão de recibo |
| `feixoCaixa.html` | **Fecho de Caixa** | Fecho diário, entradas/saídas, saldo |
| `perfilEstudante.html` | **Perfil do Aluno** | Detalhe individual, histórico de pagamentos |

Todas as 8 telas reutilizam o mesmo **Sidebar fixo (260px)** com os mesmos 7 módulos (Dashboard, Alunos, Pagamentos, Recibos, Entradas, Saídas, Relatórios, Configurações) — isto é decisivo para a arquitetura: vai ser um **Shell único** com conteúdo trocável, não 8 janelas soltas.

---

## 1. Stack alvo (confirmado na estrutura real do projeto)

| Camada | Escolha | Motivo |
|---|---|---|
| Target Framework | **.NET 10.0** (preview/RC — escolha intencional do projeto) | atenção: SDKs preview mudam de instalador frequentemente; baixar o SDK exato usado em dev e arquivá-lo, pois pode não estar mais disponível para download mais tarde |
| Framework UI | **Avalonia 11.x** | multiplataforma, XAML, roda 100% offline depois de publicado |
| Padrão | **MVVM**, com **separação em 2 projetos**: `ScoolManager.Core` (lógica, sem dependência de UI) + `ScoolManager.Desktop` (Avalonia, Views/ViewModels) | já está assim na estrutura real — boa prática, mantemos |
| MVVM toolkit | **CommunityToolkit.Mvvm** | `ObservableObject`, `[RelayCommand]`, `[ObservableProperty]` — evita boilerplate de `INotifyPropertyChanged` |
| Injeção de dependência | **Microsoft.Extensions.DependencyInjection** | já vem nos templates novos do Avalonia |
| Navegação | **ViewLocator** (já existe `ViewLocator.cs` no projeto) **+ ViewModel-first navigation** | troca de `CurrentPage` no Shell, resolvido automaticamente pelo `ViewLocator` |
| Ícones | **Material.Icons.Avalonia** (substitui o Google Fonts "Material Symbols Outlined", que é web) |
| Tema/Design tokens | **ResourceDictionary (.axaml)** gerado a partir do `DESIGN.md` |

### Onde cada coisa vive nos 2 projetos
- **`ScoolManager.Core`** → `Models/` (Aluno, Pagamento, Recibo, Transacao), `Services/` + interfaces (`IAlunoService`, `IPagamentoService`, `IReciboService`, `ICaixaService`, `IAuthService`), DbContext do EF Core/SQLite. **Sem nenhuma referência a Avalonia** — fica testável e reutilizável (ex.: se um dia quiserem versão mobile).
- **`ScoolManager.Desktop`** → `ViewModels/`, `Views/`, `Styles/`, `Assets/` (fontes/imagens), `App.axaml`, `Program.cs`, `ViewLocator.cs`. Referencia `ScoolManager.Core` via `<ProjectReference>`.

---

## 2. O que baixar/instalar ANTES de ir para a máquina offline

A máquina final não tem internet — então **tudo precisa vir em cache local ou instalador**. Faz isto numa máquina COM internet primeiro:

### 2.1 SDKs e ferramentas (instaladores .exe/.msi — guardar os instaladores)
- **.NET SDK 10.0** — https://dotnet.microsoft.com/download/dotnet/10.0 (baixar o installer offline, não o "online installer"). ⚠️ Como é preview/RC, **guarda o instalador exato usado em dev** (ex. cópia local em `/installers/dotnet-sdk-10.0.x-win-x64.exe`) — versões preview saem do ar nos canais oficiais quando a próxima RC/RTM é lançada, então não dá pra confiar em "baixar de novo depois".
- **Avalonia templates**: `dotnet new install Avalonia.Templates` (depois de instalado, o template fica em cache local em `~/.templateengine`, não precisa de internet de novo)
- IDE: **Visual Studio 2022/2026** (Community, com workload ".NET desktop", e suporte ao SDK preview habilitado em *Tools → Options → Preview Features*) **ou JetBrains Rider** (ambos têm instalador offline) **ou VS Code + extensão "Avalonia for VSCode" + C# Dev Kit** (baixar os `.vsix` manualmente se for offline)
- **AvaloniaUI Extension para Visual Studio/Rider** (preview de XAML) — opcional, mas ajuda muito

### 2.2 Cache local do NuGet (passo crítico para offline)
Com internet, restaura o projeto uma vez e depois copia a pasta de cache:
```
%userprofile%\.nuget\packages   (Windows)
~/.nuget/packages               (Linux/Mac)
```
Leva essa pasta inteira no pendrive/imagem da máquina, ou configura um **NuGet.Config local** apontando para uma pasta-pasta de pacotes (`<packageSources><add key="local" value="C:\nuget-offline-cache" /></packageSources>`).

### 2.3 Pacotes NuGet a baixar (lista completa, organizada por projeto)

Primeiro, o que realmente precisa de gráfico — confirmei revendo o SVG/código de cada uma das 8 telas:

| Tela | Visual gráfico encontrado | O que é |
|---|---|---|
| Dashboard | `<svg>` com `<path>` de área + linha | **Gráfico de linha/área** (Receita mensal, Dez–Mai) |
| Centro de Cobrança | `<svg>` com 2 `<circle>` (stroke-dasharray) | **Anel de progresso (donut/gauge)** — "Saúde Financeira 73%" |
| Fecho de Caixa | `<svg>` com `<path>` em forma de onda | **Mini gráfico de área** ("Desempenho vs. Ontem +12.4%") |
| Emitir Recibo, Alunos, Pagamentos, Perfil, Login | — | Sem gráfico real (o "Canvas"/"graph" que aparece no grep é só ruído de classes Tailwind ou ícones) |

Conclusão: **3 telas precisam de gráfico de fato**, todos cobertos por uma única lib (`LiveCharts2`).

```text
═══ ScoolManager.Core (.csproj) ═══
Microsoft.EntityFrameworkCore.Sqlite        → persistência local SQLite
Microsoft.EntityFrameworkCore.Design        → necessário para gerar Migrations (dotnet ef)
Microsoft.EntityFrameworkCore.Tools         → comandos `dotnet ef migrations add/update` no CLI
BCrypt.Net-Next                             → hash de senha do login (offline, sem servidor)
QuestPDF                                    → gera o PDF do Recibo (licença Community grátis até USD 1M de receita anual — confirmar antes do release)

═══ ScoolManager.Desktop (.csproj) ═══
Avalonia                                    → já deve estar (base)
Avalonia.Desktop                            → já deve estar (runtime desktop)
Avalonia.Themes.Fluent                      → já deve estar (tema base)
Avalonia.Fonts.Inter                        → fallback, mesmo já tendo as fontes próprias embutidas
Avalonia.Diagnostics                        → já deve estar (debug overlay, só em build Debug)
Avalonia.Controls.DataGrid                  → grids de Alunos, Cobrança, Pagamentos, Transações de Caixa
Avalonia.Xaml.Behaviors                     → úteis para replicar micro-interações do HTML (hover/focus/mousedown do `code.html`) sem code-behind
CommunityToolkit.Mvvm                       → [ObservableProperty], [RelayCommand], ObservableValidator
Microsoft.Extensions.DependencyInjection    → registo de Services/ViewModels
Material.Icons.Avalonia                     → ícones (school, login, lock, payments, receipt_long, trending_up/down, etc.)
LiveChartsCore.SkiaSharpView.Avalonia       → as 3 telas com gráfico: Dashboard (linha/área), Cobrança (gauge/donut), Fecho de Caixa (área)
SkiaSharp.NativeAssets.Linux / .Win32 / .macOS  → dependência nativa do SkiaSharp (usado pelo LiveCharts) — IMPORTANTE: o NuGet do SkiaSharp normalmente já traz os 3, mas confirmar que o pacote nativo do SO de destino entra no `dotnet restore` antes de ir para offline (ver nota abaixo)
```

⚠️ **Nota crítica sobre SkiaSharp/LiveCharts offline:** esses pacotes trazem binários nativos (`.so`/`.dll`/`.dylib`) **específicos por RID** (`linux-x64`, `win-x64`...). Ao fazer o `dotnet restore`/`dotnet publish` pela primeira vez (com internet), faz isso já especificando o **RID exato da máquina offline de destino** (`-r linux-x64` ou `-r win-x64`), senão o cache do NuGet não vai conter o binário nativo certo e o app vai crashar ao tentar desenhar o primeiro gráfico, já sem internet para corrigir.

Todos os pacotes acima ficam resolvidos e cacheados localmente em `~/.nuget/packages` após um único `dotnet restore` com internet — não esquecer de copiar essa pasta (secção 2.2) e fazer um `dotnet build`/`publish` de teste **desconectando a internet de propósito** antes de levar para a máquina final, só para confirmar que nada tenta baixar nada.

### 2.4 Fontes (✅ já feito no projeto — só confirmar)
No HTML as fontes vinham do Google Fonts via `<link>` (precisa internet). No Avalonia **as fontes têm que ser arquivos físicos embutidos no projeto** — e isso **já está feito**:

- `ScoolManager.Desktop/Assets/Fonts/Inter/` — já tem a família completa (pesos 100–900, itálicos, optical sizes 18pt/24pt/28pt, + variable fonts), com `OFL.txt` (licença) presente. ✔️
- `ScoolManager.Desktop/Assets/Fonts/Poppins/` — já tem a família completa (Thin–Black, itálicos), com `OFL.txt` presente. ✔️
- **Material Symbols Outlined** — NÃO precisa baixar a fonte web; usar a lib `Material.Icons.Avalonia` (glifos como `Geometry`/ícones vetoriais embutidos via NuGet, sem precisar de fonte alguma). Ainda não vejo essa dependência na estrutura — falta adicionar ao `.csproj` do Desktop.

Falta só:
1. Confirmar no `App.axaml` que os `FontFamily` registados via `avares://ScoolManager.Desktop/Assets/Fonts/Inter#Inter` (e idem Poppins) batem com os nomes usados no `Styles/Typography.axaml`.
2. Como há **muitos pesos/optical sizes** (18pt/24pt/28pt), decidir qual variante usar por padrão no body-text vs. headings — Tailwind original só usava pesos 400/500/600/700 "normais", então provavelmente basta o conjunto **24pt** (ótimo para tamanhos de texto de 14–24px usados no design) + a variable font como fallback.

### 2.5 Imagens de fundo e avatares
- A imagem de fundo do Login (`googleusercontent.com/...`) precisa ser baixada e embutida (`Assets/Images/login-bg.jpg`).
- As telas de **Dashboard**, **Alunos**, **Perfil do Aluno** usam **fotos de alunos/funcionários** vindas de URLs do Google (placeholders de demonstração). No sistema real e offline, isto vira:
  - Campo `FotoPath` no Model do Aluno/Funcionário, apontando para um ficheiro local (`%AppData%/SchoolManager/Photos/{id}.jpg`), guardado quando o utilizador faz upload da foto; **ou**
  - Um **avatar gerado localmente** (iniciais sobre fundo colorido) quando não há foto — não depende de internet nem de serviço externo.
- Nota: `QuestPDF` (geração do recibo) é grátis para uso comercial até USD 1M de receita anual da empresa que usa o software — vale confirmar a licença antes de embarcar no produto final.

### 2.6 Publicação final (para rodar sem .NET instalado na máquina offline)
Usar **publish self-contained**, assim a máquina destino não precisa nem do .NET Runtime instalado:
```
dotnet publish -c Release -r win-x64 --self-contained true -p:PublishSingleFile=true
```
(ajustar o RID: `win-x64`, `linux-x64`, etc., conforme o SO da máquina final). Isso gera um `.exe`/binário único que já leva runtime + libs.

---

## 3. Mapeamento do Design System → Avalonia

O `DESIGN.md` já está praticamente pronto pra virar um `ResourceDictionary`. Estratégia:

| Conceito Tailwind/CSS | Equivalente Avalonia |
|---|---|
| `theme.extend.colors` (tokens do DESIGN.md) | `<SolidColorBrush x:Key="Primary" Color="#ADC6FF"/>` num `Colors.axaml` |
| `fontFamily` (display-lg, body-md...) | `FontFamily` definida em `Styles` reaproveitáveis (`x:Key="DisplayLgStyle"`) |
| `borderRadius` | `CornerRadius` em `Style` de `Border`/`Button` |
| `spacing` (xs, sm, md, lg, xl) | `Thickness` constantes em `Styles.axaml` ou `StaticResource` |
| `box-shadow` / glow azul | `BoxShadow` (propriedade nativa do Avalonia 11, sintaxe CSS-like: `0 4 12 0 #4D3B82F6`) |
| `backdrop-filter: blur(12px)` (glassmorphism) | **Não existe nativo no Avalonia.** Workaround: `Border` semi-transparente + leve "blur" via efeito de composição (`Avalonia.Rendering.Composition`) ou simplesmente simular com opacidade — **não há blur real de fundo cross-platform fácil**, então recomendo simplificar para um fundo semitransparente liso. |
| `:hover`, `:focus` (Tailwind) | `Pseudoclasses` do Avalonia: `Button:pointerover`, `TextBox:focus` dentro de `<Style Selector="...">` |
| Gradiente do botão (`linear-gradient`) | `LinearGradientBrush` no `Style` do `Button` |
| Ícones Material Symbols | `Material.Icons.Avalonia` → `<materialIcons:MaterialIcon Kind="School"/>`, `Kind="Login"`, `Kind="Lock"`, `Kind="EmailOutline"`, `Kind="CheckCircle"` |

Sugestão de organização de arquivos de tema:
```
/Styles
  Colors.axaml        (paleta do DESIGN.md)
  Typography.axaml     (estilos de texto: DisplayLg, HeadlineSm, BodyMd...)
  Controls.axaml        (estilos de Button, TextBox "neo-input", etc.)
App.axaml  → referencia os 3 acima em <Application.Resources>/<Styles>
```

---

## 4. Arquitetura de Shell (Sidebar persistente)

Como as 7 telas internas partilham o **mesmo sidebar**, a arquitetura correta **não** é repetir o menu em cada View. É um padrão clássico de app desktop:

- **`ShellView`** (a "MainWindow" depois do login) = `Sidebar` (UserControl fixo) + `ContentControl` que troca o conteúdo.
- **`ShellViewModel`**:
  - `ObservableCollection<NavItem> NavItems` (Dashboard, Alunos, Pagamentos, Recibos, Entradas, Saídas, Relatórios, Configurações)
  - `ViewModelBase CurrentPage` — bindado ao `ContentControl.Content` do Shell. Trocar de página = trocar esta propriedade.
  - `NavigateCommand(string destino)` — instancia o ViewModel certo (via DI) e atualiza `CurrentPage`.
  - `LogoutCommand` — volta para `LoginView`.
- **`ViewLocator`** do Avalonia resolve automaticamente a `View` certa a partir do tipo do `ViewModel` (convenção `XyzViewModel` → `XyzView`), então o `ContentControl` só precisa de `Content="{Binding CurrentPage}"`.

Isto poupa de recriar o sidebar 7 vezes e mantém o estado de navegação coerente (igual ao comportamento do HTML, onde o `<aside>` é fixo e só o `<main>` muda).

## 5. Estrutura de pastas (refletindo a estrutura REAL: `ScoolManager.Core` + `ScoolManager.Desktop`)

```
ScoolManager/
├── ScoolManager.Core/                      (sem dependência de Avalonia — testável, reutilizável)
│   ├── Models/
│   │   ├── Aluno.cs
│   │   ├── Pagamento.cs
│   │   ├── Recibo.cs
│   │   ├── Transacao.cs            (entrada/saída de caixa)
│   │   └── Usuario.cs              (substitui o atual Class1.cs — apagar o stub)
│   ├── Services/
│   │   ├── IAuthService.cs / AuthService.cs
│   │   ├── IAlunoService.cs / AlunoService.cs
│   │   ├── IPagamentoService.cs / PagamentoService.cs
│   │   ├── IReciboService.cs / ReciboService.cs    (gera PDF via QuestPDF)
│   │   └── ICaixaService.cs / CaixaService.cs
│   ├── Data/
│   │   └── SchoolManagerDbContext.cs       (EF Core + SQLite)
│   └── ScoolManager.Core.csproj
│
├── ScoolManager.Desktop/                   (já existe — Avalonia)
│   ├── Assets/
│   │   ├── Fonts/Inter/ ✔️ já presente
│   │   ├── Fonts/Poppins/ ✔️ já presente
│   │   └── Images/                  (login-bg.jpg, default-avatar.png — a adicionar)
│   ├── ViewModels/
│   │   ├── ViewModelBase.cs ✔️ já existe
│   │   ├── MainWindowViewModel.cs ✔️ já existe → vai virar o `ShellViewModel`
│   │   ├── LoginViewModel.cs
│   │   ├── DashboardViewModel.cs
│   │   ├── AlunosListViewModel.cs
│   │   ├── CobrancaViewModel.cs
│   │   ├── GestaoPagamentosViewModel.cs
│   │   ├── EmitirReciboViewModel.cs
│   │   ├── FechoCaixaViewModel.cs
│   │   └── PerfilEstudanteViewModel.cs
│   ├── Views/
│   │   ├── MainWindow.axaml ✔️ já existe → vira o `ShellView`
│   │   ├── LoginView.axaml
│   │   ├── DashboardView.axaml
│   │   ├── AlunosListView.axaml
│   │   ├── CobrancaView.axaml
│   │   ├── GestaoPagamentosView.axaml
│   │   ├── EmitirReciboView.axaml
│   │   ├── FechoCaixaView.axaml
│   │   ├── PerfilEstudanteView.axaml
│   │   └── Shared/
│   │       ├── SidebarView.axaml          (extraído 1x, usado no Shell)
│   │       ├── KpiCardView.axaml          (cartão reutilizado em Dashboard/Cobrança)
│   │       └── TopAppBarView.axaml        (cabeçalho repetido em todas as telas internas)
│   ├── ViewLocator.cs ✔️ já existe
│   ├── App.axaml ✔️ já existe → adicionar `Styles/` aqui
│   ├── Styles/                      (a criar)
│   │   ├── Colors.axaml
│   │   ├── Typography.axaml
│   │   └── Controls.axaml
│   ├── Models/                      (pasta já existe, vazia — **decisão**: mover seu conteúdo para `Core/Models`, manter aqui só se houver algo 100% específico de UI, ex. `NavItem.cs`)
│   ├── Program.cs ✔️ já existe
│   └── ScoolManager.Desktop.csproj → referenciar `ScoolManager.Core` via `<ProjectReference>`
│
└── MockUp/                           (mantém como referência visual — não entra no build)
```

**Diferenças-chave em relação ao que eu tinha documentado antes:**
- `Models/` e `Services/` vão para o `Core`, não para o `Desktop`.
- `Models/` que já existe (vazia) dentro do `Desktop` deve conter **apenas** tipos específicos de apresentação (ex. `NavItem` do sidebar), não as entidades de negócio.
- `MainWindowViewModel.cs` e `MainWindow.axaml` já existentes assumem o papel do `ShellViewModel`/`ShellView` descrito na secção 4 — não precisa criar do zero, é renomear/expandir.
- `INavigationService`: a **interface** pode viver em `Core` (assim os ViewModels do Core/testes a usam sem depender de Avalonia), mas a **implementação concreta** (que manipula `ViewModelBase CurrentPage` e instancia Views via DI) fica em `Desktop`, pois conhece o `ViewLocator`.## 6. ViewModels de todas as telas (descrição, sem código)

Todos herdam de `ViewModelBase : ObservableObject` (CommunityToolkit.Mvvm) e usam `[ObservableProperty]` / `[RelayCommand]`.

### 6.1 `LoginViewModel`
- Props: `Email`, `Password`, `RememberMe`, `IsBusy`, `ErrorMessage`
- Commands: `LoginCommand` (async, `CanExecute` se `!IsBusy`), `ForgotPasswordCommand`
- Validação via `ObservableValidator` (`[Required]`, `[EmailAddress]`)
- Depende de: `IAuthService`, `INavigationService`

### 6.2 `ShellViewModel` (ver secção 4)
- `CurrentPage` (ViewModelBase), `NavItems`, `LoggedUserName`, `NavigateCommand`, `LogoutCommand`

### 6.3 `DashboardViewModel`
- Props: `AlunosAtivos`, `ReceitaDoMes`, `EmDivida`, `RecebidoHoje` (cada um com valor + % de variação)
- `ObservableCollection<DevedorResumo> TopDevedores` (os 5 da lista "Devedor 1..5")
- `EntradasHoje`, `SaidasHoje`, `SaldoDoDia` (Resumo Financeiro do Dia)
- Commands: `NovoAlunoCommand`, `NovoPagamentoCommand`, `EmitirReciboCommand`, `NovaEntradaCommand`, `RelatorioDiarioCommand`, `FecharDiaCommand` — todos via `INavigationService` (atalhos rápidos = navegação)
- Depende de: `IAlunoService`, `IPagamentoService`, `ICaixaService`

### 6.4 `AlunosListViewModel`
- `ObservableCollection<AlunoListItem> Alunos` (nome, classe, status, dívida)
- Props de filtro: `SearchTerm`, `FiltroClasse`, `FiltroStatus` — recomputam a lista (idealmente via `CollectionView`/filtro em memória ou query no SQLite)
- Paginação: `PaginaAtual`, `TotalPaginas`
- Commands: `AbrirPerfilCommand(Aluno)` (navega para `PerfilEstudanteViewModel`), `NovoAlunoCommand`, `PaginaAnteriorCommand`, `ProximaPaginaCommand`
- Depende de: `IAlunoService`

### 6.5 `CobrancaViewModel`
- KPIs: `TotalDevedores`, `DividaGlobal`, `ValorRecuperado`
- `ObservableCollection<DevedorListItem> Devedores` (tabela principal)
- Painel de ações: `DevedorSelecionado`, `EnviarLembreteCommand`, `RegistrarPromessaPagamentoCommand`
- Indicador circular de progresso: `PercentualRecuperado` (calculado)
- Depende de: `IPagamentoService`, `IAlunoService`

### 6.6 `GestaoPagamentosViewModel`
- Contexto: aluno selecionado (`AlunoAtual`, vindo por navegação) + `DividaAtual`
- `ObservableCollection<MesPagamento> StatusPorMes` (tabela de status de pagamento)
- Formulário de pagamento: `MesSelecionado` (ComboBox), `ValorAReceber` (decimal), `FormaPagamento` (enum: Dinheiro/Transferência/Multicaixa)
- Resumo calculado: `Troco`/`ValorRestante`
- Commands: `ConfirmarPagamentoCommand` (async — grava + dispara emissão de recibo), `AbrirReciboRapidoCommand`
- Depende de: `IPagamentoService`, `INavigationService`

### 6.7 `EmitirReciboViewModel`
- Dados do recibo: `EscolaInfo`, `DestinatarioInfo`, `ObservableCollection<ItemRecibo> Itens`, `Total`
- `NumeroRecibo` (gerado automaticamente, sequencial)
- Props de assinatura: `ResponsavelEmissao`
- Commands: `GerarPdfCommand` (via `IReciboService`/QuestPDF), `ImprimirCommand`, `EnviarPorEmailCommand` *(se offline, deixar como "exportar PDF" apenas — sem envio real)*
- Depende de: `IReciboService`

### 6.8 `FechoCaixaViewModel`
- KPIs: `TotalEntradas`, `TotalSaidas`, `SaldoDoDia`
- `ObservableCollection<Transacao> Transacoes` (tabela detalhada)
- Painel de controlo: `SenhaConfirmacao` (campo de segurança para fechar caixa), `ObservacoesFecho`
- Mini-gráfico do saldo diário → dados para `LiveChartsCore` (`ISeries[] SaldoSeries`)
- Commands: `FecharCaixaCommand` (async, irreversível — pedir confirmação), `ExportarRelatorioCommand`
- Depende de: `ICaixaService`

### 6.9 `PerfilEstudanteViewModel`
- `AlunoSelecionado` (dados pessoais, foto, classe, encarregado)
- Tabs: `AbaSelecionada` (enum: Informações/Financeiro/Documentos) — Tabs do HTML viram `TabControl` ou `RadioButton`+`ContentControl` no Avalonia
- `ObservableCollection<PagamentoHistorico> HistoricoPagamentos`
- Widget financeiro: `SaldoDevedor`, `UltimoPagamento`
- Widget de matrícula: `DataMatricula`, `ClasseAtual`, `Turno`
- Commands: `EditarAlunoCommand`, `NovoPagamentoCommand` (navega pra `GestaoPagamentosViewModel` já com o aluno pré-selecionado), `EmitirReciboCommand`
- Depende de: `IAlunoService`, `IPagamentoService`

### Padrão comum de navegação com contexto
Várias telas recebem um "aluno selecionado" da tela anterior (ex.: Alunos → Perfil → Gestão de Pagamentos). Recomenda-se um `INavigationService.NavigateTo<TViewModel>(object parameter)` que injeta o parâmetro via um método `Initialize(object parameter)` chamado pelo Shell ao trocar `CurrentPage` — evita acoplar ViewModels entre si.

---

## 7. Observação importante: backend "offline"

Como o app vai rodar sem internet, **não pode chamar API externa**. Sugiro:
- Banco local **SQLite** (via `Microsoft.EntityFrameworkCore.Sqlite` — também baixar o pacote NuGet com antecedência) com tabela de usuários/credenciais.
- `AuthService` local faz hash da senha (ex. `BCrypt.Net-Next` — outro pacote NuGet a cachear) e compara contra o SQLite.
- O indicador "Servidor: Online" do footer original deixa de fazer sentido num app 100% offline — sugiro trocar para algo como "Modo Local" ou status do banco de dados local.

---

## 8. Resumo da checklist offline (antes de levar pra máquina sem internet)

- [ ] Instalador do .NET SDK 8 (offline installer)
- [ ] IDE instalado (VS/Rider/VSCode + extensões, todos via instalador local)
- [ ] Pasta `~/.nuget/packages` com todos os pacotes da lista da seção 2.3 (+ SQLite/BCrypt se for usar)
- [ ] `.ttf` de Poppins e Inter salvos em `Assets/Fonts`
- [ ] Imagem de fundo do login salva localmente
- [ ] Projeto publicado com `--self-contained true` para o RID certo (win-x64/linux-x64)

---

Quando quiseres, gero o XAML do `LoginView` + `LoginViewModel` reais com base nesta documentação.
