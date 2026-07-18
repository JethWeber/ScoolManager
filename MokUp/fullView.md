<!DOCTYPE html>

<html class="dark" lang="pt-PT"><head>
<meta charset="utf-8"/>
<meta content="width=device-width, initial-scale=1.0" name="viewport"/>
<title>Configurações | Gestão Escolar</title>
<script src="https://cdn.tailwindcss.com?plugins=forms,container-queries"></script>
<link href="https://fonts.googleapis.com/css2?family=Inter:wght@400;500;600;700&amp;family=Poppins:wght@500;600;700&amp;display=swap" rel="stylesheet"/>
<link href="https://fonts.googleapis.com/css2?family=Material+Symbols+Outlined:wght,FILL@100..700,0..1&amp;display=swap" rel="stylesheet"/>
<link href="https://fonts.googleapis.com/css2?family=Material+Symbols+Outlined:wght,FILL@100..700,0..1&amp;display=swap" rel="stylesheet"/>
<script id="tailwind-config">
      tailwind.config = {
        darkMode: "class",
        theme: {
          extend: {
            "colors": {
                    "on-secondary": "#263143",
                    "on-tertiary-fixed": "#001c39",
                    "error": "#ffb4ab",
                    "surface-container": "#171f33",
                    "error-container": "#93000a",
                    "on-surface-variant": "#c2c6d6",
                    "surface-bright": "#31394d",
                    "surface-tint": "#adc6ff",
                    "on-tertiary-container": "#002a51",
                    "on-primary-fixed-variant": "#004395",
                    "tertiary-fixed": "#d4e3ff",
                    "surface-dim": "#0b1326",
                    "outline": "#8c909f",
                    "secondary": "#bcc7de",
                    "on-tertiary": "#00315d",
                    "surface-container-low": "#131b2e",
                    "outline-variant": "#424754",
                    "secondary-fixed-dim": "#bcc7de",
                    "on-primary-container": "#00285d",
                    "on-background": "#dae2fd",
                    "surface-container-lowest": "#060e20",
                    "surface": "#0b1326",
                    "tertiary-fixed-dim": "#a4c9ff",
                    "on-primary": "#002e6a",
                    "primary-container": "#4d8eff",
                    "inverse-surface": "#dae2fd",
                    "surface-container-high": "#222a3d",
                    "on-error-container": "#ffdad6",
                    "on-secondary-fixed-variant": "#3c475a",
                    "background": "#0b1326",
                    "on-tertiary-fixed-variant": "#004883",
                    "on-error": "#690005",
                    "secondary-fixed": "#d8e3fb",
                    "primary-fixed": "#d8e2ff",
                    "on-primary-fixed": "#001a42",
                    "tertiary-container": "#4c93e7",
                    "primary": "#adc6ff",
                    "tertiary": "#a4c9ff",
                    "surface-container-highest": "#2d3449",
                    "surface-variant": "#2d3449",
                    "on-surface": "#dae2fd",
                    "secondary-container": "#3e495d",
                    "on-secondary-container": "#aeb9d0",
                    "inverse-primary": "#005ac2",
                    "primary-fixed-dim": "#adc6ff",
                    "on-secondary-fixed": "#111c2d",
                    "inverse-on-surface": "#283044"
            },
            "borderRadius": {
                    "DEFAULT": "0.25rem",
                    "lg": "0.5rem",
                    "xl": "0.75rem",
                    "full": "9999px"
            },
            "spacing": {
                    "sidebar_width": "260px",
                    "lg": "24px",
                    "base": "4px",
                    "sm": "12px",
                    "xl": "32px",
                    "md": "16px",
                    "xs": "8px",
                    "gutter": "20px"
            },
            "fontFamily": {
                    "body-lg": ["Inter"],
                    "label-muted": ["Inter"],
                    "display-lg": ["Poppins"],
                    "label-bold": ["Inter"],
                    "data-numeric": ["Inter"],
                    "headline-sm": ["Poppins"],
                    "body-md": ["Inter"],
                    "body-sm": ["Inter"],
                    "display-md": ["Poppins"]
            },
            "fontSize": {
                    "body-lg": ["16px", {"lineHeight": "1.5", "fontWeight": "400"}],
                    "label-muted": ["12px", {"lineHeight": "1", "fontWeight": "400"}],
                    "display-lg": ["32px", {"lineHeight": "1.2", "letterSpacing": "-0.02em", "fontWeight": "600"}],
                    "label-bold": ["12px", {"lineHeight": "1", "letterSpacing": "0.05em", "fontWeight": "600"}],
                    "data-numeric": ["20px", {"lineHeight": "1", "letterSpacing": "-0.01em", "fontWeight": "700"}],
                    "headline-sm": ["18px", {"lineHeight": "1.4", "fontWeight": "500"}],
                    "body-md": ["14px", {"lineHeight": "1.5", "fontWeight": "400"}],
                    "body-sm": ["13px", {"lineHeight": "1.5", "fontWeight": "400"}],
                    "display-md": ["24px", {"lineHeight": "1.3", "fontWeight": "600"}]
            }
          },
        },
      }
    </script>
<style>
        .neo-glass {
            background: rgba(23, 31, 51, 0.7);
            backdrop-filter: blur(12px);
            border: 1px solid rgba(255, 255, 255, 0.05);
            box-shadow: inset 1px 1px 0px rgba(255, 255, 255, 0.05),
                        20px 20px 40px rgba(0, 0, 0, 0.4);
        }
        .neo-inset {
            background: #0b1326;
            border: 1px solid #334155;
            box-shadow: inset 2px 2px 5px rgba(0,0,0,0.5);
        }
        .neo-btn-primary {
            background: linear-gradient(135deg, #3B82F6 0%, #2563EB 100%);
            box-shadow: 0px 4px 12px rgba(59, 130, 246, 0.3),
                        inset 0px 1px 0px rgba(255, 255, 255, 0.2);
            transition: all 0.2s ease;
        }
        .neo-btn-primary:active {
            transform: scale(0.96);
            box-shadow: 0px 2px 4px rgba(59, 130, 246, 0.2);
        }
        .custom-scrollbar::-webkit-scrollbar {
            width: 6px;
        }
        .custom-scrollbar::-webkit-scrollbar-track {
            background: transparent;
        }
        .custom-scrollbar::-webkit-scrollbar-thumb {
            background: #2d3449;
            border-radius: 10px;
        }
        .tab-active {
            position: relative;
        }
        .tab-active::after {
            content: '';
            position: absolute;
            bottom: -2px;
            left: 0;
            width: 100%;
            height: 3px;
            background: #adc6ff;
            border-radius: 99px;
            box-shadow: 0 0 8px #adc6ff;
        }
    </style>
</head>
<body class="bg-background text-on-surface font-body-md selection:bg-primary selection:text-on-primary overflow-hidden h-screen w-screen">
<!-- Desktop Side Navigation -->
<aside class="fixed left-0 top-0 h-full w-[260px] bg-surface-container-low dark:bg-surface-container-low shadow-[20px_0_40px_rgba(0,0,0,0.4)] flex flex-col py-lg z-50">
<div class="px-xl mb-xl">
<h1 class="font-display-md text-display-md font-bold text-primary dark:text-primary">Gestão Escolar</h1>
<p class="text-on-surface-variant/60 font-label-muted text-label-muted mt-1 uppercase tracking-widest">Administração Central</p>
</div>
<nav class="flex-1 space-y-2">
<!-- Dashboard -->
<a class="flex items-center px-lg py-sm text-on-surface-variant hover:text-on-surface transition-colors hover:bg-surface-container-high group" href="../dashboard_school_manager_refatorado/code.html">
<span class="material-symbols-outlined mr-md" data-icon="dashboard">dashboard</span>
<span class="font-body-md">Dashboard</span>
</a>
<!-- Alunos -->
<a class="flex items-center px-lg py-sm text-on-surface-variant hover:text-on-surface transition-colors hover:bg-surface-container-high group" href="../gest_o_de_alunos_school_manager/code.html">
<span class="material-symbols-outlined mr-md" data-icon="group">group</span>
<span class="font-body-md">Alunos</span>
</a>
<!-- Financeiro -->
<a class="flex items-center px-lg py-sm text-on-surface-variant hover:text-on-surface transition-colors hover:bg-surface-container-high group" href="../financeiro_pagamentos_school_manager/code.html">
<span class="material-symbols-outlined mr-md" data-icon="account_balance_wallet">account_balance_wallet</span>
<span class="font-body-md">Financeiro</span>
</a>
<!-- Relatórios -->
<a class="flex items-center px-lg py-sm text-on-surface-variant hover:text-on-surface transition-colors hover:bg-surface-container-high group" href="../relat_rios_administrativos_school_manager/code.html">
<span class="material-symbols-outlined mr-md" data-icon="analytics">analytics</span>
<span class="font-body-md">Relatórios</span>
</a>
<!-- Escola -->
<a class="flex items-center px-lg py-sm text-on-surface-variant hover:text-on-surface transition-colors hover:bg-surface-container-high group" href="../escola_classes_school_manager/code.html">
<span class="material-symbols-outlined mr-md" data-icon="school">school</span>
<span class="font-body-md">Escola</span>
</a>
<!-- Configurações (Active) -->
<a class="flex items-center px-lg py-sm bg-primary text-on-primary rounded-r-full shadow-[0_4px_12px_rgba(59,130,246,0.3)] active:scale-95 duration-200" href="code.html">
<span class="material-symbols-outlined mr-md" data-icon="settings">settings</span>
<span class="font-body-md">Configurações</span>
</a>
</nav>
<div class="px-lg pt-xl border-t border-outline-variant/10">
<div class="flex items-center space-x-3 p-sm neo-glass rounded-xl">
<div class="w-10 h-10 rounded-lg overflow-hidden flex-shrink-0">
<img class="w-full h-full object-cover" data-alt="A formal and professional portrait of a school administrative officer in a suit, captured in a modern office with soft cinematic blue lighting. The style is hyper-realistic and corporate, emphasizing authority and trust. The overall aesthetic is dark-themed to match a sophisticated administrative software dashboard." src="https://lh3.googleusercontent.com/aida-public/AB6AXuAVvNsMrHOytZ-Xj7c-w8_QRhUIbiHBBbn8LC62h123f_PtOq7Uq53Dlu_m9ePFKrTKFLiE7HG-pEhUAxCdEBT5UzWFgWE3meqH4uOfPm09fC0EpeahBlByLwkvy_Jm7JKtVubq9tFHyv4RNVznoJ3TKeJ2-FNnvoQ4_E1OzpCer4CuyUfcnG_iZB_OvzwYNnM8KDje4j5C9EEuk1RdAcuF0dgRI7Ie0yurUjXMt-h52vDQDUy3jf43GhiNiKj8FeTTThhrFHrThwv2"/>
</div>
<div class="flex-1 min-w-0">
<p class="font-label-bold text-on-surface truncate">Dr. Ricardo Silva</p>
<p class="font-label-muted text-on-surface-variant/70">Diretor Geral</p>
</div>
</div>
</div>
</aside>
<!-- Top App Bar -->
<header class="fixed top-0 right-0 w-[calc(100%-260px)] h-16 bg-surface dark:bg-surface flex justify-between items-center px-xl border-b border-outline-variant/20 shadow-sm z-40">
<div class="flex items-center space-x-4">
<span class="font-headline-sm text-headline-sm font-semibold text-on-surface">Configurações do Sistema</span>
</div>
<div class="flex items-center space-x-xl">
<div class="relative w-72 focus-within:ring-2 focus-within:ring-primary rounded-full bg-surface-container-highest px-4 py-1.5 flex items-center">
<span class="material-symbols-outlined text-on-surface-variant mr-2" data-icon="search">search</span>
<input class="bg-transparent border-none focus:ring-0 text-body-sm w-full text-on-surface placeholder:text-on-surface-variant/50" placeholder="Procurar definição..." type="text"/>
</div>
<div class="flex items-center space-x-4">
<button class="w-10 h-10 flex items-center justify-center hover:bg-surface-container-highest rounded-full transition-all relative">
<span class="material-symbols-outlined" data-icon="notifications">notifications</span>
<span class="absolute top-2 right-2 w-2 h-2 bg-error rounded-full"></span>
</button>
<button class="w-10 h-10 flex items-center justify-center hover:bg-surface-container-highest rounded-full transition-all">
<span class="material-symbols-outlined" data-icon="account_circle">account_circle</span>
</button>
</div>
</div>
</header>
<!-- Main Content -->
<main class="fixed inset-0 left-[260px] top-16 bottom-8 overflow-y-auto custom-scrollbar p-xl bg-surface-dim">
<!-- Header & Tabs -->
<div class="mb-lg">
<div class="flex justify-between items-end border-b border-outline-variant/20 pb-4">
<div class="flex space-x-8">
<button class="pb-4 font-label-bold text-on-surface tab-active transition-all flex items-center gap-2" id="tab-institucional" onclick="switchTab('institucional')">
<span class="material-symbols-outlined text-[18px]" data-icon="school">school</span>
                        Dados da Escola
                    </button>
<button class="pb-4 font-label-bold text-on-surface-variant hover:text-on-surface transition-all flex items-center gap-2" id="tab-utilizadores" onclick="switchTab('utilizadores')">
<span class="material-symbols-outlined text-[18px]" data-icon="manage_accounts">manage_accounts</span>
                        Utilizadores
                    </button>
<button class="pb-4 font-label-bold text-on-surface-variant hover:text-on-surface transition-all flex items-center gap-2" id="tab-permissoes" onclick="switchTab('permissoes')">
<span class="material-symbols-outlined text-[18px]" data-icon="verified_user">verified_user</span>
                        Permissões
                    </button>
<button class="pb-4 font-label-bold text-on-surface-variant hover:text-on-surface transition-all flex items-center gap-2" id="tab-backup" onclick="switchTab('backup')">
<span class="material-symbols-outlined text-[18px]" data-icon="cloud_sync">cloud_sync</span>
                        Backup &amp; Segurança
                    </button>
</div>
<div class="pb-4">
<button class="neo-btn-primary px-xl py-2 rounded-lg font-label-bold text-white flex items-center gap-2">
<span class="material-symbols-outlined text-[18px]" data-icon="save">save</span>
                        Guardar Alterações
                    </button>
</div>
</div>
</div>
<!-- Tab Content: Institucional -->
<div class="grid grid-cols-12 gap-lg animate-in fade-in slide-in-from-bottom-4 duration-500" id="content-institucional">
<div class="col-span-8 space-y-lg">
<div class="neo-glass rounded-xl p-lg">
<h3 class="font-headline-sm text-primary mb-md flex items-center gap-2">
<span class="material-symbols-outlined" data-icon="info">info</span>
                        Informação Institucional
                    </h3>
<div class="grid grid-cols-2 gap-xl">
<div class="space-y-base">
<label class="font-label-muted text-on-surface-variant block">Nome Oficial da Instituição</label>
<input class="w-full neo-inset rounded-lg px-4 py-2.5 text-body-md focus:border-primary outline-none transition-all" type="text" value="Complexo Escolar Politécnico de Luanda"/>
</div>
<div class="space-y-base">
<label class="font-label-muted text-on-surface-variant block">NIF (Número de Identificação Fiscal)</label>
<input class="w-full neo-inset rounded-lg px-4 py-2.5 text-body-md focus:border-primary outline-none transition-all" type="text" value="5412009876"/>
</div>
<div class="space-y-base">
<label class="font-label-muted text-on-surface-variant block">Website</label>
<input class="w-full neo-inset rounded-lg px-4 py-2.5 text-body-md focus:border-primary outline-none transition-all" type="text" value="www.cepl-edu.ao"/>
</div>
<div class="space-y-base">
<label class="font-label-muted text-on-surface-variant block">E-mail Administrativo</label>
<input class="w-full neo-inset rounded-lg px-4 py-2.5 text-body-md focus:border-primary outline-none transition-all" type="email" value="geral@cepl-edu.ao"/>
</div>
</div>
</div>
<div class="neo-glass rounded-xl p-lg">
<h3 class="font-headline-sm text-primary mb-md flex items-center gap-2">
<span class="material-symbols-outlined" data-icon="location_on">location_on</span>
                        Localização e Contacto
                    </h3>
<div class="space-y-md">
<div class="space-y-base">
<label class="font-label-muted text-on-surface-variant block">Endereço Completo</label>
<textarea class="w-full neo-inset rounded-lg px-4 py-2.5 text-body-md focus:border-primary outline-none transition-all h-24 resize-none">Rua Direita de Luanda, Bairro Talatona, Sector C, Luanda, Angola</textarea>
</div>
<div class="grid grid-cols-2 gap-xl">
<div class="space-y-base">
<label class="font-label-muted text-on-surface-variant block">Telefone Principal</label>
<input class="w-full neo-inset rounded-lg px-4 py-2.5 text-body-md focus:border-primary outline-none transition-all" type="text" value="+244 923 000 000"/>
</div>
<div class="space-y-base">
<label class="font-label-muted text-on-surface-variant block">Telefone Secundário</label>
<input class="w-full neo-inset rounded-lg px-4 py-2.5 text-body-md focus:border-primary outline-none transition-all" type="text" value="+244 222 000 000"/>
</div>
</div>
</div>
</div>
</div>
<!-- Sidebar Content -->
<div class="col-span-4 space-y-lg">
<div class="neo-glass rounded-xl p-lg text-center overflow-hidden relative">

<div class="relative z-10">
<div class="w-32 h-32 mx-auto mb-md rounded-2xl neo-inset p-2 overflow-hidden bg-surface-container flex items-center justify-center">
<img class="max-w-full h-auto" data-alt="A clean, minimalist school crest or emblem in professional navy blue and gold colors. The design should feel authoritative, elegant, and modern, representing high-quality Angolan education. The style should be flat vector with subtle gradients to match a premium dark UI." src="https://lh3.googleusercontent.com/aida-public/AB6AXuDrZ2mGT_FINut0VeoOoJ5TDhc6D7nxbBi6q7xSRcpHvvgT0hj5XLHrVaRz6RSm0RDM4N3on8Bb9u_KTmGwMOYQvb2EpHH2ybTNa400aH1MVhcOuPILCvaJX8bFM7SZwgS6Q46U_t1l7fjo1lpMwuvA2PVEWYQ_Qv8Putk3yNmIlPWheGCBu0_5BA53xsUGwAkL-BBM8SMCmBOE_0Vj07xRu2vrGYR3q6TgAJRx040w-fF12SamVJJfwo0b3tYbnkPtyLQ7eKs1k6Le"/>
</div>
<button class="text-primary font-label-bold hover:underline mb-xs block w-full text-center">Alterar Logotipo</button>
<p class="text-on-surface-variant font-label-muted text-xs">PNG ou SVG, máx 2MB</p>
</div>
</div>
<div class="neo-glass rounded-xl p-lg">
<h4 class="font-label-bold text-on-surface mb-md">Estado do Sistema</h4>
<div class="space-y-md">
<div class="flex justify-between items-center p-sm neo-inset rounded-lg">
<div class="flex items-center gap-3">
<span class="w-2 h-2 bg-emerald-500 rounded-full shadow-[0_0_8px_rgba(16,185,129,0.8)]"></span>
<span class="text-body-sm">Licença Ativa</span>
</div>
<span class="text-on-surface-variant font-label-muted">Expira em 240 dias</span>
</div>
<div class="flex justify-between items-center p-sm neo-inset rounded-lg">
<div class="flex items-center gap-3">
<span class="material-symbols-outlined text-primary text-[18px]" data-icon="storage">storage</span>
<span class="text-body-sm">Espaço em Nuvem</span>
</div>
<span class="text-on-surface-variant font-label-muted">45.2 GB / 100 GB</span>
</div>
</div>
</div>
</div>
</div>
<!-- Tab Content: Utilizadores (Hidden by default) -->
<div class="hidden animate-in fade-in slide-in-from-bottom-4 duration-500" id="content-utilizadores">
<div class="neo-glass rounded-xl overflow-hidden">
<div class="p-lg flex justify-between items-center">
<h3 class="font-headline-sm text-primary">Gestão de Utilizadores</h3>
<button class="neo-btn-primary px-lg py-2 rounded-lg font-label-bold text-white text-sm flex items-center gap-2">
<span class="material-symbols-outlined text-[18px]" data-icon="person_add">person_add</span>
                        Novo Utilizador
                    </button>
</div>
<table class="w-full text-left">
<thead>
<tr class="bg-surface-container-highest/50">
<th class="px-lg py-md font-label-bold text-on-surface-variant">Nome</th>
<th class="px-lg py-md font-label-bold text-on-surface-variant">Cargo</th>
<th class="px-lg py-md font-label-bold text-on-surface-variant">Último Acesso</th>
<th class="px-lg py-md font-label-bold text-on-surface-variant">Estado</th>
<th class="px-lg py-md font-label-bold text-on-surface-variant">Ações</th>
</tr>
</thead>
<tbody class="divide-y divide-outline-variant/10">
<tr class="hover:bg-surface-container-high transition-colors">
<td class="px-lg py-md flex items-center gap-3">
<div class="w-8 h-8 rounded-full bg-primary-container text-on-primary-container flex items-center justify-center font-bold text-xs">RS</div>
<span class="text-body-md">Ricardo Silva</span>
</td>
<td class="px-lg py-md text-on-surface-variant">Diretor Geral</td>
<td class="px-lg py-md text-on-surface-variant">Hoje, 10:45</td>
<td class="px-lg py-md">
<span class="bg-emerald-500/10 text-emerald-400 px-2 py-1 rounded text-xs font-bold uppercase">Ativo</span>
</td>
<td class="px-lg py-md">
<button class="p-2 hover:bg-surface-container-highest rounded-lg transition-all">
<span class="material-symbols-outlined text-[20px]" data-icon="edit">edit</span>
</button>
<button class="p-2 hover:bg-surface-container-highest rounded-lg transition-all text-error/80">
<span class="material-symbols-outlined text-[20px]" data-icon="delete">delete</span>
</button>
</td>
</tr>
<tr class="hover:bg-surface-container-high transition-colors">
<td class="px-lg py-md flex items-center gap-3">
<div class="w-8 h-8 rounded-full bg-secondary-container text-on-secondary-container flex items-center justify-center font-bold text-xs">MA</div>
<span class="text-body-md">Maria Antónia</span>
</td>
<td class="px-lg py-md text-on-surface-variant">Tesoureira</td>
<td class="px-lg py-md text-on-surface-variant">Ontem, 16:30</td>
<td class="px-lg py-md">
<span class="bg-emerald-500/10 text-emerald-400 px-2 py-1 rounded text-xs font-bold uppercase">Ativo</span>
</td>
<td class="px-lg py-md">
<button class="p-2 hover:bg-surface-container-highest rounded-lg transition-all">
<span class="material-symbols-outlined text-[20px]" data-icon="edit">edit</span>
</button>
<button class="p-2 hover:bg-surface-container-highest rounded-lg transition-all text-error/80">
<span class="material-symbols-outlined text-[20px]" data-icon="delete">delete</span>
</button>
</td>
</tr>
</tbody>
</table>
</div>
</div>
<!-- Tab Content: Backup (Hidden by default) -->
<div class="hidden animate-in fade-in slide-in-from-bottom-4 duration-500" id="content-backup">
<div class="grid grid-cols-12 gap-lg">
<div class="col-span-7 space-y-lg">
<div class="neo-glass rounded-xl p-lg">
<div class="flex items-center justify-between mb-lg">
<h3 class="font-headline-sm text-primary flex items-center gap-2">
<span class="material-symbols-outlined" data-icon="history">history</span>
                                Histórico de Backups
                            </h3>
<button class="neo-btn-primary px-lg py-2 rounded-lg text-sm font-label-bold flex items-center gap-2">
<span class="material-symbols-outlined text-[18px]" data-icon="cloud_upload">cloud_upload</span>
                                Criar Backup Agora
                            </button>
</div>
<div class="space-y-md">
<div class="p-lg neo-inset rounded-xl flex items-center justify-between">
<div class="flex items-center gap-4">
<div class="w-12 h-12 rounded-lg bg-surface-container-highest flex items-center justify-center">
<span class="material-symbols-outlined text-primary text-[28px]" data-icon="description">description</span>
</div>
<div>
<p class="font-label-bold text-on-surface">backup_escolar_full_20231024.sql</p>
<p class="text-xs text-on-surface-variant">24 Out 2023 | 124.5 MB | Servidor Local</p>
</div>
</div>
<button class="p-3 hover:bg-surface-container-high rounded-full transition-all text-primary">
<span class="material-symbols-outlined" data-icon="download">download</span>
</button>
</div>
<div class="p-lg neo-inset rounded-xl flex items-center justify-between">
<div class="flex items-center gap-4">
<div class="w-12 h-12 rounded-lg bg-surface-container-highest flex items-center justify-center">
<span class="material-symbols-outlined text-primary text-[28px]" data-icon="cloud_done">cloud_done</span>
</div>
<div>
<p class="font-label-bold text-on-surface">daily_automatic_cloud_sync.bak</p>
<p class="text-xs text-on-surface-variant">Hoje, 04:00 | 128.2 MB | Google Drive</p>
</div>
</div>
<button class="p-3 hover:bg-surface-container-high rounded-full transition-all text-primary">
<span class="material-symbols-outlined" data-icon="restore">restore</span>
</button>
</div>
</div>
</div>
</div>
<div class="col-span-5">
<div class="neo-glass rounded-xl p-lg space-y-lg">
<h3 class="font-label-bold text-on-surface uppercase tracking-wider">Configurações Automáticas</h3>
<div class="space-y-lg">
<div class="flex items-center justify-between">
<div>
<p class="font-body-md">Backup Diário Automático</p>
<p class="text-xs text-on-surface-variant">Executar todos os dias às 03:00 AM</p>
</div>
<label class="relative inline-flex items-center cursor-pointer">
<input checked="" class="sr-only peer" type="checkbox"/>
<div class="w-11 h-6 bg-surface-container-highest rounded-full peer peer-checked:after:translate-x-full peer-checked:after:border-white after:content-[''] after:absolute after:top-[2px] after:left-[2px] after:bg-white after:border-gray-300 after:border after:rounded-full after:h-5 after:w-5 after:transition-all peer-checked:bg-primary"></div>
</label>
</div>
<div class="flex items-center justify-between">
<div>
<p class="font-body-md">Sincronização em Nuvem</p>
<p class="text-xs text-on-surface-variant">Enviar cópia para Google Drive/AWS</p>
</div>
<label class="relative inline-flex items-center cursor-pointer">
<input checked="" class="sr-only peer" type="checkbox"/>
<div class="w-11 h-6 bg-surface-container-highest rounded-full peer peer-checked:after:translate-x-full peer-checked:after:border-white after:content-[''] after:absolute after:top-[2px] after:left-[2px] after:bg-white after:border-gray-300 after:border after:rounded-full after:h-5 after:w-5 after:transition-all peer-checked:bg-primary"></div>
</label>
</div>
<div class="flex items-center justify-between">
<div>
<p class="font-body-md">Notificar Falhas por E-mail</p>
<p class="text-xs text-on-surface-variant">Alertar administrador em caso de erro</p>
</div>
<label class="relative inline-flex items-center cursor-pointer">
<input class="sr-only peer" type="checkbox"/>
<div class="w-11 h-6 bg-surface-container-highest rounded-full peer peer-checked:after:translate-x-full peer-checked:after:border-white after:content-[''] after:absolute after:top-[2px] after:left-[2px] after:bg-white after:border-gray-300 after:border after:rounded-full after:h-5 after:w-5 after:transition-all peer-checked:bg-primary"></div>
</label>
</div>
</div>
<div class="pt-lg">
<div class="p-lg neo-inset rounded-xl bg-primary/5 border-primary/20 flex flex-col items-center text-center">
<span class="material-symbols-outlined text-primary text-[48px] mb-2" data-icon="verified">verified</span>
<p class="text-on-surface font-body-md font-semibold">Base de Dados Saudável</p>
<p class="text-on-surface-variant text-xs mt-1">Última verificação de integridade realizada há 2 horas. Nenhum erro encontrado.</p>
</div>
</div>
</div>
</div>
</div>
</div>
</main>
<!-- Footer -->
<footer class="fixed bottom-0 right-0 w-[calc(100%-260px)] h-8 bg-surface-container-lowest dark:bg-surface-container-lowest flex justify-between items-center px-xl text-label-muted font-label-muted z-40">
<div class="text-on-surface-variant/60">
            v2.4.1 | Status do Backup: Sincronizado
        </div>
<div class="flex space-x-lg text-on-surface-variant/60">
<a class="hover:text-primary transition-colors" href="#">Suporte</a>
<a class="hover:text-primary transition-colors" href="#">Documentação</a>
</div>
</footer>
<script>
        function switchTab(tabName) {
            // Hide all contents
            document.querySelectorAll('[id^="content-"]').forEach(el => el.classList.add('hidden'));
            // Remove active style from all buttons
            document.querySelectorAll('[id^="tab-"]').forEach(el => {
                el.classList.remove('text-on-surface', 'tab-active');
                el.classList.add('text-on-surface-variant');
            });

            // Show selected content
            const activeContent = document.getElementById('content-' + tabName);
            if(activeContent) activeContent.classList.remove('hidden');

            // Add active style to selected button
            const activeTab = document.getElementById('tab-' + tabName);
            if(activeTab) {
                activeTab.classList.remove('text-on-surface-variant');
                activeTab.classList.add('text-on-surface', 'tab-active');
            }
        }
    </script>
</body></html>



<!DOCTYPE html>

<html class="dark" lang="pt-ao"><head>
<meta charset="utf-8"/>
<meta content="width=device-width, initial-scale=1.0" name="viewport"/>
<title>Centro de Cobrança - School Manager</title>
<script src="https://cdn.tailwindcss.com?plugins=forms,container-queries"></script>
<link href="https://fonts.googleapis.com/css2?family=Poppins:wght@400;500;600;700&amp;family=Inter:wght@400;600;700&amp;display=swap" rel="stylesheet"/>
<link href="https://fonts.googleapis.com/css2?family=Material+Symbols+Outlined:wght,FILL@100..700,0..1&amp;display=swap" rel="stylesheet"/>
<link href="https://fonts.googleapis.com/css2?family=Material+Symbols+Outlined:wght,FILL@100..700,0..1&amp;display=swap" rel="stylesheet"/>
<script id="tailwind-config">
        tailwind.config = {
            darkMode: "class",
            theme: {
                extend: {
                    "colors": {
                        "on-tertiary-container": "#002a51",
                        "on-primary-container": "#00285d",
                        "tertiary-container": "#4c93e7",
                        "on-tertiary-fixed-variant": "#004883",
                        "primary": "#adc6ff",
                        "secondary": "#bcc7de",
                        "inverse-on-surface": "#283044",
                        "surface-tint": "#adc6ff",
                        "surface-container-lowest": "#060e20",
                        "secondary-fixed": "#d8e3fb",
                        "on-error-container": "#ffdad6",
                        "on-primary-fixed-variant": "#004395",
                        "tertiary-fixed": "#d4e3ff",
                        "on-surface": "#dae2fd",
                        "surface-dim": "#0b1326",
                        "on-surface-variant": "#c2c6d6",
                        "surface-variant": "#2d3449",
                        "error-container": "#93000a",
                        "surface-bright": "#31394d",
                        "on-primary-fixed": "#001a42",
                        "on-secondary-fixed-variant": "#3c475a",
                        "surface-container-high": "#222a3d",
                        "surface": "#0b1326",
                        "primary-fixed": "#d8e2ff",
                        "on-tertiary-fixed": "#001c39",
                        "secondary-fixed-dim": "#bcc7de",
                        "tertiary-fixed-dim": "#a4c9ff",
                        "surface-container": "#171f33",
                        "on-tertiary": "#00315d",
                        "on-primary": "#002e6a",
                        "background": "#0b1326",
                        "tertiary": "#a4c9ff",
                        "on-secondary-container": "#aeb9d0",
                        "surface-container-low": "#131b2e",
                        "on-secondary-fixed": "#111c2d",
                        "on-secondary": "#263143",
                        "primary-fixed-dim": "#adc6ff",
                        "surface-container-highest": "#2d3449",
                        "inverse-surface": "#dae2fd",
                        "primary-container": "#4d8eff",
                        "error": "#ffb4ab",
                        "on-background": "#dae2fd",
                        "outline": "#8c909f",
                        "on-error": "#690005",
                        "outline-variant": "#424754",
                        "secondary-container": "#3e495d",
                        "inverse-primary": "#005ac2"
                    },
                    "borderRadius": {
                        "DEFAULT": "0.25rem",
                        "lg": "0.5rem",
                        "xl": "0.75rem",
                        "full": "9999px"
                    },
                    "spacing": {
                        "xs": "8px",
                        "xl": "32px",
                        "md": "16px",
                        "sm": "12px",
                        "gutter": "20px",
                        "sidebar_width": "260px",
                        "lg": "24px",
                        "base": "4px"
                    },
                    "fontFamily": {
                        "display-lg": ["Poppins"],
                        "data-numeric": ["Inter"],
                        "body-md": ["Inter"],
                        "body-sm": ["Inter"],
                        "body-lg": ["Inter"],
                        "headline-sm": ["Poppins"],
                        "label-muted": ["Inter"],
                        "display-md": ["Poppins"],
                        "label-bold": ["Inter"]
                    },
                    "fontSize": {
                        "display-lg": ["32px", {"lineHeight": "1.2", "letterSpacing": "-0.02em", "fontWeight": "600"}],
                        "data-numeric": ["20px", {"lineHeight": "1", "letterSpacing": "-0.01em", "fontWeight": "700"}],
                        "body-md": ["14px", {"lineHeight": "1.5", "fontWeight": "400"}],
                        "body-sm": ["13px", {"lineHeight": "1.5", "fontWeight": "400"}],
                        "body-lg": ["16px", {"lineHeight": "1.5", "fontWeight": "400"}],
                        "headline-sm": ["18px", {"lineHeight": "1.4", "fontWeight": "500"}],
                        "label-muted": ["12px", {"lineHeight": "1", "fontWeight": "400"}],
                        "display-md": ["24px", {"lineHeight": "1.3", "fontWeight": "600"}],
                        "label-bold": ["12px", {"lineHeight": "1", "letterSpacing": "0.05em", "fontWeight": "600"}]
                    }
                },
            },
        }
    </script>
<style>
        .material-symbols-outlined {
            font-variation-settings: 'FILL' 0, 'wght' 400, 'GRAD' 0, 'opsz' 24;
        }
        
        .neo-pressed {
            box-shadow: inset 4px 4px 8px rgba(0, 0, 0, 0.4), 
                        inset -4px -4px 8px rgba(255, 255, 255, 0.05);
        }
        
        .neo-float {
            box-shadow: 0 4px 12px rgba(0, 0, 0, 0.4),
                        inset 1px 1px 0px rgba(255, 255, 255, 0.05);
        }

        .glass-panel {
            background: rgba(19, 27, 46, 0.6);
            backdrop-filter: blur(12px);
            border: 1px solid rgba(255, 255, 255, 0.05);
        }

        ::-webkit-scrollbar {
            width: 8px;
        }
        ::-webkit-scrollbar-track {
            background: #0b1326;
        }
        ::-webkit-scrollbar-thumb {
            background: #2d3449;
            border-radius: 4px;
        }
        ::-webkit-scrollbar-thumb:hover {
            background: #424754;
        }
    </style>
</head>
<body class="bg-background text-on-surface font-body-md overflow-hidden flex h-screen">
<!-- Fixed SideNavBar (JSON Derived) -->
<aside class="fixed left-0 top-0 h-full p-md space-y-lg w-[260px] flex flex-col border-r border-outline-variant/10 bg-surface-container-low dark:bg-surface-container-low shadow-xl">
<div class="flex items-center gap-sm px-sm py-md">
<div class="w-10 h-10 bg-primary-container flex items-center justify-center rounded-lg shadow-lg">
<span class="material-symbols-outlined text-on-primary-container" style="font-variation-settings: 'FILL' 1;">school</span>
</div>
<div>
<h1 class="text-headline-sm font-display-lg text-on-surface tracking-tight">School Manager</h1>
<p class="font-label-muted text-label-muted">Gestão Escolar</p>
</div>
</div>
<nav class="flex-1 flex flex-col gap-xs overflow-y-auto pr-xs">
<!-- Dashboard Tab -->
<a class="flex items-center gap-md p-md text-on-surface-variant hover:text-on-surface hover:bg-surface-variant/50 transition-all duration-200" href="../dashboard_school_manager_refatorado/code.html">
<span class="material-symbols-outlined">dashboard</span>
<span class="font-label-bold text-label-bold">Dashboard</span>
</a>
<!-- Alunos Tab -->
<a class="flex items-center gap-md p-md text-on-surface-variant hover:text-on-surface hover:bg-surface-variant/50 transition-all duration-200" href="../gest_o_de_alunos_school_manager/code.html">
<span class="material-symbols-outlined">group</span>
<span class="font-label-bold text-label-bold">Alunos</span>
</a>
<!-- Pagamentos Tab (Active Context for Debt Collection) -->
<a class="flex items-center gap-md p-md bg-primary-container text-on-primary-container rounded-lg font-label-bold shadow-[0_4px_12px_rgba(77,142,255,0.3)] active:scale-[0.98] transition-transform" href="../financeiro_pagamentos_school_manager/code.html">
<span class="material-symbols-outlined" style="font-variation-settings: 'FILL' 1;">payments</span>
<span class="font-label-bold text-label-bold">Pagamentos</span>
</a>
<!-- Recibos Tab -->
<a class="flex items-center gap-md p-md text-on-surface-variant hover:text-on-surface hover:bg-surface-variant/50 transition-all duration-200" 
<!-- Entradas Tab -->
<a class="flex items-center gap-md p-md text-on-surface-variant hover:text-on-surface hover:bg-surface-variant/50 transition-all duration-200" href="../financeiro_entradas_school_manager/code.html">
<span class="material-symbols-outlined">login</span>
<span class="font-label-bold text-label-bold">Entradas</span>
</a>
<!-- Saídas Tab -->
<a class="flex items-center gap-md p-md text-on-surface-variant hover:text-on-surface hover:bg-surface-variant/50 transition-all duration-200" href="../financeiro_sa_das_school_manager/code.html">
<span class="material-symbols-outlined">logout</span>
<span class="font-label-bold text-label-bold">Saídas</span>
</a>
<!-- Relatórios Tab -->
<a class="flex items-center gap-md p-md text-on-surface-variant hover:text-on-surface hover:bg-surface-variant/50 transition-all duration-200" href="../relat_rios_administrativos_school_manager/code.html">
<span class="material-symbols-outlined">analytics</span>
<span class="font-label-bold text-label-bold">Relatórios</span>
</a>
<!-- Configurações Tab -->
<a class="flex items-center gap-md p-md text-on-surface-variant hover:text-on-surface hover:bg-surface-variant/50 transition-all duration-200" href="../configura_es_do_sistema_school_manager/code.html">
<span class="material-symbols-outlined">settings</span>
<span class="font-label-bold text-label-bold">Configurações</span>
</a>
</nav>
<div class="pt-md border-t border-outline-variant/10 space-y-xs">
<div class="flex items-center gap-sm p-sm rounded-xl bg-surface-container-high neo-float">
<img class="w-10 h-10 rounded-full border-2 border-primary/30 object-cover" data-alt="A close-up high-resolution profile portrait of a professional administrative secretary in a dark modern office environment. She is wearing professional attire, illuminated by the soft blue glow of a computer screen. The background is softly blurred, showing a futuristic enterprise workspace with dark furniture and subtle lighting accents, reflecting a sophisticated Angolan fintech aesthetic." src="https://lh3.googleusercontent.com/aida-public/AB6AXuDOWvs3AsVmPCsd343g7RW807QXBAqfq-XcBAkkXTCywfYxN1SquZoYcszJnHLPTMyYQ-Lir9iCEPhJmIa7HyUmHNKsfjpGuUTDpsvL-4Mxe4HfHGXksuAIdh26Kp4F5bupVbxkl_vuXTJf_1jsrWgID9fV8YBJq0UOF37CqGep3B2-tW54ACeCePGjuJ6abMsBiw2sRtKu-eDmedn_Cwh2USy6QGM58Rn1nAx0uIXS7IN4gCIYKDVwj1axPLj6K5lCwAxUSGQkplpO"/>
<div>
<p class="font-label-bold text-on-surface leading-none">Secretaria</p>
<p class="font-label-muted text-label-muted">Administrador</p>
</div>
<div class="ml-auto w-2 h-2 bg-emerald-500 rounded-full shadow-[0_0_8px_#10b981]"></div>
</div>
<button class="w-full flex items-center gap-md p-md text-on-surface-variant hover:text-error transition-colors">
<span class="material-symbols-outlined">logout</span>
<span class="font-label-bold text-label-bold">Sair do Sistema</span>
</button>
</div>
</aside>
<!-- Main Content Canvas -->
<main class="flex-1 ml-[260px] flex flex-col overflow-hidden">
<!-- TopAppBar (JSON Derived) -->
<header class="flex flex-row justify-between items-center w-full max-w-[calc(100vw-260px)] h-auto py-lg px-xl bg-transparent">
<div>
<h2 class="font-display-md text-display-md text-primary tracking-tight">Alunos com Pagamentos em Atraso</h2>
<p class="font-body-sm text-label-muted">Gestão de inadimplência e recuperação de crédito</p>
</div>
<div class="flex items-center gap-md">
<div class="relative group">
<input class="bg-surface-container-lowest border border-outline-variant/30 rounded-full pl-xl pr-md py-xs text-body-sm text-on-surface focus:outline-none focus:border-primary transition-all w-64 neo-pressed" placeholder="Pesquisar devedor..." type="text"/>
<span class="material-symbols-outlined absolute left-md top-1/2 -translate-y-1/2 text-on-surface-variant text-[18px]">search</span>
</div>
<button class="hover:bg-surface-container-high rounded-full p-xs transition-colors text-on-surface-variant flex items-center justify-center">
<span class="material-symbols-outlined">calendar_today</span>
</button>
<div class="relative">
<button class="hover:bg-surface-container-high rounded-full p-xs transition-colors text-on-surface-variant flex items-center justify-center">
<span class="material-symbols-outlined">notifications</span>
</button>
<span class="absolute top-1 right-1 w-2.5 h-2.5 bg-error rounded-full border-2 border-background"></span>
</div>
</div>
</header>
<!-- Content Area -->
<div class="flex-1 overflow-y-auto px-xl pb-xl space-y-lg">
<!-- KPI Section (Bento Grid) -->
<section class="grid grid-cols-1 md:grid-cols-3 gap-lg"><!-- Total em Dívida -->
<div class="p-lg rounded-xl bg-surface-container-low neo-float border-t border-l border-white/5 flex flex-col justify-between group hover:bg-surface-container-high transition-colors">
<div class="flex justify-between items-start">
<div class="w-12 h-12 bg-primary/20 rounded-lg flex items-center justify-center shadow-lg">
<span class="material-symbols-outlined text-primary" style="font-variation-settings: 'FILL' 1;">account_balance_wallet</span>
</div>
</div>
<div class="mt-xl">
<p class="font-label-muted text-label-muted mb-xs">Total em Dívida (Kz)</p>
<h3 class="font-data-numeric text-display-md text-on-surface">3.450.000 <span class="text-label-muted text-[14px] font-normal">Kz</span></h3>
</div>
</div>
<!-- Nº de Alunos Devedores -->
<div class="p-lg rounded-xl bg-surface-container-low neo-float border-t border-l border-white/5 flex flex-col justify-between group hover:bg-surface-container-high transition-colors">
<div class="flex justify-between items-start">
<div class="w-12 h-12 bg-tertiary/20 rounded-lg flex items-center justify-center shadow-lg">
<span class="material-symbols-outlined text-tertiary" style="font-variation-settings: 'FILL' 1;">group_off</span>
</div>
</div>
<div class="mt-xl">
<p class="font-label-muted text-label-muted mb-xs">Nº de Alunos Devedores</p>
<h3 class="font-data-numeric text-display-md text-on-surface">142 <span class="text-label-muted text-[14px] font-normal">Alunos</span></h3>
</div>
</div>
<!-- Média de Meses em Atraso -->
<div class="p-lg rounded-xl bg-surface-container-low neo-float border-t border-l border-white/5 flex flex-col justify-between group hover:bg-surface-container-high transition-colors">
<div class="flex justify-between items-start">
<div class="w-12 h-12 bg-emerald-500/20 rounded-lg flex items-center justify-center shadow-lg">
<span class="material-symbols-outlined text-emerald-400" style="font-variation-settings: 'FILL' 1;">calendar_month</span>
</div>
</div>
<div class="mt-xl">
<p class="font-label-muted text-label-muted mb-xs">Média de Meses em Atraso</p>
<h3 class="font-data-numeric text-display-md text-on-surface">2.4 <span class="text-label-muted text-[14px] font-normal">Meses</span></h3>
</div>
</div></section>
<!-- Main Data Grid & Quick Actions -->
<div class="grid grid-cols-12 gap-lg h-full">
<!-- Data Table (Windows Style) -->
<section class="col-span-12 lg:col-span-9 bg-surface-container-low rounded-xl neo-float border border-outline-variant/10 overflow-hidden flex flex-col">
<div class="p-md px-lg border-b border-outline-variant/10 flex justify-between items-center bg-surface-container/50">
<h4 class="font-label-bold text-on-surface uppercase tracking-wider">Lista de Inadimplentes</h4>
<div class="flex gap-xs">
<button class="bg-surface-container-high text-on-surface-variant px-sm py-xs rounded-lg text-body-sm hover:text-on-surface flex items-center gap-xs transition-colors">
<span class="material-symbols-outlined text-[18px]">filter_list</span>
                                Filtrar
                            </button>
<button class="bg-surface-container-high text-on-surface-variant px-sm py-xs rounded-lg text-body-sm hover:text-on-surface flex items-center gap-xs transition-colors">
<span class="material-symbols-outlined text-[18px]">download</span>
                                Exportar
                            </button>
</div>
</div>
<div class="overflow-x-auto">
<table class="w-full text-left border-collapse">
<thead>
<tr class="bg-surface-container-lowest/30 border-b border-outline-variant/20"><th class="px-lg py-md font-label-bold text-label-muted uppercase text-[11px]">Estudante</th>
<th class="px-md py-md font-label-bold text-label-muted uppercase text-[11px]">Classe</th>
<th class="px-md py-md font-label-bold text-label-muted uppercase text-[11px]">Meses em Falta</th>
<th class="px-md py-md font-label-bold text-label-muted uppercase text-[11px]">Último Pagamento</th>
<th class="px-md py-md font-label-bold text-label-muted uppercase text-[11px] text-right">Valor Total em Dívida</th>
<th class="px-lg py-md font-label-bold text-label-muted uppercase text-[11px] text-center">Ações</th></tr>
</thead>
<tbody class="divide-y divide-outline-variant/10"><tr class="hover:bg-surface-variant/30 transition-colors group cursor-pointer">
<td class="px-lg py-md">
<div class="flex items-center gap-sm">
<div class="w-8 h-8 rounded-full bg-secondary-container flex items-center justify-center font-label-bold text-on-secondary-container">JS</div>
<span class="font-body-md text-on-surface">João Pedro da Silva</span>
</div>
</td>
<td class="px-md py-md font-body-sm text-on-surface-variant">8ª Classe A</td>
<td class="px-md py-md">
<span class="px-xs py-[2px] bg-error-container/30 text-error rounded-full text-[11px] font-label-bold">3 Meses</span>
</td>
<td class="px-md py-md font-body-sm text-on-surface-variant">15 Out 2023</td>
<td class="px-md py-md text-right font-data-numeric text-error">120.000 <span class="text-[11px] opacity-70">Kz</span></td>
<td class="px-lg py-md text-center">
<div class="flex items-center justify-center gap-xs">
<button class="p-xs hover:bg-primary/10 text-primary rounded transition-colors" title="Notificar">
<span class="material-symbols-outlined text-[18px]">notifications</span>
</button>
<button class="p-xs hover:bg-surface-variant text-on-surface-variant rounded transition-colors" title="Ver Perfil">
<span class="material-symbols-outlined text-[18px]">person</span>
</button>
</div>
</td>
</tr>
<tr class="hover:bg-surface-variant/30 transition-colors group cursor-pointer">
<td class="px-lg py-md">
<div class="flex items-center gap-sm">
<div class="w-8 h-8 rounded-full bg-secondary-container flex items-center justify-center font-label-bold text-on-secondary-container">MA</div>
<span class="font-body-md text-on-surface">Maria Luísa Alberto</span>
</div>
</td>
<td class="px-md py-md font-body-sm text-on-surface-variant">9ª Classe B</td>
<td class="px-md py-md">
<span class="px-xs py-[2px] bg-surface-variant text-on-surface-variant rounded-full text-[11px] font-label-bold">1 Mês</span>
</td>
<td class="px-md py-md font-body-sm text-on-surface-variant">12 Dez 2023</td>
<td class="px-md py-md text-right font-data-numeric text-primary">95.000 <span class="text-[11px] opacity-70">Kz</span></td>
<td class="px-lg py-md text-center">
<div class="flex items-center justify-center gap-xs">
<button class="p-xs hover:bg-primary/10 text-primary rounded transition-colors" title="Notificar">
<span class="material-symbols-outlined text-[18px]">notifications</span>
</button>
<button class="p-xs hover:bg-surface-variant text-on-surface-variant rounded transition-colors" title="Ver Perfil">
<span class="material-symbols-outlined text-[18px]">person</span>
</button>
</div>
</td>
</tr></tbody>
</table>
</div>
<!-- Pagination Placeholder -->
<div class="mt-auto p-md px-lg border-t border-outline-variant/10 flex justify-between items-center text-label-muted">
<span class="text-[11px]">Mostrando 5 de 142 devedores</span>
<div class="flex gap-xs">
<button class="w-8 h-8 rounded-lg border border-outline-variant/20 flex items-center justify-center hover:bg-surface-container-high transition-colors">
<span class="material-symbols-outlined text-[18px]">chevron_left</span>
</button>
<button class="w-8 h-8 rounded-lg border border-primary/40 bg-primary/10 text-primary flex items-center justify-center font-label-bold">1</button>
<button class="w-8 h-8 rounded-lg border border-outline-variant/20 flex items-center justify-center hover:bg-surface-container-high transition-colors">2</button>
<button class="w-8 h-8 rounded-lg border border-outline-variant/20 flex items-center justify-center hover:bg-surface-container-high transition-colors">
<span class="material-symbols-outlined text-[18px]">chevron_right</span>
</button>
</div>
</div>
</section>
<!-- Quick Actions & Summary (Right Panel) -->
<aside class="col-span-12 lg:col-span-3 space-y-lg">
<!-- Actions Panel -->
<div class="p-lg rounded-xl bg-surface-container-low neo-float border border-outline-variant/10 flex flex-col gap-md">
<h4 class="font-label-bold text-on-surface uppercase tracking-wider text-[11px] mb-xs">Ações Rápidas</h4>
<button class="flex items-center gap-md p-md rounded-xl bg-primary-container text-on-primary-container font-label-bold shadow-lg hover:scale-[1.02] active:scale-[0.98] transition-all text-left">
<span class="material-symbols-outlined p-xs bg-white/20 rounded-lg">notifications_active</span>
<span>Notificar Encarregado</span>
</button>
<button class="flex items-center gap-md p-md rounded-xl bg-surface-container-high text-on-surface border border-outline-variant/20 font-label-bold hover:bg-surface-variant transition-all text-left">
<span class="material-symbols-outlined p-xs bg-primary/10 text-primary rounded-lg">description</span>
<span>Emitir Aviso de Cobrança</span>
</button>
<button class="flex items-center gap-md p-md rounded-xl bg-surface-container-high text-on-surface border border-outline-variant/20 font-label-bold hover:bg-surface-variant transition-all text-left">
<span class="material-symbols-outlined p-xs bg-tertiary/10 text-tertiary rounded-lg">assignment_return</span>
<span>Plano de Parcelamento</span>
</button>
</div>
<!-- Small Chart / Visual Indicator -->
<div class="p-lg rounded-xl bg-surface-container-low neo-float border border-outline-variant/10">
<h4 class="font-label-bold text-on-surface uppercase tracking-wider text-[11px] mb-md">Saúde Financeira</h4>
<div class="flex items-center justify-center py-md">
<div class="relative w-32 h-32 flex items-center justify-center">
<!-- Circular Progress Placeholder -->
<svg class="w-full h-full -rotate-90">
<circle cx="64" cy="64" fill="transparent" r="58" stroke="#2d3449" stroke-width="8"></circle>
<circle cx="64" cy="64" fill="transparent" r="58" stroke="#adc6ff" stroke-dasharray="364.4" stroke-dashoffset="100" stroke-linecap="round" stroke-width="8"></circle>
</svg>
<div class="absolute inset-0 flex flex-col items-center justify-center">
<span class="font-data-numeric text-headline-sm text-on-surface">73%</span>
<span class="text-[9px] text-label-muted uppercase">Recuperado</span>
</div>
</div>
</div>
<p class="text-body-sm text-on-surface-variant text-center mt-xs px-md">
                            A meta de recuperação do trimestre está em <b>73%</b>. Faltam 930.000 Kz para o objetivo.
                        </p>
</div>
<!-- System Status Tooltip-like Card -->
<div class="p-md rounded-xl bg-error/10 border border-error/20 flex items-center gap-md">
<span class="material-symbols-outlined text-error" style="font-variation-settings: 'FILL' 1;">error</span>
<div class="flex-1">
<p class="font-label-bold text-error text-[11px]">ALERTA CRÍTICO</p>
<p class="text-[10px] text-on-surface-variant">5 alunos ultrapassaram 6 meses de atraso. Ação jurídica recomendada.</p>
</div>
</div>
</aside>
</div>
</div>
<!-- Footer (JSON Derived) -->
<footer class="flex justify-between items-center px-xl w-full max-w-[calc(100vw-260px)] py-md mt-auto border-t border-outline-variant/10 bg-transparent text-on-surface-variant font-label-muted text-label-muted">
<div class="flex items-center gap-md">
<p>School Manager v1.0.0 | Todos os direitos reservados</p>
</div>
<div class="flex items-center gap-xl">
<div class="flex items-center gap-xs text-emerald-500 font-label-bold">
<span class="material-symbols-outlined text-[16px]">cloud_done</span>
<span>Backup: OK</span>
</div>
</div>
</footer>
</main>
<!-- Floating Atmosphere Elements -->
<div class="fixed top-0 right-0 w-1/3 h-1/3 bg-primary/5 blur-[120px] pointer-events-none -z-10"></div>
<div class="fixed bottom-0 left-[260px] w-1/4 h-1/4 bg-tertiary/5 blur-[100px] pointer-events-none -z-10"></div>
<script>
        // Micro-interactions for table rows
        document.querySelectorAll('tbody tr').forEach(row => {
            row.addEventListener('click', () => {
                row.classList.toggle('bg-primary/5');
            });
        });

        // Search Bar Glow Effect
        const searchInput = document.querySelector('input[type="text"]');
        searchInput.addEventListener('focus', () => {
            searchInput.parentElement.classList.add('shadow-[0_0_15px_rgba(173,198,255,0.1)]');
        });
        searchInput.addEventListener('blur', () => {
            searchInput.parentElement.classList.remove('shadow-[0_0_15px_rgba(173,198,255,0.1)]');
        });
    </script>
</body></html>

<!DOCTYPE html>

<html class="dark" lang="pt-AO"><head>
<meta charset="utf-8"/>
<meta content="width=device-width, initial-scale=1.0" name="viewport"/>
<title>School Manager - Executive Dashboard</title>
<script src="https://cdn.tailwindcss.com?plugins=forms,container-queries"></script>
<link href="https://fonts.googleapis.com/css2?family=Poppins:wght@400;500;600;700&amp;family=Inter:wght@400;500;600;700&amp;display=swap" rel="stylesheet"/>
<link href="https://fonts.googleapis.com/css2?family=Material+Symbols+Outlined:wght,FILL@100..700,0..1&amp;display=swap" rel="stylesheet"/>
<script id="tailwind-config">
        tailwind.config = {
            darkMode: "class",
            theme: {
                extend: {
                    "colors": {
                        "on-tertiary-container": "#002a51",
                        "on-primary-container": "#00285d",
                        "tertiary-container": "#4c93e7",
                        "on-tertiary-fixed-variant": "#004883",
                        "primary": "#adc6ff",
                        "secondary": "#bcc7de",
                        "inverse-on-surface": "#283044",
                        "surface-tint": "#adc6ff",
                        "surface-container-lowest": "#060e20",
                        "secondary-fixed": "#d8e3fb",
                        "on-error-container": "#ffdad6",
                        "on-primary-fixed-variant": "#004395",
                        "tertiary-fixed": "#d4e3ff",
                        "on-surface": "#dae2fd",
                        "surface-dim": "#0b1326",
                        "on-surface-variant": "#c2c6d6",
                        "surface-variant": "#2d3449",
                        "error-container": "#93000a",
                        "surface-bright": "#31394d",
                        "on-primary-fixed": "#001a42",
                        "on-secondary-fixed-variant": "#3c475a",
                        "surface-container-high": "#222a3d",
                        "surface": "#0b1326",
                        "primary-fixed": "#d8e2ff",
                        "on-tertiary-fixed": "#001c39",
                        "secondary-fixed-dim": "#bcc7de",
                        "tertiary-fixed-dim": "#a4c9ff",
                        "surface-container": "#171f33",
                        "on-tertiary": "#00315d",
                        "on-primary": "#002e6a",
                        "background": "#0b1326",
                        "tertiary": "#a4c9ff",
                        "on-secondary-container": "#aeb9d0",
                        "surface-container-low": "#131b2e",
                        "on-secondary-fixed": "#111c2d",
                        "on-secondary": "#263143",
                        "primary-fixed-dim": "#adc6ff",
                        "surface-container-highest": "#2d3449",
                        "inverse-surface": "#dae2fd",
                        "primary-container": "#4d8eff",
                        "error": "#ffb4ab",
                        "on-background": "#dae2fd",
                        "outline": "#8c909f",
                        "on-error": "#690005",
                        "outline-variant": "#424754",
                        "secondary-container": "#3e495d",
                        "inverse-primary": "#005ac2"
                    },
                    "borderRadius": {
                        "DEFAULT": "0.25rem",
                        "lg": "0.5rem",
                        "xl": "0.75rem",
                        "full": "9999px"
                    },
                    "spacing": {
                        "xs": "8px",
                        "xl": "32px",
                        "md": "16px",
                        "sm": "12px",
                        "gutter": "20px",
                        "sidebar_width": "260px",
                        "lg": "24px",
                        "base": "4px"
                    },
                    "fontFamily": {
                        "display-lg": ["Poppins"],
                        "data-numeric": ["Inter"],
                        "body-md": ["Inter"],
                        "body-sm": ["Inter"],
                        "body-lg": ["Inter"],
                        "headline-sm": ["Poppins"],
                        "label-muted": ["Inter"],
                        "display-md": ["Poppins"],
                        "label-bold": ["Inter"]
                    },
                    "fontSize": {
                        "display-lg": ["32px", {"lineHeight": "1.2", "letterSpacing": "-0.02em", "fontWeight": "600"}],
                        "data-numeric": ["20px", {"lineHeight": "1", "letterSpacing": "-0.01em", "fontWeight": "700"}],
                        "body-md": ["14px", {"lineHeight": "1.5", "fontWeight": "400"}],
                        "body-sm": ["13px", {"lineHeight": "1.5", "fontWeight": "400"}],
                        "body-lg": ["16px", {"lineHeight": "1.5", "fontWeight": "400"}],
                        "headline-sm": ["18px", {"lineHeight": "1.4", "fontWeight": "500"}],
                        "label-muted": ["12px", {"lineHeight": "1", "fontWeight": "400"}],
                        "display-md": ["24px", {"lineHeight": "1.3", "fontWeight": "600"}],
                        "label-bold": ["12px", {"lineHeight": "1", "letterSpacing": "0.05em", "fontWeight": "600"}]
                    }
                }
            }
        }
    </script>
<style>
        .material-symbols-outlined {
            font-variation-settings: 'FILL' 0, 'wght' 400, 'GRAD' 0, 'opsz' 24;
        }
        .neo-card {
            background: linear-gradient(145deg, #171f33, #111b2e);
            box-shadow: inset 1px 1px 0px rgba(255,255,255,0.05), 8px 8px 16px rgba(0,0,0,0.4);
            border: 1px solid rgba(140, 144, 159, 0.1);
        }
        .neo-card-inner {
            background: #0b1326;
            box-shadow: inset 2px 2px 5px rgba(0,0,0,0.5), inset -1px -1px 2px rgba(255,255,255,0.05);
        }
        .active-glow {
            box-shadow: 0 0 15px rgba(77, 142, 255, 0.4);
        }
        .glass-badge {
            background: rgba(255, 255, 255, 0.05);
            backdrop-filter: blur(8px);
            border: 1px solid rgba(255, 255, 255, 0.1);
        }
        .chart-glow {
            filter: drop-shadow(0 0 8px rgba(59, 130, 246, 0.6));
        }
    </style>
</head>
<body class="bg-background text-on-surface font-body-md min-h-screen overflow-x-hidden selection:bg-primary-container selection:text-on-primary-container">
<!-- Fixed Side Navigation -->
<aside class="fixed left-0 top-0 h-full w-[260px] bg-surface-container-low border-r border-outline-variant/10 p-md flex flex-col space-y-lg z-50 shadow-xl">
<div class="flex items-center gap-sm px-xs pt-xs mb-md">
<div class="w-12 h-12 neo-card rounded-xl flex items-center justify-center text-primary active-glow">
<span class="material-symbols-outlined text-[28px]" style="font-variation-settings: 'FILL' 1;">school</span>
</div>
<div>
<h1 class="text-headline-sm font-display-lg text-on-surface tracking-tight leading-none">School Manager</h1>
<p class="text-label-muted font-label-muted opacity-60">Gestão Escolar</p>
</div>
</div>
<nav class="flex-1 space-y-xs overflow-y-auto pr-xs">
<a class="flex items-center gap-md p-md bg-primary-container text-on-primary-container rounded-lg font-label-bold shadow-[0_4px_12px_rgba(77,142,255,0.3)] scale-[0.98] transition-all" href="code.html">
<span class="material-symbols-outlined">dashboard</span>
<span>Dashboard</span>
</a>
<a class="flex items-center gap-md p-md text-on-surface-variant hover:text-on-surface hover:bg-surface-variant/50 transition-all duration-200 rounded-lg" href="../gest_o_de_alunos_school_manager/code.html">
<span class="material-symbols-outlined">group</span>
<span>Alunos</span>
</a>
<a class="flex items-center gap-md p-md text-on-surface-variant hover:text-on-surface hover:bg-surface-variant/50 transition-all duration-200 rounded-lg" href="../financeiro_pagamentos_school_manager/code.html">
<span class="material-symbols-outlined">account_balance_wallet</span>
<span>Financeiro</span>
</a>
<a class="flex items-center gap-md p-md text-on-surface-variant hover:text-on-surface hover:bg-surface-variant/50 transition-all duration-200 rounded-lg" href="../relat_rios_administrativos_school_manager/code.html">
<span class="material-symbols-outlined">analytics</span>
<span>Relatórios</span>
</a>
<a class="flex items-center gap-md p-md text-on-surface-variant hover:text-on-surface hover:bg-surface-variant/50 transition-all duration-200 rounded-lg" href="../escola_classes_school_manager/code.html">
<span class="material-symbols-outlined">corporate_fare</span>
<span>Escola</span>
</a>
<a class="flex items-center gap-md p-md text-on-surface-variant hover:text-on-surface hover:bg-surface-variant/50 transition-all duration-200 rounded-lg" href="../configura_es_do_sistema_school_manager/code.html">
<span class="material-symbols-outlined">settings</span>
<span>Configurações</span>
</a>
</nav>
<div class="pt-md border-t border-outline-variant/10 space-y-sm">
<div class="flex items-center gap-md p-md bg-surface-container-high rounded-xl neo-card">
<img class="w-10 h-10 rounded-full border border-primary/30 object-cover" data-alt="A professional headshot of a middle-aged female administrative officer with a friendly expression. She is wearing formal office attire and is set against a blurred background of a modern office with warm lighting. The image style is polished and high-end, matching a premium corporate dashboard aesthetic." src="https://lh3.googleusercontent.com/aida-public/AB6AXuBbNaIpMon-ez8AYfHgNa-m456ztmtdlP3W9Z7fXzVyJGzWwW6CWb3impjkdGeCeq9h5fvVzgXG5a6E0mPR1rJnbXNLsb8YAxdxcRGLP8HrPtGSwa2FwlGHwgQWgmq1Ubj9hVB37kqSjgJiMycgdsX3iMX2TG6ezgqMs4O8VlpC6xcAfqul8FEKs6DMa6Avc_0NmUsTP723Fur9ObYRZV9tederwLhzU6UMYoT7PnSq1g0urB6Z9aHjikXAZYa8rEHCaxGFrpUg1gau"/>
<div class="flex-1 overflow-hidden">
<p class="font-label-bold text-on-surface truncate">Secretaria</p>
<p class="text-label-muted font-label-muted opacity-60 truncate">Administrador</p>
</div>
<span class="w-2 h-2 rounded-full bg-emerald-500 shadow-[0_0_8px_rgba(16,185,129,0.6)]"></span>
</div>
<button class="w-full flex items-center gap-md p-md text-on-surface-variant hover:text-error transition-all rounded-lg group">
<span class="material-symbols-outlined group-hover:rotate-12 transition-transform">logout</span>
<span>Sair do Sistema</span>
</button>
</div>
</aside>
<!-- Main Content Canvas -->
<main class="ml-[260px] min-h-screen flex flex-col p-xl">
<!-- Top App Bar -->
<header class="flex justify-between items-center mb-xl">
<div>
<h2 class="font-display-md text-display-md text-on-surface">Olá, Secretaria! 👋</h2>
<p class="font-body-sm text-on-surface-variant opacity-70">Bem-vinda de volta ao School Manager</p>
</div>
<div class="flex items-center gap-md">
<div class="neo-card-inner rounded-xl px-md py-sm flex items-center gap-sm">
<span class="material-symbols-outlined text-primary">calendar_today</span>
<div class="text-right">
<p class="text-label-bold text-on-surface leading-none">27 de Maio, 2026</p>
<p class="text-label-muted opacity-60">Quarta-feira, 10:30</p>
</div>
</div>
<button class="w-12 h-12 neo-card rounded-xl flex items-center justify-center relative hover:bg-surface-variant/30 transition-all">
<span class="material-symbols-outlined">notifications</span>
<span class="absolute top-2 right-2 w-5 h-5 bg-error text-[10px] flex items-center justify-center rounded-full text-on-error font-bold border-2 border-background">3</span>
</button>
</div>
</header>
<!-- KPI Cards Grid -->
<section class="grid grid-cols-1 md:grid-cols-2 lg:grid-cols-4 gap-lg mb-xl">
<!-- Alunos Ativos -->
<div class="neo-card p-lg rounded-xl relative overflow-hidden group">
<div class="flex justify-between items-start mb-md">
<div class="p-sm neo-card-inner rounded-lg text-primary">
<span class="material-symbols-outlined text-[28px]" style="font-variation-settings: 'FILL' 1;">group</span>
</div>
<div class="text-emerald-400 flex items-center gap-1 glass-badge px-2 py-0.5 rounded-full text-[10px] font-bold">
<span class="material-symbols-outlined text-[12px]">trending_up</span>
                        +8 este mês
                    </div>
</div>
<h3 class="text-label-muted opacity-60 mb-1">Alunos Ativos</h3>
<p class="text-display-lg font-data-numeric">78</p>
<div class="absolute -right-4 -bottom-4 opacity-5 pointer-events-none group-hover:opacity-10 transition-opacity">
<span class="material-symbols-outlined text-[80px]">groups</span>
</div>
</div>
<!-- Receita do Mês -->
<div class="neo-card p-lg rounded-xl relative overflow-hidden group">
<div class="flex justify-between items-start mb-md">
<div class="p-sm neo-card-inner rounded-lg text-primary">
<span class="material-symbols-outlined text-[28px]" style="font-variation-settings: 'FILL' 1;">payments</span>
</div>
<div class="text-emerald-400 flex items-center gap-1 glass-badge px-2 py-0.5 rounded-full text-[10px] font-bold">
<span class="material-symbols-outlined text-[12px]">trending_up</span>
                        +12.5%
                    </div>
</div>
<h3 class="text-label-muted opacity-60 mb-1">Receita do Mês</h3>
<p class="text-display-lg font-data-numeric">1.200.000 <span class="text-headline-sm font-label-bold opacity-40">Kz</span></p>
<div class="absolute -right-4 -bottom-4 opacity-5 pointer-events-none group-hover:opacity-10 transition-opacity">
<span class="material-symbols-outlined text-[80px]">account_balance_wallet</span>
</div>
</div>
<!-- Em Dívida -->
<div class="neo-card p-lg rounded-xl relative overflow-hidden group">
<div class="flex justify-between items-start mb-md">
<div class="p-sm neo-card-inner rounded-lg text-error">
<span class="material-symbols-outlined text-[28px]" style="font-variation-settings: 'FILL' 1;">credit_card_off</span>
</div>
<div class="text-error flex items-center gap-1 glass-badge px-2 py-0.5 rounded-full text-[10px] font-bold">
<span class="material-symbols-outlined text-[12px]">trending_down</span>
                        -3.2%
                    </div>
</div>
<h3 class="text-label-muted opacity-60 mb-1">Em Dívida</h3>
<p class="text-display-lg font-data-numeric">850.000 <span class="text-headline-sm font-label-bold opacity-40">Kz</span></p>
<div class="absolute -right-4 -bottom-4 opacity-5 pointer-events-none group-hover:opacity-10 transition-opacity">
<span class="material-symbols-outlined text-[80px]">error</span>
</div>
</div>
<!-- Recebido Hoje -->
<div class="neo-card p-lg rounded-xl relative overflow-hidden group">
<div class="flex justify-between items-start mb-md">
<div class="p-sm neo-card-inner rounded-lg text-primary">
<span class="material-symbols-outlined text-[28px]" style="font-variation-settings: 'FILL' 1;">account_balance</span>
</div>
<div class="text-emerald-400 flex items-center gap-1 glass-badge px-2 py-0.5 rounded-full text-[10px] font-bold">
<span class="material-symbols-outlined text-[12px]">arrow_upward</span>
                        +15.8%
                    </div>
</div>
<h3 class="text-label-muted opacity-60 mb-1">Recebido Hoje</h3>
<p class="text-display-lg font-data-numeric">65.000 <span class="text-headline-sm font-label-bold opacity-40">Kz</span></p>
<div class="absolute -right-4 -bottom-4 opacity-5 pointer-events-none group-hover:opacity-10 transition-opacity">
<span class="material-symbols-outlined text-[80px]">savings</span>
</div>
</div>
</section>
<!-- Charts and Tables -->
<section class="grid grid-cols-12 gap-lg mb-xl">
<!-- Line Chart: Receita -->
<div class="col-span-12 lg:col-span-8 neo-card p-lg rounded-xl">
<div class="flex justify-between items-center mb-xl">
<h3 class="font-display-md text-headline-sm text-on-surface">Receita dos Últimos 6 Meses</h3>
<select class="bg-surface-container-high border-none text-on-surface rounded-lg text-body-sm focus:ring-primary">
<option>6 Meses</option>
<option>12 Meses</option>
</select>
</div>
<div class="h-[300px] w-full relative">
<svg class="w-full h-full chart-glow" viewbox="0 0 800 250">
<defs>
<lineargradient id="chartGradient" x1="0%" x2="0%" y1="0%" y2="100%">
<stop offset="0%" style="stop-color:#3B82F6;stop-opacity:0.3"></stop>
<stop offset="100%" style="stop-color:#3B82F6;stop-opacity:0"></stop>
</lineargradient>
</defs>
<path d="M0,180 L160,160 L320,170 L480,120 L640,130 L800,80 L800,250 L0,250 Z" fill="url(#chartGradient)"></path>
<path d="M0,180 L160,160 L320,170 L480,120 L640,130 L800,80" fill="none" stroke="#3B82F6" stroke-linecap="round" stroke-linejoin="round" stroke-width="3"></path>
<circle cx="0" cy="180" fill="#3B82F6" r="4"></circle>
<circle cx="160" cy="160" fill="#3B82F6" r="4"></circle>
<circle cx="320" cy="170" fill="#3B82F6" r="4"></circle>
<circle cx="480" cy="120" fill="#3B82F6" r="4"></circle>
<circle cx="640" cy="130" fill="#3B82F6" r="4"></circle>
<circle cx="800" cy="80" fill="#3B82F6" r="4"></circle>
</svg>
<div class="flex justify-between mt-4 text-label-muted opacity-60">
<span>Dez</span>
<span>Jan</span>
<span>Fev</span>
<span>Mar</span>
<span>Abr</span>
<span>Mai</span>
</div>
</div>
</div>
<!-- Top 5 Devedores -->
<div class="col-span-12 lg:col-span-4 neo-card p-lg rounded-xl">
<div class="flex justify-between items-center mb-md">
<h3 class="font-display-md text-headline-sm text-on-surface">Top 5 Devedores</h3>
<button class="text-primary text-label-bold hover:underline">Ver todos</button>
</div>
<div class="space-y-sm">
<div class="flex items-center gap-sm p-sm neo-card-inner rounded-lg group hover:border-primary/50 transition-all border border-transparent">
<span class="text-label-muted opacity-40 font-bold w-4">1</span>
<img class="w-10 h-10 rounded-full border border-outline-variant" data-alt="Student João Pedro" src="https://lh3.googleusercontent.com/aida-public/AB6AXuAP-zjot9aXDPS9NELkTBFGfLmWUa4AolAsS9ynYbDC5qdeMfkQXzPGetcaVYLUjLFpNURjABwAlFRUokCVD539YCkJ6x9SPQVSxY9pEgachr5mwfCeVDcJAM-g5L3PNCeJ5KQp_3cVaQbB7iwILAt7_lVdc_rnVseIPxO9zIEOfUA3KPPorgPLxZSCFzCPe9carBGqGLVe0LMSxur7EvMwr8ZmUvNZ5Lh1FfvxK0oZ0pO64Ltj4KvuhxK5OJXv5GnybnK-2_wMHg7C"/>
<div class="flex-1">
<p class="text-body-md font-label-bold truncate">João Pedro da Silva</p>
<p class="text-label-muted opacity-60">8ª Classe A</p>
</div>
<div class="text-right">
<p class="text-primary font-data-numeric text-[14px]">120.000</p>
<p class="text-[10px] text-label-muted opacity-40">Kz</p>
</div>
</div>
<div class="flex items-center gap-sm p-sm neo-card-inner rounded-lg group hover:border-primary/50 transition-all border border-transparent">
<span class="text-label-muted opacity-40 font-bold w-4">2</span>
<img class="w-10 h-10 rounded-full border border-outline-variant" data-alt="Student Maria Luísa" src="https://lh3.googleusercontent.com/aida-public/AB6AXuCLBTsWgr1G7MxKjobbTR03OFh3WMxCAvUGsMxlARUAAx4Q-maOrEelfFcYnG3bUk9vx8qJyOu1NPsaQgkgJTH5OhtWapFALFEYPaiLpn6sU_rZj9i-I3In0_TXVUbyHpBAQvysbLvcxlG5ciKa94DOpYAM4Rlh77EVMba_CaSa310-K5xdQX_4MwduX0S5UDmvIPudcPptHN4HEtGVCpfRo04PPLf6azvfNAG_GQFKKDF4STU_tIpNrFYZO-TUdTPDyPS7Ti1F_12F"/>
<div class="flex-1">
<p class="text-body-md font-label-bold truncate">Maria Luísa Alberto</p>
<p class="text-label-muted opacity-60">9ª Classe B</p>
</div>
<div class="text-right">
<p class="text-primary font-data-numeric text-[14px]">95.000</p>
<p class="text-[10px] text-label-muted opacity-40">Kz</p>
</div>
</div>
<div class="flex items-center gap-sm p-sm neo-card-inner rounded-lg group hover:border-primary/50 transition-all border border-transparent">
<span class="text-label-muted opacity-40 font-bold w-4">3</span>
<img class="w-10 h-10 rounded-full border border-outline-variant" data-alt="Student Manuel Francisco" src="https://lh3.googleusercontent.com/aida-public/AB6AXuCqneSzPh5lhiShDHg9Ky3fTi176X1KfsceTAj-M_MHu8DYBgy7qBUST9YBXlG0JwGPgN9ulgDKTsR0fQFWmIu-sxHJQ6wyLykpt8sZSWqmTZjJjuBhmp4w00N3Kd6yAh7DD9Q6Szf-98g0emAsnjNYw_xmZffKINKexlva07Bv_YMF0AKRlox7kVzR0lpdNAotbco7wD9eTK8zbR8-iO66IXxduQG70PZe4yoyZo06uvX6Lrauipre6D-lsylwDbB9baT7jnazojoZ"/>
<div class="flex-1">
<p class="text-body-md font-label-bold truncate">Manuel Francisco</p>
<p class="text-label-muted opacity-60">7ª Classe A</p>
</div>
<div class="text-right">
<p class="text-primary font-data-numeric text-[14px]">80.000</p>
<p class="text-[10px] text-label-muted opacity-40">Kz</p>
</div>
</div>
<div class="flex items-center gap-sm p-sm neo-card-inner rounded-lg group hover:border-primary/50 transition-all border border-transparent">
<span class="text-label-muted opacity-40 font-bold w-4">4</span>
<img class="w-10 h-10 rounded-full border border-outline-variant" data-alt="Student Ana Paula" src="https://lh3.googleusercontent.com/aida-public/AB6AXuDTMgWIeVxDvHXv3BDw1ZiuyI5YEZR8-rtso3SalQi0pvMuEld1XirYwQTjHcby0pdxq7d-5lpWGX0ME1EFoOT-P-rwiAnYqR6yNEu88jpaJvHi-HRQph9qC2dwwpLl8Gx7xQysX3s78fWEcEo72oIVSl868z6aYmbiiVa4Z3TK1WUVABJKn9gKwtiAWVoOLSZulFN-fNM7ASWYuclR7UDVgQ8nDcLuI7niIP2NS21BTa6x7OYsRO17Xqqk24qYtlyaceAgyAQchzek"/>
<div class="flex-1">
<p class="text-body-md font-label-bold truncate">Ana Paula Domingos</p>
<p class="text-label-muted opacity-60">10ª Classe A</p>
</div>
<div class="text-right">
<p class="text-primary font-data-numeric text-[14px]">75.000</p>
<p class="text-[10px] text-label-muted opacity-40">Kz</p>
</div>
</div>
<div class="flex items-center gap-sm p-sm neo-card-inner rounded-lg group hover:border-primary/50 transition-all border border-transparent">
<span class="text-label-muted opacity-40 font-bold w-4">5</span>
<img class="w-10 h-10 rounded-full border border-outline-variant" data-alt="Student Carlos Manuel" src="https://lh3.googleusercontent.com/aida-public/AB6AXuAlNuPhLfSVrwGZL7jXRzGNx-GPDSSOQ3yligkwRvJlYdtCGwillCxHi7owcpCErFFSMgG43WWNikcmvGwVjBiNkrn2LGUh3lOjbUPA-tnHegKQt_2dnNv9XpSolArSaotDS2KWjVmdK8cp57qIx5yUX38H4w7nJduNSIZcRafnC7thyzMhRXYkBZ4WvMYS7yFoUAEr7MTBziOXinCQ8U1NdxZc3FBE7mfBq1cTvgYBYO8wBqH2FsRh1GNa-pHUhruHhEYzlKbWm5Hw"/>
<div class="flex-1">
<p class="text-body-md font-label-bold truncate">Carlos Manuel</p>
<p class="text-label-muted opacity-60">6ª Classe B</p>
</div>
<div class="text-right">
<p class="text-primary font-data-numeric text-[14px]">65.000</p>
<p class="text-[10px] text-label-muted opacity-40">Kz</p>
</div>
</div>
</div>
</div>
</section>
<!-- Bottom Grid: Quick Actions & Daily Summary -->
<section class="grid grid-cols-12 gap-lg mt-auto">
<!-- Atalhos Rápidos -->
<div class="col-span-12 lg:col-span-7 neo-card p-lg rounded-xl">
<h3 class="font-display-md text-headline-sm text-on-surface mb-lg">Atalhos Rápidos</h3>
<div class="grid grid-cols-5 gap-md">
<button class="flex flex-col items-center gap-sm p-md neo-card-inner rounded-xl group hover:bg-primary-container/20 transition-all duration-300 hover:scale-105">
<div class="w-12 h-12 bg-primary-container/10 rounded-full flex items-center justify-center text-primary group-hover:scale-110 transition-transform">
<span class="material-symbols-outlined text-[28px]">person_add</span>
</div>
<span class="text-label-muted text-center leading-tight">Novo Aluno</span>
</button>
<button class="flex flex-col items-center gap-sm p-md neo-card-inner rounded-xl group hover:bg-primary-container/20 transition-all duration-300 hover:scale-105">
<div class="w-12 h-12 bg-primary-container/10 rounded-full flex items-center justify-center text-primary group-hover:scale-110 transition-transform">
<span class="material-symbols-outlined text-[28px]">point_of_sale</span>
</div>
<span class="text-label-muted text-center leading-tight">Novo Pagamento</span>
</button>
<button class="flex flex-col items-center gap-sm p-md neo-card-inner rounded-xl group hover:bg-primary-container/20 transition-all duration-300 hover:scale-105">
<div class="w-12 h-12 bg-primary-container/10 rounded-full flex items-center justify-center text-primary group-hover:scale-110 transition-transform">
<span class="material-symbols-outlined text-[28px]">description</span>
</div>
<span class="text-label-muted text-center leading-tight">Emitir Recibo</span>
</button>
<button class="flex flex-col items-center gap-sm p-md neo-card-inner rounded-xl group hover:bg-primary-container/20 transition-all duration-300 hover:scale-105">
<div class="w-12 h-12 bg-primary-container/10 rounded-full flex items-center justify-center text-primary group-hover:scale-110 transition-transform">
<span class="material-symbols-outlined text-[28px]">input</span>
</div>
<span class="text-label-muted text-center leading-tight">Nova Entrada</span>
</button>
<button class="flex flex-col items-center gap-sm p-md neo-card-inner rounded-xl group hover:bg-primary-container/20 transition-all duration-300 hover:scale-105">
<div class="w-12 h-12 bg-primary-container/10 rounded-full flex items-center justify-center text-primary group-hover:scale-110 transition-transform">
<span class="material-symbols-outlined text-[28px]">assessment</span>
</div>
<span class="text-label-muted text-center leading-tight">Relatório Diário</span>
</button>
</div>
</div>
<!-- Resumo Financeiro do Dia -->
<div class="col-span-12 lg:col-span-5 neo-card p-lg rounded-xl border border-primary/20 bg-primary/5">
<div class="flex justify-between items-center mb-lg">
<h3 class="font-display-md text-headline-sm text-on-surface">Resumo Financeiro do Dia</h3>
<button class="bg-primary-container text-on-primary-container px-md py-sm rounded-lg font-label-bold text-label-bold hover:brightness-110 active:scale-95 transition-all shadow-lg">Fechar Dia</button>
</div>
<div class="grid grid-cols-2 gap-md mb-md">
<div class="neo-card-inner p-md rounded-xl flex items-center gap-sm">
<div class="w-10 h-10 bg-emerald-500/10 text-emerald-400 rounded-lg flex items-center justify-center">
<span class="material-symbols-outlined">trending_up</span>
</div>
<div>
<p class="text-label-muted text-[10px] uppercase tracking-wider opacity-60">Entradas</p>
<p class="text-body-lg font-data-numeric">65.000 <span class="text-xs opacity-40">Kz</span></p>
</div>
</div>
<div class="neo-card-inner p-md rounded-xl flex items-center gap-sm">
<div class="w-10 h-10 bg-error/10 text-error rounded-lg flex items-center justify-center">
<span class="material-symbols-outlined">trending_down</span>
</div>
<div>
<p class="text-label-muted text-[10px] uppercase tracking-wider opacity-60">Saídas</p>
<p class="text-body-lg font-data-numeric">12.500 <span class="text-xs opacity-40">Kz</span></p>
</div>
</div>
</div>
<div class="neo-card p-md rounded-xl bg-primary/10 border border-primary/30 flex items-center justify-between">
<div class="flex items-center gap-sm">
<div class="w-12 h-12 bg-primary rounded-xl flex items-center justify-center text-on-primary shadow-xl">
<span class="material-symbols-outlined text-[32px]">account_balance_wallet</span>
</div>
<div>
<p class="text-label-muted text-[10px] uppercase tracking-wider opacity-80">Saldo do Dia</p>
<p class="text-display-md font-data-numeric text-primary">52.500 <span class="text-headline-sm opacity-60">Kz</span></p>
</div>
</div>
<span class="material-symbols-outlined text-primary/40 text-[48px]">verified</span>
</div>
</div>
</section>
<!-- Footer -->
<footer class="mt-xl py-md border-t border-outline-variant/10 flex justify-between items-center text-label-muted opacity-50">
<p>School Manager v1.0.0 | Todos os direitos reservados</p>
<div class="flex items-center gap-md">
<span class="flex items-center gap-xs"><span class="w-2 h-2 rounded-full bg-emerald-500"></span> Backup: OK</span>
<div class="flex gap-sm">
<a class="hover:text-on-surface transition-colors" href="#">Termos</a>
<a class="hover:text-on-surface transition-colors" href="#">Suporte</a>
</div>
</div>
</footer>
</main>
<script>
        document.querySelectorAll('button, a').forEach(el => {
            el.addEventListener('mousedown', () => {
                el.classList.add('scale-95');
            });
            el.addEventListener('mouseup', () => {
                el.classList.remove('scale-95');
            });
            el.addEventListener('mouseleave', () => {
                el.classList.remove('scale-95');
            });
        });

        setInterval(() => {
            const now = new Date();
            const hours = String(now.getHours()).padStart(2, '0');
            const minutes = String(now.getMinutes()).padStart(2, '0');
            const timeEl = document.querySelector('header .text-label-muted');
            if (timeEl) {
                const day = timeEl.innerText.split(',')[0];
                timeEl.innerText = `${day}, ${hours}:${minutes}`;
            }
        }, 10000);
    </script>
</body></html>


<!DOCTYPE html>

<html class="dark" lang="pt-PT"><head>
<meta charset="utf-8"/>
<meta content="width=device-width, initial-scale=1.0" name="viewport"/>
<title>Gestão Escolar - Classes</title>
<script src="https://cdn.tailwindcss.com?plugins=forms,container-queries"></script>
<link href="https://fonts.googleapis.com/css2?family=Inter:wght@400;500;600;700&amp;family=Poppins:wght@500;600;700;800&amp;display=swap" rel="stylesheet"/>
<link href="https://fonts.googleapis.com/css2?family=Material+Symbols+Outlined:wght,FILL@100..700,0..1&amp;display=swap" rel="stylesheet"/>
<link href="https://fonts.googleapis.com/css2?family=Material+Symbols+Outlined:wght,FILL@100..700,0..1&amp;display=swap" rel="stylesheet"/>
<script id="tailwind-config">
      tailwind.config = {
        darkMode: "class",
        theme: {
          extend: {
            "colors": {
                    "on-secondary": "#263143",
                    "on-tertiary-fixed": "#001c39",
                    "error": "#ffb4ab",
                    "surface-container": "#171f33",
                    "error-container": "#93000a",
                    "on-surface-variant": "#c2c6d6",
                    "surface-bright": "#31394d",
                    "surface-tint": "#adc6ff",
                    "on-tertiary-container": "#002a51",
                    "on-primary-fixed-variant": "#004395",
                    "tertiary-fixed": "#d4e3ff",
                    "surface-dim": "#0b1326",
                    "outline": "#8c909f",
                    "secondary": "#bcc7de",
                    "on-tertiary": "#00315d",
                    "surface-container-low": "#131b2e",
                    "outline-variant": "#424754",
                    "secondary-fixed-dim": "#bcc7de",
                    "on-primary-container": "#00285d",
                    "on-background": "#dae2fd",
                    "surface-container-lowest": "#060e20",
                    "surface": "#0b1326",
                    "tertiary-fixed-dim": "#a4c9ff",
                    "on-primary": "#002e6a",
                    "primary-container": "#4d8eff",
                    "inverse-surface": "#dae2fd",
                    "surface-container-high": "#222a3d",
                    "on-error-container": "#ffdad6",
                    "on-secondary-fixed-variant": "#3c475a",
                    "background": "#0b1326",
                    "on-tertiary-fixed-variant": "#004883",
                    "on-error": "#690005",
                    "secondary-fixed": "#d8e3fb",
                    "primary-fixed": "#d8e2ff",
                    "on-primary-fixed": "#001a42",
                    "tertiary-container": "#4c93e7",
                    "primary": "#adc6ff",
                    "tertiary": "#a4c9ff",
                    "surface-container-highest": "#2d3449",
                    "surface-variant": "#2d3449",
                    "on-surface": "#dae2fd",
                    "secondary-container": "#3e495d",
                    "on-secondary-container": "#aeb9d0",
                    "inverse-primary": "#005ac2",
                    "primary-fixed-dim": "#adc6ff",
                    "on-secondary-fixed": "#111c2d",
                    "inverse-on-surface": "#283044"
            },
            "borderRadius": {
                    "DEFAULT": "0.25rem",
                    "lg": "0.5rem",
                    "xl": "0.75rem",
                    "full": "9999px"
            },
            "spacing": {
                    "sidebar_width": "260px",
                    "lg": "24px",
                    "base": "4px",
                    "sm": "12px",
                    "xl": "32px",
                    "md": "16px",
                    "xs": "8px",
                    "gutter": "20px"
            },
            "fontFamily": {
                    "body-lg": ["Inter"],
                    "label-muted": ["Inter"],
                    "display-lg": ["Poppins"],
                    "label-bold": ["Inter"],
                    "data-numeric": ["Inter"],
                    "headline-sm": ["Poppins"],
                    "body-md": ["Inter"],
                    "body-sm": ["Inter"],
                    "display-md": ["Poppins"]
            },
            "fontSize": {
                    "body-lg": ["16px", {"lineHeight": "1.5", "fontWeight": "400"}],
                    "label-muted": ["12px", {"lineHeight": "1", "fontWeight": "400"}],
                    "display-lg": ["32px", {"lineHeight": "1.2", "letterSpacing": "-0.02em", "fontWeight": "600"}],
                    "label-bold": ["12px", {"lineHeight": "1", "letterSpacing": "0.05em", "fontWeight": "600"}],
                    "data-numeric": ["20px", {"lineHeight": "1", "letterSpacing": "-0.01em", "fontWeight": "700"}],
                    "headline-sm": ["18px", {"lineHeight": "1.4", "fontWeight": "500"}],
                    "body-md": ["14px", {"lineHeight": "1.5", "fontWeight": "400"}],
                    "body-sm": ["13px", {"lineHeight": "1.5", "fontWeight": "400"}],
                    "display-md": ["24px", {"lineHeight": "1.3", "fontWeight": "600"}]
            }
          },
        },
      }
    </script>
<style>
        .neo-pressed {
            box-shadow: inset 4px 4px 8px rgba(0, 0, 0, 0.4), inset -2px -2px 6px rgba(255, 255, 255, 0.05);
        }
        .neo-flat {
            box-shadow: 8px 8px 16px rgba(0, 0, 0, 0.4), -4px -4px 12px rgba(255, 255, 255, 0.03);
            border: 1px solid rgba(255, 255, 255, 0.05);
        }
        .glass-edge {
            border-top: 1px solid rgba(255, 255, 255, 0.1);
            border-left: 1px solid rgba(255, 255, 255, 0.1);
        }
        .scroll-hide::-webkit-scrollbar { display: none; }
    </style>
</head>
<body class="bg-background text-on-background font-body-md selection:bg-primary selection:text-on-primary overflow-hidden">
<!-- Desktop Sidebar -->
<aside class="fixed left-0 top-0 h-full w-[260px] bg-surface-container-low flex flex-col py-lg z-50 shadow-[20px_0_40px_rgba(0,0,0,0.4)]">
<div class="px-xl mb-xl">
<h1 class="font-display-md text-display-md font-bold text-primary">Gestão Escolar</h1>
<p class="text-on-surface-variant font-label-muted text-label-muted mt-xs uppercase tracking-widest">Administração Central</p>
</div>
<nav class="flex-1 flex flex-col gap-xs pr-md">
<a class="flex items-center gap-md py-md px-xl text-on-surface-variant hover:text-on-surface transition-all duration-200 group" href="../dashboard_school_manager_refatorado/code.html">
<span class="material-symbols-outlined group-hover:scale-110 transition-transform">dashboard</span>
<span class="font-body-md">Dashboard</span>
</a>
<a class="flex items-center gap-md py-md px-xl text-on-surface-variant hover:text-on-surface transition-all duration-200 group" href="../gest_o_de_alunos_school_manager/code.html">
<span class="material-symbols-outlined group-hover:scale-110 transition-transform">group</span>
<span class="font-body-md">Alunos</span>
</a>
<a class="flex items-center gap-md py-md px-xl text-on-surface-variant hover:text-on-surface transition-all duration-200 group" href="../financeiro_pagamentos_school_manager/code.html">
<span class="material-symbols-outlined group-hover:scale-110 transition-transform">account_balance_wallet</span>
<span class="font-body-md">Financeiro</span>
</a>
<a class="flex items-center gap-md py-md px-xl text-on-surface-variant hover:text-on-surface transition-all duration-200 group" href="../relat_rios_administrativos_school_manager/code.html">
<span class="material-symbols-outlined group-hover:scale-110 transition-transform">analytics</span>
<span class="font-body-md">Relatórios</span>
</a>
<a class="flex items-center gap-md py-md px-xl bg-primary text-on-primary rounded-r-full shadow-[0_4px_12px_rgba(59,130,246,0.3)] group active:scale-95 duration-200" href="code.html">
<span class="material-symbols-outlined" style="font-variation-settings: 'FILL' 1;">school</span>
<span class="font-body-md font-semibold">Escola</span>
</a>
<a class="flex items-center gap-md py-md px-xl text-on-surface-variant hover:text-on-surface transition-all duration-200 group" href="../configura_es_do_sistema_school_manager/code.html">
<span class="material-symbols-outlined group-hover:scale-110 transition-transform">settings</span>
<span class="font-body-md">Configurações</span>
</a>
</nav>
<div class="mt-auto px-xl pt-lg">
<div class="p-md bg-surface-container-high rounded-xl neo-flat flex items-center gap-md">
<div class="w-10 h-10 rounded-full bg-primary-container flex items-center justify-center text-on-primary-container font-bold">
                    AD
                </div>
<div>
<p class="font-label-bold text-label-bold text-on-surface">Admin</p>
<p class="text-[10px] text-on-surface-variant/70">Sessão Ativa</p>
</div>
</div>
</div>
</aside>
<!-- Main Canvas -->
<main class="ml-[260px] min-h-screen relative bg-surface-dim">
<!-- TopAppBar -->
<header class="fixed top-0 right-0 w-[calc(100%-260px)] h-16 bg-surface flex justify-between items-center px-xl z-40 border-b border-outline-variant/20 shadow-sm">
<div class="flex items-center gap-md">
<span class="font-headline-sm text-headline-sm font-semibold text-on-surface">Módulo Escola</span>
<span class="text-outline-variant">/</span>
<span class="text-primary font-bold font-headline-sm text-headline-sm">Classes</span>
</div>
<div class="flex items-center gap-lg">
<div class="relative group">
<input class="bg-surface-container-lowest border-outline-variant/30 rounded-full pl-xl pr-md py-xs text-body-sm w-64 focus:ring-2 focus:ring-primary focus:border-transparent transition-all neo-pressed" placeholder="Pesquisar classe..." type="text"/>
<span class="material-symbols-outlined absolute left-3 top-1/2 -translate-y-1/2 text-on-surface-variant/50">search</span>
</div>
<div class="flex items-center gap-xs">
<button class="p-base hover:bg-surface-container-highest rounded-full transition-all active:scale-90">
<span class="material-symbols-outlined text-on-surface-variant">notifications</span>
</button>
<button class="p-base hover:bg-surface-container-highest rounded-full transition-all active:scale-90">
<span class="material-symbols-outlined text-on-surface-variant">account_circle</span>
</button>
</div>
</div>
</header>
<!-- Content Area -->
<div class="pt-24 px-xl pb-12 overflow-y-auto h-screen scroll-hide">
<!-- Page Header Actions -->
<div class="flex justify-between items-end mb-xl">
<div>
<h2 class="font-display-lg text-display-lg text-on-surface">Estrutura Acadêmica</h2>
<p class="text-on-surface-variant font-body-md mt-xs">Gerencie as turmas e o fluxo de ocupação institucional.</p>
</div>
<button class="flex items-center gap-xs bg-gradient-to-br from-primary-container to-primary text-on-primary px-lg py-md rounded-xl font-semibold shadow-[0_4px_12px_rgba(59,130,246,0.3)] hover:brightness-110 active:scale-95 transition-all">
<span class="material-symbols-outlined">add_circle</span>
<span>Nova Classe</span>
</button>
</div>
<!-- Dashboard Analytics for Classes (Bento Style Small) -->
<div class="grid grid-cols-12 gap-lg mb-xl">
<div class="col-span-4 bg-surface-container rounded-xl p-lg neo-flat glass-edge relative overflow-hidden group">
<div class="absolute -right-4 -bottom-4 opacity-5 group-hover:scale-125 transition-transform duration-700">
<span class="material-symbols-outlined text-9xl">school</span>
</div>
<p class="text-on-surface-variant font-label-bold uppercase tracking-wider">Total de Alunos</p>
<div class="flex items-end justify-between mt-sm">
<span class="font-data-numeric text-[32px] text-primary">1,452</span>
<div class="bg-primary/10 text-primary text-[11px] px-sm py-base rounded-full font-bold">
                            +12% vs 2023
                        </div>
</div>
</div>
<div class="col-span-4 bg-surface-container rounded-xl p-lg neo-flat glass-edge relative overflow-hidden group">
<div class="absolute -right-4 -bottom-4 opacity-5 group-hover:scale-125 transition-transform duration-700">
<span class="material-symbols-outlined text-9xl">airline_seat_recline_normal</span>
</div>
<p class="text-on-surface-variant font-label-bold uppercase tracking-wider">Capacidade Total</p>
<div class="flex items-end justify-between mt-sm">
<span class="font-data-numeric text-[32px] text-tertiary">1,600</span>
<div class="flex flex-col items-end">
<span class="text-body-sm font-bold text-on-surface">90.7% Ocupado</span>
</div>
</div>
</div>
<div class="col-span-4 bg-surface-container rounded-xl p-lg neo-flat glass-edge relative overflow-hidden group">
<div class="absolute -right-4 -bottom-4 opacity-5 group-hover:scale-125 transition-transform duration-700">
<span class="material-symbols-outlined text-9xl">pending_actions</span>
</div>
<p class="text-on-surface-variant font-label-bold uppercase tracking-wider">Vagas Disponíveis</p>
<div class="flex items-end justify-between mt-sm">
<span class="font-data-numeric text-[32px] text-error">148</span>
<span class="text-label-muted text-on-surface-variant/60">Em 14 classes</span>
</div>
</div>
</div>
<!-- Classes Grid -->
<div class="grid grid-cols-12 gap-lg">
<!-- Class Card 1 -->
<div class="col-span-3 bg-surface-container rounded-xl p-lg neo-flat glass-edge hover:-translate-y-1 transition-transform duration-300">
<div class="flex justify-between items-start mb-md">
<div class="p-sm bg-primary-container/20 rounded-lg">
<span class="material-symbols-outlined text-primary" style="font-variation-settings: 'FILL' 1;">groups</span>
</div>
<button class="text-on-surface-variant hover:text-primary transition-colors">
<span class="material-symbols-outlined">more_vert</span>
</button>
</div>
<h3 class="font-display-md text-display-md text-on-surface">10ª Classe</h3>
<p class="text-on-surface-variant font-body-sm mb-lg">Ensino Médio Regular</p>
<div class="space-y-md">
<div class="flex justify-between text-body-sm">
<span class="text-on-surface-variant">Matriculados:</span>
<span class="font-bold text-on-surface">240</span>
</div>
<div class="flex justify-between text-body-sm">
<span class="text-on-surface-variant">Capacidade:</span>
<span class="font-bold text-on-surface">250</span>
</div>
<div class="w-full h-2 bg-surface-container-lowest rounded-full overflow-hidden neo-pressed">
<div class="h-full bg-primary" style="width: 96%"></div>
</div>
<div class="flex justify-between items-center pt-md border-t border-outline-variant/10">
<span class="text-[10px] uppercase font-bold text-primary/80">96% Ocupado</span>
<button class="text-primary font-bold text-body-sm hover:underline">Ver Turmas</button>
</div>
</div>
</div>
<!-- Class Card 2 -->
<div class="col-span-3 bg-surface-container rounded-xl p-lg neo-flat glass-edge hover:-translate-y-1 transition-transform duration-300">
<div class="flex justify-between items-start mb-md">
<div class="p-sm bg-primary-container/20 rounded-lg">
<span class="material-symbols-outlined text-primary" style="font-variation-settings: 'FILL' 1;">groups</span>
</div>
<button class="text-on-surface-variant hover:text-primary transition-colors">
<span class="material-symbols-outlined">more_vert</span>
</button>
</div>
<h3 class="font-display-md text-display-md text-on-surface">11ª Classe</h3>
<p class="text-on-surface-variant font-body-sm mb-lg">Ensino Médio Regular</p>
<div class="space-y-md">
<div class="flex justify-between text-body-sm">
<span class="text-on-surface-variant">Matriculados:</span>
<span class="font-bold text-on-surface">215</span>
</div>
<div class="flex justify-between text-body-sm">
<span class="text-on-surface-variant">Capacidade:</span>
<span class="font-bold text-on-surface">250</span>
</div>
<div class="w-full h-2 bg-surface-container-lowest rounded-full overflow-hidden neo-pressed">
<div class="h-full bg-primary" style="width: 86%"></div>
</div>
<div class="flex justify-between items-center pt-md border-t border-outline-variant/10">
<span class="text-[10px] uppercase font-bold text-primary/80">86% Ocupado</span>
<button class="text-primary font-bold text-body-sm hover:underline">Ver Turmas</button>
</div>
</div>
</div>
<!-- Class Card 3 -->
<div class="col-span-3 bg-surface-container rounded-xl p-lg neo-flat glass-edge hover:-translate-y-1 transition-transform duration-300 border-2 border-primary/20">
<div class="flex justify-between items-start mb-md">
<div class="p-sm bg-primary-container/20 rounded-lg">
<span class="material-symbols-outlined text-primary" style="font-variation-settings: 'FILL' 1;">workspace_premium</span>
</div>
<button class="text-on-surface-variant hover:text-primary transition-colors">
<span class="material-symbols-outlined">more_vert</span>
</button>
</div>
<h3 class="font-display-md text-display-md text-on-surface">12ª Classe</h3>
<p class="text-on-surface-variant font-body-sm mb-lg">Finalistas / Exames</p>
<div class="space-y-md">
<div class="flex justify-between text-body-sm">
<span class="text-on-surface-variant">Matriculados:</span>
<span class="font-bold text-on-surface">288</span>
</div>
<div class="flex justify-between text-body-sm">
<span class="text-on-surface-variant">Capacidade:</span>
<span class="font-bold text-on-surface">300</span>
</div>
<div class="w-full h-2 bg-surface-container-lowest rounded-full overflow-hidden neo-pressed">
<div class="h-full bg-primary" style="width: 96%"></div>
</div>
<div class="flex justify-between items-center pt-md border-t border-outline-variant/10">
<span class="text-[10px] uppercase font-bold text-primary/80">96% Ocupado</span>
<button class="text-primary font-bold text-body-sm hover:underline">Ver Turmas</button>
</div>
</div>
</div>
<!-- Class Card 4 -->
<div class="col-span-3 bg-surface-container rounded-xl p-lg neo-flat glass-edge hover:-translate-y-1 transition-transform duration-300">
<div class="flex justify-between items-start mb-md">
<div class="p-sm bg-primary-container/20 rounded-lg">
<span class="material-symbols-outlined text-primary" style="font-variation-settings: 'FILL' 1;">history_edu</span>
</div>
<button class="text-on-surface-variant hover:text-primary transition-colors">
<span class="material-symbols-outlined">more_vert</span>
</button>
</div>
<h3 class="font-display-md text-display-md text-on-surface">7ª Classe</h3>
<p class="text-on-surface-variant font-body-sm mb-lg">Ensino Primário</p>
<div class="space-y-md">
<div class="flex justify-between text-body-sm">
<span class="text-on-surface-variant">Matriculados:</span>
<span class="font-bold text-on-surface">120</span>
</div>
<div class="flex justify-between text-body-sm">
<span class="text-on-surface-variant">Capacidade:</span>
<span class="font-bold text-on-surface">200</span>
</div>
<div class="w-full h-2 bg-surface-container-lowest rounded-full overflow-hidden neo-pressed">
<div class="h-full bg-primary" style="width: 60%"></div>
</div>
<div class="flex justify-between items-center pt-md border-t border-outline-variant/10">
<span class="text-[10px] uppercase font-bold text-error">60% Ocupado</span>
<button class="text-primary font-bold text-body-sm hover:underline">Ver Turmas</button>
</div>
</div>
</div>
<!-- Empty State / Add New Placeholder -->
<div class="col-span-3 border-2 border-dashed border-outline-variant/30 rounded-xl p-lg flex flex-col items-center justify-center gap-md hover:bg-surface-container-high/50 transition-all cursor-pointer group">
<div class="w-12 h-12 rounded-full bg-surface-container-high flex items-center justify-center text-outline-variant group-hover:bg-primary/20 group-hover:text-primary transition-all">
<span class="material-symbols-outlined text-3xl">add</span>
</div>
<p class="font-label-bold text-outline-variant group-hover:text-primary">Configurar Nova Classe</p>
</div>
</div>
<!-- Footer Meta -->
<footer class="mt-xl flex justify-between items-center opacity-60">
<div class="flex gap-lg">
<div class="flex items-center gap-xs">
<div class="w-2 h-2 rounded-full bg-green-400"></div>
<span class="text-label-muted">Sistema Online</span>
</div>
<span class="text-label-muted">v2.4.1 | Status do Backup: Sincronizado</span>
</div>
<div class="flex gap-lg">
<a class="text-label-muted hover:text-primary transition-colors" href="#">Suporte</a>
<a class="text-label-muted hover:text-primary transition-colors" href="#">Documentação</a>
</div>
</footer>
</div>
</main>
<!-- Contextual FAB (Suppressed on this focused view as per rules, but shown here as a ghost interaction or micro-action if needed) -->
<!-- Not rendered based on suppression rules for "Details/focused" screens, but "Home/Dashboard" is allowed. Since this is a core module "Escola", we stick to the top action button. -->
<script>
        // Micro-interactions
        document.querySelectorAll('.neo-flat').forEach(card => {
            card.addEventListener('mousedown', () => {
                card.style.transform = 'scale(0.98)';
                card.classList.add('neo-pressed');
                card.classList.remove('neo-flat');
            });
            card.addEventListener('mouseup', () => {
                card.style.transform = 'scale(1.01)';
                card.classList.remove('neo-pressed');
                card.classList.add('neo-flat');
            });
            card.addEventListener('mouseleave', () => {
                card.style.transform = 'scale(1)';
            });
        });
    </script>
</body></html>



<!DOCTYPE html>

<html class="dark" lang="pt-PT"><head>
<meta charset="utf-8"/>
<meta content="width=device-width, initial-scale=1.0" name="viewport"/>
<script src="https://cdn.tailwindcss.com?plugins=forms,container-queries"></script>
<link href="https://fonts.googleapis.com/css2?family=Inter:wght@400;500;600;700&amp;family=Poppins:wght@500;600;700&amp;display=swap" rel="stylesheet"/>
<link href="https://fonts.googleapis.com/css2?family=Material+Symbols+Outlined:wght,FILL@100..700,0..1&amp;display=swap" rel="stylesheet"/>
<link href="https://fonts.googleapis.com/css2?family=Material+Symbols+Outlined:wght,FILL@100..700,0..1&amp;display=swap" rel="stylesheet"/>
<script id="tailwind-config">
      tailwind.config = {
        darkMode: "class",
        theme: {
          extend: {
            "colors": {
                    "on-secondary": "#263143",
                    "on-tertiary-fixed": "#001c39",
                    "error": "#ffb4ab",
                    "surface-container": "#171f33",
                    "error-container": "#93000a",
                    "on-surface-variant": "#c2c6d6",
                    "surface-bright": "#31394d",
                    "surface-tint": "#adc6ff",
                    "on-tertiary-container": "#002a51",
                    "on-primary-fixed-variant": "#004395",
                    "tertiary-fixed": "#d4e3ff",
                    "surface-dim": "#0b1326",
                    "outline": "#8c909f",
                    "secondary": "#bcc7de",
                    "on-tertiary": "#00315d",
                    "surface-container-low": "#131b2e",
                    "outline-variant": "#424754",
                    "secondary-fixed-dim": "#bcc7de",
                    "on-primary-container": "#00285d",
                    "on-background": "#dae2fd",
                    "surface-container-lowest": "#060e20",
                    "surface": "#0b1326",
                    "tertiary-fixed-dim": "#a4c9ff",
                    "on-primary": "#002e6a",
                    "primary-container": "#4d8eff",
                    "inverse-surface": "#dae2fd",
                    "surface-container-high": "#222a3d",
                    "on-error-container": "#ffdad6",
                    "on-secondary-fixed-variant": "#3c475a",
                    "background": "#0b1326",
                    "on-tertiary-fixed-variant": "#004883",
                    "on-error": "#690005",
                    "secondary-fixed": "#d8e3fb",
                    "primary-fixed": "#d8e2ff",
                    "on-primary-fixed": "#001a42",
                    "tertiary-container": "#4c93e7",
                    "primary": "#adc6ff",
                    "tertiary": "#a4c9ff",
                    "surface-container-highest": "#2d3449",
                    "surface-variant": "#2d3449",
                    "on-surface": "#dae2fd",
                    "secondary-container": "#3e495d",
                    "on-secondary-container": "#aeb9d0",
                    "inverse-primary": "#005ac2",
                    "primary-fixed-dim": "#adc6ff",
                    "on-secondary-fixed": "#111c2d",
                    "inverse-on-surface": "#283044"
            },
            "borderRadius": {
                    "DEFAULT": "0.25rem",
                    "lg": "0.5rem",
                    "xl": "0.75rem",
                    "full": "9999px"
            },
            "spacing": {
                    "sidebar_width": "260px",
                    "lg": "24px",
                    "base": "4px",
                    "sm": "12px",
                    "xl": "32px",
                    "md": "16px",
                    "xs": "8px",
                    "gutter": "20px"
            },
            "fontFamily": {
                    "body-lg": ["Inter"],
                    "label-muted": ["Inter"],
                    "display-lg": ["Poppins"],
                    "label-bold": ["Inter"],
                    "data-numeric": ["Inter"],
                    "headline-sm": ["Poppins"],
                    "body-md": ["Inter"],
                    "body-sm": ["Inter"],
                    "display-md": ["Poppins"]
            },
            "fontSize": {
                    "body-lg": ["16px", {"lineHeight": "1.5", "fontWeight": "400"}],
                    "label-muted": ["12px", {"lineHeight": "1", "fontWeight": "400"}],
                    "display-lg": ["32px", {"lineHeight": "1.2", "letterSpacing": "-0.02em", "fontWeight": "600"}],
                    "label-bold": ["12px", {"lineHeight": "1", "letterSpacing": "0.05em", "fontWeight": "600"}],
                    "data-numeric": ["20px", {"lineHeight": "1", "letterSpacing": "-0.01em", "fontWeight": "700"}],
                    "headline-sm": ["18px", {"lineHeight": "1.4", "fontWeight": "500"}],
                    "body-md": ["14px", {"lineHeight": "1.5", "fontWeight": "400"}],
                    "body-sm": ["13px", {"lineHeight": "1.5", "fontWeight": "400"}],
                    "display-md": ["24px", {"lineHeight": "1.3", "fontWeight": "600"}]
            }
          },
        },
      }
    </script>
<style>
      .neo-depth-inner {
        box-shadow: inset 1px 1px 1px rgba(255, 255, 255, 0.05), inset -1px -1px 1px rgba(0, 0, 0, 0.2);
      }
      .neo-depth-outer {
        box-shadow: 0 4px 12px rgba(0, 0, 0, 0.4);
      }
      .neo-pressed {
        box-shadow: inset 2px 2px 5px rgba(0, 0, 0, 0.5), inset -1px -1px 2px rgba(255, 255, 255, 0.05);
      }
      .glass-accents {
        backdrop-filter: blur(12px);
        background: rgba(23, 31, 51, 0.7);
        border: 1px solid rgba(140, 144, 159, 0.1);
      }
      .material-symbols-outlined {
        font-variation-settings: 'FILL' 0, 'wght' 400, 'GRAD' 0, 'opsz' 24;
      }
      ::-webkit-scrollbar {
        width: 8px;
      }
      ::-webkit-scrollbar-track {
        background: #0b1326;
      }
      ::-webkit-scrollbar-thumb {
        background: #2d3449;
        border-radius: 4px;
      }
      ::-webkit-scrollbar-thumb:hover {
        background: #3e495d;
      }
    </style>
</head>
<body class="bg-background text-on-background font-body-md min-h-screen">
<!-- Desktop Side Navigation Shell -->
<aside class="fixed left-0 top-0 h-full w-[260px] bg-surface-container-low flex flex-col py-lg shadow-[20px_0_40px_rgba(0,0,0,0.4)] z-50">
<div class="px-lg mb-xl">
<div class="flex items-center gap-xs">
<span class="material-symbols-outlined text-primary text-[32px]">school</span>
<div>
<h1 class="font-display-md text-display-md font-bold text-primary leading-tight">Gestão Escolar</h1>
<p class="font-label-muted text-label-muted text-on-surface-variant opacity-70">Administração Central</p>
</div>
</div>
</div>
<nav class="flex-1 space-y-base px-base">
<a class="flex items-center gap-md px-lg py-sm text-on-surface-variant hover:bg-surface-container-high transition-all rounded-r-full active:scale-95 duration-200" href="../dashboard_school_manager_refatorado/code.html">
<span class="material-symbols-outlined">dashboard</span>
<span class="font-body-md">Dashboard</span>
</a>
<a class="flex items-center gap-md px-lg py-sm text-on-surface-variant hover:bg-surface-container-high transition-all rounded-r-full active:scale-95 duration-200" href="../gest_o_de_alunos_school_manager/code.html">
<span class="material-symbols-outlined">group</span>
<span class="font-body-md">Alunos</span>
</a>
<a class="flex items-center gap-md px-lg py-sm text-on-surface-variant hover:bg-surface-container-high transition-all rounded-r-full active:scale-95 duration-200" href="../financeiro_pagamentos_school_manager/code.html">
<span class="material-symbols-outlined">account_balance_wallet</span>
<span class="font-body-md">Financeiro</span>
</a>
<a class="flex items-center gap-md px-lg py-sm text-on-surface-variant hover:bg-surface-container-high transition-all rounded-r-full active:scale-95 duration-200" href="../relat_rios_administrativos_school_manager/code.html">
<span class="material-symbols-outlined">analytics</span>
<span class="font-body-md">Relatórios</span>
</a>
<!-- Active Tab: Escola -->
<a class="flex items-center gap-md px-lg py-sm bg-primary text-on-primary rounded-r-full shadow-[0_4px_12px_rgba(59,130,246,0.3)] active:scale-95 duration-200" href="../escola_classes_school_manager/code.html">
<span class="material-symbols-outlined">school</span>
<span class="font-body-md font-semibold">Escola</span>
</a>
<a class="flex items-center gap-md px-lg py-sm text-on-surface-variant hover:bg-surface-container-high transition-all rounded-r-full active:scale-95 duration-200" href="../configura_es_do_sistema_school_manager/code.html">
<span class="material-symbols-outlined">settings</span>
<span class="font-body-md">Configurações</span>
</a>
</nav>
</aside>
<!-- Top App Bar Shell -->
<header class="fixed top-0 right-0 w-[calc(100%-260px)] h-16 bg-surface flex justify-between items-center px-xl border-b border-outline-variant/20 shadow-sm z-40">
<div class="flex items-center gap-md">
<h2 class="font-headline-sm text-headline-sm font-semibold text-on-surface">Escola / <span class="text-primary">Turmas</span></h2>
</div>
<div class="flex items-center gap-lg">
<div class="relative group">
<span class="absolute left-3 top-1/2 -translate-y-1/2 material-symbols-outlined text-outline">search</span>
<input class="bg-surface-container-low border border-outline-variant/30 rounded-full pl-10 pr-4 py-2 text-body-sm focus:ring-2 focus:ring-primary outline-none w-64 transition-all focus:w-80 neo-depth-inner" placeholder="Procurar turma..." type="text"/>
</div>
<div class="flex gap-sm">
<button class="p-2 hover:bg-surface-container-highest rounded-full transition-all text-on-surface-variant">
<span class="material-symbols-outlined">notifications</span>
</button>
<button class="p-2 hover:bg-surface-container-highest rounded-full transition-all text-on-surface-variant">
<span class="material-symbols-outlined">account_circle</span>
</button>
</div>
</div>
</header>
<!-- Main Content Canvas -->
<main class="ml-[260px] pt-16 pb-8 min-h-screen px-xl">
<div class="max-w-[1400px] mx-auto py-lg space-y-lg">
<!-- Context Action Bar -->
<div class="flex flex-col md:flex-row md:items-center justify-between gap-md">
<div class="flex gap-md">
<!-- Filters -->
<div class="flex flex-col">
<label class="font-label-muted text-label-muted text-on-surface-variant mb-1 ml-1">Classe</label>
<select class="bg-surface-container-high text-on-surface border border-outline-variant/30 rounded-lg px-md py-2 font-body-sm focus:ring-2 focus:ring-primary outline-none neo-depth-inner min-w-[160px]">
<option>Todas as Classes</option>
<option>10ª Classe</option>
<option>11ª Classe</option>
<option>12ª Classe</option>
<option>13ª Classe</option>
</select>
</div>
<div class="flex flex-col">
<label class="font-label-muted text-label-muted text-on-surface-variant mb-1 ml-1">Período</label>
<select class="bg-surface-container-high text-on-surface border border-outline-variant/30 rounded-lg px-md py-2 font-body-sm focus:ring-2 focus:ring-primary outline-none neo-depth-inner min-w-[160px]">
<option>Todos Períodos</option>
<option>Manhã</option>
<option>Tarde</option>
<option>Noite</option>
</select>
</div>
</div>
<button class="mt-auto bg-gradient-to-b from-primary-container to-inverse-primary text-on-primary-container font-label-bold px-lg py-3 rounded-xl shadow-[0_4px_12px_rgba(59,130,246,0.3)] flex items-center gap-xs hover:brightness-110 active:scale-95 transition-all">
<span class="material-symbols-outlined">add_circle</span>
                    Nova Turma
                </button>
</div>
<!-- Bento Layout Content -->
<div class="grid grid-cols-12 gap-lg">
<!-- Main Table Card -->
<div class="col-span-12 lg:col-span-9 bg-surface-container-low rounded-xl p-lg border border-outline-variant/10 neo-depth-outer">
<div class="flex items-center justify-between mb-lg">
<h3 class="font-headline-sm text-headline-sm font-semibold">Listagem de Turmas</h3>
<span class="bg-surface-container-high text-primary px-sm py-1 rounded-full font-label-muted neo-depth-inner">Total: 24 Turmas</span>
</div>
<div class="overflow-x-auto">
<table class="w-full text-left border-collapse">
<thead>
<tr class="text-on-surface-variant border-b border-outline-variant/20 font-label-bold uppercase tracking-wider text-[11px]">
<th class="pb-md px-md">Turma</th>
<th class="pb-md px-md">Classe</th>
<th class="pb-md px-md">Sala</th>
<th class="pb-md px-md">Período</th>
<th class="pb-md px-md">Alunos</th>
<th class="pb-md px-md text-right">Ações</th>
</tr>
</thead>
<tbody class="text-body-sm">
<tr class="hover:bg-surface-container-high transition-colors group cursor-pointer border-b border-outline-variant/5">
<td class="py-md px-md font-semibold text-primary">T10-A</td>
<td class="py-md px-md">10ª Classe - Bio</td>
<td class="py-md px-md">Sala 04</td>
<td class="py-md px-md">
<span class="px-xs py-0.5 bg-secondary-container/30 text-on-secondary-container rounded text-[11px]">Manhã</span>
</td>
<td class="py-md px-md">
<div class="flex items-center gap-xs">
<span class="font-data-numeric">32</span>
<div class="w-16 h-1.5 bg-surface-container-highest rounded-full overflow-hidden">
<div class="h-full bg-primary w-[80%]"></div>
</div>
</div>
</td>
<td class="py-md px-md text-right">
<button class="p-1.5 text-on-surface-variant hover:text-primary transition-all opacity-0 group-hover:opacity-100">
<span class="material-symbols-outlined text-[20px]">edit</span>
</button>
<button class="p-1.5 text-on-surface-variant hover:text-primary transition-all opacity-0 group-hover:opacity-100">
<span class="material-symbols-outlined text-[20px]">visibility</span>
</button>
</td>
</tr>
<tr class="hover:bg-surface-container-high transition-colors group cursor-pointer border-b border-outline-variant/5">
<td class="py-md px-md font-semibold text-primary">T11-B</td>
<td class="py-md px-md">11ª Classe - Mat</td>
<td class="py-md px-md">Sala 12</td>
<td class="py-md px-md">
<span class="px-xs py-0.5 bg-tertiary-container/30 text-on-tertiary-container rounded text-[11px]">Tarde</span>
</td>
<td class="py-md px-md">
<div class="flex items-center gap-xs">
<span class="font-data-numeric">28</span>
<div class="w-16 h-1.5 bg-surface-container-highest rounded-full overflow-hidden">
<div class="h-full bg-primary w-[70%]"></div>
</div>
</div>
</td>
<td class="py-md px-md text-right">
<button class="p-1.5 text-on-surface-variant hover:text-primary transition-all opacity-0 group-hover:opacity-100">
<span class="material-symbols-outlined text-[20px]">edit</span>
</button>
<button class="p-1.5 text-on-surface-variant hover:text-primary transition-all opacity-0 group-hover:opacity-100">
<span class="material-symbols-outlined text-[20px]">visibility</span>
</button>
</td>
</tr>
<tr class="hover:bg-surface-container-high transition-colors group cursor-pointer border-b border-outline-variant/5">
<td class="py-md px-md font-semibold text-primary">T12-C</td>
<td class="py-md px-md">12ª Classe - Inf</td>
<td class="py-md px-md">Lab Info 2</td>
<td class="py-md px-md">
<span class="px-xs py-0.5 bg-error-container/30 text-error rounded text-[11px]">Noite</span>
</td>
<td class="py-md px-md">
<div class="flex items-center gap-xs">
<span class="font-data-numeric">18</span>
<div class="w-16 h-1.5 bg-surface-container-highest rounded-full overflow-hidden">
<div class="h-full bg-error w-[45%]"></div>
</div>
</div>
</td>
<td class="py-md px-md text-right">
<button class="p-1.5 text-on-surface-variant hover:text-primary transition-all opacity-0 group-hover:opacity-100">
<span class="material-symbols-outlined text-[20px]">edit</span>
</button>
<button class="p-1.5 text-on-surface-variant hover:text-primary transition-all opacity-0 group-hover:opacity-100">
<span class="material-symbols-outlined text-[20px]">visibility</span>
</button>
</td>
</tr>
<tr class="hover:bg-surface-container-high transition-colors group cursor-pointer border-b border-outline-variant/5">
<td class="py-md px-md font-semibold text-primary">T10-D</td>
<td class="py-md px-md">10ª Classe - Letras</td>
<td class="py-md px-md">Sala 08</td>
<td class="py-md px-md">
<span class="px-xs py-0.5 bg-secondary-container/30 text-on-secondary-container rounded text-[11px]">Manhã</span>
</td>
<td class="py-md px-md">
<div class="flex items-center gap-xs">
<span class="font-data-numeric">40</span>
<div class="w-16 h-1.5 bg-surface-container-highest rounded-full overflow-hidden">
<div class="h-full bg-primary w-[100%]"></div>
</div>
</div>
</td>
<td class="py-md px-md text-right">
<button class="p-1.5 text-on-surface-variant hover:text-primary transition-all opacity-0 group-hover:opacity-100">
<span class="material-symbols-outlined text-[20px]">edit</span>
</button>
<button class="p-1.5 text-on-surface-variant hover:text-primary transition-all opacity-0 group-hover:opacity-100">
<span class="material-symbols-outlined text-[20px]">visibility</span>
</button>
</td>
</tr>
<tr class="hover:bg-surface-container-high transition-colors group cursor-pointer">
<td class="py-md px-md font-semibold text-primary">T13-E</td>
<td class="py-md px-md">13ª Classe - Prof</td>
<td class="py-md px-md">Oficina B</td>
<td class="py-md px-md">
<span class="px-xs py-0.5 bg-tertiary-container/30 text-on-tertiary-container rounded text-[11px]">Tarde</span>
</td>
<td class="py-md px-md">
<div class="flex items-center gap-xs">
<span class="font-data-numeric">22</span>
<div class="w-16 h-1.5 bg-surface-container-highest rounded-full overflow-hidden">
<div class="h-full bg-primary w-[55%]"></div>
</div>
</div>
</td>
<td class="py-md px-md text-right">
<button class="p-1.5 text-on-surface-variant hover:text-primary transition-all opacity-0 group-hover:opacity-100">
<span class="material-symbols-outlined text-[20px]">edit</span>
</button>
<button class="p-1.5 text-on-surface-variant hover:text-primary transition-all opacity-0 group-hover:opacity-100">
<span class="material-symbols-outlined text-[20px]">visibility</span>
</button>
</td>
</tr>
</tbody>
</table>
</div>
</div>
<!-- Secondary Info Cards -->
<div class="col-span-12 lg:col-span-3 space-y-lg">
<!-- Distribution Card -->
<div class="bg-surface-container-low rounded-xl p-lg border border-outline-variant/10 neo-depth-outer">
<h4 class="font-headline-sm text-label-bold text-on-surface-variant uppercase mb-md">Distribuição</h4>
<div class="space-y-md">
<div class="flex items-center justify-between">
<span class="text-body-sm">Manhã</span>
<span class="font-data-numeric text-primary">12</span>
</div>
<div class="w-full h-1 bg-surface-container-highest rounded-full">
<div class="h-full bg-primary w-1/2"></div>
</div>
<div class="flex items-center justify-between">
<span class="text-body-sm">Tarde</span>
<span class="font-data-numeric text-tertiary">8</span>
</div>
<div class="w-full h-1 bg-surface-container-highest rounded-full">
<div class="h-full bg-tertiary w-1/3"></div>
</div>
<div class="flex items-center justify-between">
<span class="text-body-sm">Noite</span>
<span class="font-data-numeric text-error">4</span>
</div>
<div class="w-full h-1 bg-surface-container-highest rounded-full">
<div class="h-full bg-error w-1/6"></div>
</div>
</div>
</div>
<!-- Quick Stats Cards -->
<div class="bg-primary-container/10 border border-primary/20 rounded-xl p-lg neo-depth-outer relative overflow-hidden group">
<div class="absolute -right-4 -top-4 opacity-10 group-hover:rotate-12 transition-transform duration-500">
<span class="material-symbols-outlined text-[100px]">groups</span>
</div>
<p class="text-on-primary-container font-label-bold uppercase text-[11px] mb-base">Alunos Ativos</p>
<h4 class="text-display-md font-data-numeric text-primary">742</h4>
<div class="mt-md flex items-center gap-xs text-primary text-[12px] font-semibold">
<span class="material-symbols-outlined text-[16px]">trending_up</span>
<span>+4% vs semestre anterior</span>
</div>
</div>
<!-- Capacity Alert -->
<div class="bg-surface-container-low border border-outline-variant/10 rounded-xl p-md flex gap-md items-center neo-depth-outer">
<div class="bg-error/20 p-2 rounded-lg text-error">
<span class="material-symbols-outlined">warning</span>
</div>
<div>
<p class="text-[12px] font-bold text-error uppercase">Alerta de Lotação</p>
<p class="text-[13px] text-on-surface-variant">Turma T10-D atingiu limite máximo.</p>
</div>
</div>
</div>
</div>
<!-- School Status Indicator -->
<div class="grid grid-cols-1 md:grid-cols-4 gap-lg">
<div class="bg-surface-container-lowest border border-outline-variant/5 p-lg rounded-xl flex items-center gap-lg neo-depth-outer">
<div class="p-3 bg-surface-container-highest rounded-xl neo-depth-inner">
<span class="material-symbols-outlined text-primary">event_available</span>
</div>
<div>
<p class="text-[11px] text-on-surface-variant uppercase font-bold">Ano Lectivo</p>
<p class="text-headline-sm font-semibold">2023/2024</p>
</div>
</div>
<div class="bg-surface-container-lowest border border-outline-variant/5 p-lg rounded-xl flex items-center gap-lg neo-depth-outer">
<div class="p-3 bg-surface-container-highest rounded-xl neo-depth-inner">
<span class="material-symbols-outlined text-tertiary">check_circle</span>
</div>
<div>
<p class="text-[11px] text-on-surface-variant uppercase font-bold">Turmas Abertas</p>
<p class="text-headline-sm font-semibold">18 / 24</p>
</div>
</div>
<div class="bg-surface-container-lowest border border-outline-variant/5 p-lg rounded-xl flex items-center gap-lg neo-depth-outer">
<div class="p-3 bg-surface-container-highest rounded-xl neo-depth-inner">
<span class="material-symbols-outlined text-on-secondary-container">meeting_room</span>
</div>
<div>
<p class="text-[11px] text-on-surface-variant uppercase font-bold">Salas Ocupadas</p>
<p class="text-headline-sm font-semibold">12 / 15</p>
</div>
</div>
<div class="bg-surface-container-lowest border border-outline-variant/5 p-lg rounded-xl flex items-center gap-lg neo-depth-outer">
<div class="p-3 bg-surface-container-highest rounded-xl neo-depth-inner">
<span class="material-symbols-outlined text-error">assignment_late</span>
</div>
<div>
<p class="text-[11px] text-on-surface-variant uppercase font-bold">Pautas Pendentes</p>
<p class="text-headline-sm font-semibold">05</p>
</div>
</div>
</div>
</div>
</main>
<!-- App Footer -->
<footer class="fixed bottom-0 right-0 w-[calc(100%-260px)] h-8 bg-surface-container-lowest flex justify-between items-center px-xl z-40">
<div class="text-label-muted text-on-surface-variant/60 font-label-muted">
            v2.4.1 | Status do Backup: <span class="text-primary">Sincronizado</span>
</div>
<div class="flex gap-lg">
<a class="text-label-muted text-on-surface-variant/60 hover:text-primary transition-all font-label-muted" href="#">Suporte</a>
<a class="text-label-muted text-on-surface-variant/60 hover:text-primary transition-all font-label-muted" href="#">Documentação</a>
</div>
</footer>
<script>
        // Micro-interaction for hover effects on neo-skeuomorphic elements
        document.querySelectorAll('.neo-depth-outer').forEach(card => {
            card.addEventListener('mouseenter', () => {
                card.style.transform = 'translateY(-2px)';
                card.style.transition = 'all 0.3s cubic-bezier(0.4, 0, 0.2, 1)';
            });
            card.addEventListener('mouseleave', () => {
                card.style.transform = 'translateY(0)';
            });
        });
    </script>
</body></html>





<!DOCTYPE html>

<html class="dark" lang="pt-BR"><head>
<meta charset="utf-8"/>
<meta content="width=device-width, initial-scale=1.0" name="viewport"/>
<title>Módulo Financeiro - Aba Caixa</title>
<script src="https://cdn.tailwindcss.com?plugins=forms,container-queries"></script>
<link href="https://fonts.googleapis.com/css2?family=Inter:wght@400;500;600;700&family=Poppins:wght@500;600;700&family=Material+Symbols+Outlined:wght,FILL@100..700,0..1&display=swap" rel="stylesheet"/>
<link href="https://fonts.googleapis.com/css2?family=Material+Symbols+Outlined:wght,FILL@100..700,0..1&display=swap" rel="stylesheet"/>
<link href="https://fonts.googleapis.com/css2?family=Inter:wght@100..900&family=Poppins:wght@100..900&display=swap" rel="stylesheet"/>
<script id="tailwind-config">
        tailwind.config = {
            darkMode: "class",
            theme: {
                extend: {
                    "colors": {
                        "on-secondary": "#263143",
                        "on-tertiary-fixed": "#001c39",
                        "error": "#ffb4ab",
                        "surface-container": "#171f33",
                        "error-container": "#93000a",
                        "on-surface-variant": "#c2c6d6",
                        "surface-bright": "#31394d",
                        "surface-tint": "#adc6ff",
                        "on-tertiary-container": "#002a51",
                        "on-primary-fixed-variant": "#004395",
                        "tertiary-fixed": "#d4e3ff",
                        "surface-dim": "#0b1326",
                        "outline": "#8c909f",
                        "secondary": "#bcc7de",
                        "on-tertiary": "#00315d",
                        "surface-container-low": "#131b2e",
                        "outline-variant": "#424754",
                        "secondary-fixed-dim": "#bcc7de",
                        "on-primary-container": "#00285d",
                        "on-background": "#dae2fd",
                        "surface-container-lowest": "#060e20",
                        "surface": "#0b1326",
                        "tertiary-fixed-dim": "#a4c9ff",
                        "on-primary": "#002e6a",
                        "primary-container": "#4d8eff",
                        "inverse-surface": "#dae2fd",
                        "surface-container-high": "#222a3d",
                        "on-error-container": "#ffdad6",
                        "on-secondary-fixed-variant": "#3c475a",
                        "background": "#0b1326",
                        "on-tertiary-fixed-variant": "#004883",
                        "on-error": "#690005",
                        "secondary-fixed": "#d8e3fb",
                        "primary-fixed": "#d8e2ff",
                        "on-primary-fixed": "#001a42",
                        "tertiary-container": "#4c93e7",
                        "primary": "#adc6ff",
                        "tertiary": "#a4c9ff",
                        "surface-container-highest": "#2d3449",
                        "surface-variant": "#2d3449",
                        "on-surface": "#dae2fd",
                        "secondary-container": "#3e495d",
                        "on-secondary-container": "#aeb9d0",
                        "inverse-primary": "#005ac2",
                        "primary-fixed-dim": "#adc6ff",
                        "on-secondary-fixed": "#111c2d",
                        "inverse-on-surface": "#283044"
                    },
                    "borderRadius": {
                        "DEFAULT": "0.25rem",
                        "lg": "0.5rem",
                        "xl": "0.75rem",
                        "full": "9999px"
                    },
                    "spacing": {
                        "sidebar_width": "260px",
                        "lg": "24px",
                        "base": "4px",
                        "sm": "12px",
                        "xl": "32px",
                        "md": "16px",
                        "xs": "8px",
                        "gutter": "20px"
                    },
                    "fontFamily": {
                        "body-lg": ["Inter"],
                        "label-muted": ["Inter"],
                        "display-lg": ["Poppins"],
                        "label-bold": ["Inter"],
                        "data-numeric": ["Inter"],
                        "headline-sm": ["Poppins"],
                        "body-md": ["Inter"],
                        "body-sm": ["Inter"],
                        "display-md": ["Poppins"]
                    },
                    "fontSize": {
                        "body-lg": ["16px", { "lineHeight": "1.5", "fontWeight": "400" }],
                        "label-muted": ["12px", { "lineHeight": "1", "fontWeight": "400" }],
                        "display-lg": ["32px", { "lineHeight": "1.2", "letterSpacing": "-0.02em", "fontWeight": "600" }],
                        "label-bold": ["12px", { "lineHeight": "1", "letterSpacing": "0.05em", "fontWeight": "600" }],
                        "data-numeric": ["20px", { "lineHeight": "1", "letterSpacing": "-0.01em", "fontWeight": "700" }],
                        "headline-sm": ["18px", { "lineHeight": "1.4", "fontWeight": "500" }],
                        "body-md": ["14px", { "lineHeight": "1.5", "fontWeight": "400" }],
                        "body-sm": ["13px", { "lineHeight": "1.5", "fontWeight": "400" }],
                        "display-md": ["24px", { "lineHeight": "1.3", "fontWeight": "600" }]
                    }
                },
            },
        }
    </script>
<style>
        .neo-glass {
            background: linear-gradient(135deg, rgba(255, 255, 255, 0.05) 0%, rgba(255, 255, 255, 0) 100%);
            border-top: 1px solid rgba(255, 255, 255, 0.1);
            border-left: 1px solid rgba(255, 255, 255, 0.1);
            backdrop-filter: blur(8px);
        }
        .neo-pressed {
            background: #111827;
            box-shadow: inset 2px 2px 5px rgba(0, 0, 0, 0.5), inset -1px -1px 3px rgba(255, 255, 255, 0.05);
        }
        .material-symbols-outlined {
            font-variation-settings: 'FILL' 0, 'wght' 400, 'GRAD' 0, 'opsz' 24;
        }
        ::-webkit-scrollbar {
            width: 8px;
        }
        ::-webkit-scrollbar-track {
            background: #0b1326;
        }
        ::-webkit-scrollbar-thumb {
            background: #2d3449;
            border-radius: 4px;
        }
    </style>
</head>
<body class="bg-background text-on-background font-body-md min-h-screen">
<!-- Desktop Sidebar (Fixed) -->
<aside class="fixed left-0 top-0 h-full w-[260px] bg-surface-container-low flex flex-col py-lg shadow-[20px_0_40px_rgba(0,0,0,0.4)] z-50">
<div class="px-xl mb-xl">
<h1 class="font-display-md text-display-md font-bold text-primary">Gestão Escolar</h1>
<p class="font-body-sm text-on-surface-variant opacity-70">Administração Central</p>
</div>
<nav class="flex-1 flex flex-col space-y-1 pr-md">
<a class="flex items-center gap-md px-xl py-md text-on-surface-variant hover:text-on-surface hover:bg-surface-container-high transition-all group" href="../dashboard_school_manager_refatorado/code.html">
<span class="material-symbols-outlined">dashboard</span>
<span class="font-body-md">Dashboard</span>
</a>
<a class="flex items-center gap-md px-xl py-md text-on-surface-variant hover:text-on-surface hover:bg-surface-container-high transition-all" href="../gest_o_de_alunos_school_manager/code.html">
<span class="material-symbols-outlined">group</span>
<span class="font-body-md">Alunos</span>
</a>
<a class="flex items-center gap-md px-xl py-md bg-primary text-on-primary rounded-r-full shadow-[0_4px_12px_rgba(59,130,246,0.3)] transition-all" href="../financeiro_pagamentos_school_manager/code.html">
<span class="material-symbols-outlined">account_balance_wallet</span>
<span class="font-body-md">Financeiro</span>
</a>
<a class="flex items-center gap-md px-xl py-md text-on-surface-variant hover:text-on-surface hover:bg-surface-container-high transition-all" href="../relat_rios_administrativos_school_manager/code.html">
<span class="material-symbols-outlined">analytics</span>
<span class="font-body-md">Relatórios</span>
</a>
<a class="flex items-center gap-md px-xl py-md text-on-surface-variant hover:text-on-surface hover:bg-surface-container-high transition-all" href="../escola_classes_school_manager/code.html">
<span class="material-symbols-outlined">school</span>
<span class="font-body-md">Escola</span>
</a>
<a class="flex items-center gap-md px-xl py-md text-on-surface-variant hover:text-on-surface hover:bg-surface-container-high transition-all" href="../configura_es_do_sistema_school_manager/code.html">
<span class="material-symbols-outlined">settings</span>
<span class="font-body-md">Configurações</span>
</a>
</nav>
<div class="px-xl mt-auto">
<div class="p-md rounded-xl bg-surface-container-high neo-glass">
<p class="font-label-muted text-label-muted text-secondary mb-xs">STATUS DO SISTEMA</p>
<div class="flex items-center gap-xs">
<span class="w-2 h-2 rounded-full bg-green-500 animate-pulse"></span>
<span class="font-body-sm text-on-surface">Servidor Online</span>
</div>
</div>
</div>
</aside>
<!-- Top App Bar -->
<header class="fixed top-0 right-0 w-[calc(100%-260px)] h-16 bg-surface flex justify-between items-center px-xl shadow-sm border-b border-outline-variant/20 z-40">
<div class="flex items-center gap-md">
<span class="material-symbols-outlined text-primary">account_balance_wallet</span>
<h2 class="font-headline-sm text-headline-sm font-semibold text-on-surface">Gestão de Caixa</h2>
</div>
<div class="flex items-center gap-xl">
<div class="relative group">
<span class="absolute left-md top-1/2 -translate-y-1/2 material-symbols-outlined text-on-surface-variant text-sm">search</span>
<input class="bg-surface-container-lowest border-outline-variant/30 rounded-full pl-xl pr-md py-xs text-body-sm focus:ring-2 focus:ring-primary w-64 transition-all" placeholder="Buscar transação..." type="text"/>
</div>
<div class="flex items-center gap-md">
<button class="material-symbols-outlined p-xs hover:bg-surface-container-highest rounded-full transition-all text-on-surface-variant">notifications</button>
<div class="flex items-center gap-sm">
<div class="text-right hidden lg:block">
<p class="font-body-sm font-bold text-on-surface leading-none">Admin Principal</p>
<p class="font-label-muted text-label-muted text-on-surface-variant">Tesouraria</p>
</div>
<span class="material-symbols-outlined text-display-md text-primary">account_circle</span>
</div>
</div>
</div>
</header>
<!-- Main Content Canvas -->
<main class="ml-[260px] pt-16 pb-12 px-xl min-h-screen">
<div class="max-w-7xl mx-auto space-y-lg py-xl">
<!-- Cashier Header & Quick Actions -->
<div class="flex flex-col md:flex-row md:items-center justify-between gap-md mb-xl">
<div>
<h3 class="font-display-md text-display-md font-bold text-on-surface mb-xs">Controle de Fluxo</h3>
<div class="flex items-center gap-sm">
<span class="px-md py-1 rounded-full bg-green-500/10 text-green-400 border border-green-500/30 flex items-center gap-xs font-label-bold">
<span class="material-symbols-outlined text-sm">lock_open</span> CAIXA ABERTO
                        </span>
<span class="font-body-sm text-on-surface-variant">Sessão iniciada em: 07:45 AM</span>
</div>
</div>
<button class="bg-gradient-to-r from-red-500 to-red-600 text-white px-xl py-md rounded-xl font-headline-sm flex items-center gap-md shadow-[0_4px_12px_rgba(239,68,68,0.3)] hover:scale-[1.02] active:scale-95 transition-all group">
<span class="material-symbols-outlined group-hover:rotate-180 transition-transform duration-500">power_settings_new</span>
                    FECHAR CAIXA
                </button>
</div>
<!-- Bento Grid KPI Cards -->
<div class="grid grid-cols-1 md:grid-cols-4 gap-lg">
<!-- Saldo Inicial -->
<div class="p-lg rounded-xl bg-surface-container neo-glass flex flex-col justify-between h-40 shadow-[0_10px_20px_rgba(0,0,0,0.3)] border-t border-l border-white/5 relative overflow-hidden group">
<div class="absolute top-0 right-0 w-24 h-24 bg-primary/5 rounded-full blur-3xl -mr-12 -mt-12 group-hover:bg-primary/10 transition-colors"></div>
<div>
<div class="flex items-center justify-between mb-sm">
<span class="font-label-bold text-secondary uppercase tracking-widest">Saldo Inicial</span>
<span class="material-symbols-outlined text-on-surface-variant/40">start</span>
</div>
<p class="font-data-numeric text-display-md text-on-surface">75.000,00 <span class="text-body-sm text-on-surface-variant ml-xs">AOA</span></p>
</div>
<div class="mt-md px-md py-1 rounded-lg bg-surface-container-lowest/50 backdrop-blur-md inline-block w-fit">
<span class="font-body-sm text-on-surface-variant">Abertura: Dinheiro</span>
</div>
</div>
<!-- Entradas -->
<div class="p-lg rounded-xl bg-surface-container neo-glass flex flex-col justify-between h-40 shadow-[0_10px_20px_rgba(0,0,0,0.3)] border-t border-l border-white/5 relative overflow-hidden group">
<div class="absolute top-0 right-0 w-24 h-24 bg-green-500/5 rounded-full blur-3xl -mr-12 -mt-12"></div>
<div>
<div class="flex items-center justify-between mb-sm">
<span class="font-label-bold text-secondary uppercase tracking-widest">Entradas</span>
<span class="material-symbols-outlined text-green-400">trending_up</span>
</div>
<p class="font-data-numeric text-display-md text-green-400">142.350,00 <span class="text-body-sm text-on-surface-variant ml-xs">AOA</span></p>
</div>
<div class="mt-md text-body-sm text-on-surface-variant flex items-center gap-xs">
<span class="material-symbols-outlined text-sm">history</span> +12 transações hoje
                    </div>
</div>
<!-- Saídas -->
<div class="p-lg rounded-xl bg-surface-container neo-glass flex flex-col justify-between h-40 shadow-[0_10px_20px_rgba(0,0,0,0.3)] border-t border-l border-white/5 relative overflow-hidden group">
<div class="absolute top-0 right-0 w-24 h-24 bg-red-500/5 rounded-full blur-3xl -mr-12 -mt-12"></div>
<div>
<div class="flex items-center justify-between mb-sm">
<span class="font-label-bold text-secondary uppercase tracking-widest">Saídas</span>
<span class="material-symbols-outlined text-red-400">trending_down</span>
</div>
<p class="font-data-numeric text-display-md text-red-400">18.200,00 <span class="text-body-sm text-on-surface-variant ml-xs">AOA</span></p>
</div>
<div class="mt-md text-body-sm text-on-surface-variant flex items-center gap-xs">
<span class="material-symbols-outlined text-sm">payments</span> 3 pagamentos manuais
                    </div>
</div>
<!-- Saldo Atual -->
<div class="p-lg rounded-xl bg-primary-container text-on-primary-container flex flex-col justify-between h-40 shadow-[0_15px_30px_rgba(77,142,255,0.25)] border-t border-l border-white/20 relative overflow-hidden group">
<div class="absolute top-0 right-0 w-32 h-32 bg-white/20 rounded-full blur-2xl -mr-16 -mt-16"></div>
<div>
<div class="flex items-center justify-between mb-sm">
<span class="font-label-bold text-on-primary-container uppercase tracking-widest opacity-80">Saldo Atualizado</span>
<span class="material-symbols-outlined" style="font-variation-settings: 'FILL' 1;">account_balance</span>
</div>
<p class="font-data-numeric text-[28px] font-extrabold">199.150,00 <span class="text-body-sm opacity-70 ml-xs">AOA</span></p>
</div>
<div class="mt-md flex justify-between items-center">
<span class="font-body-sm font-semibold flex items-center gap-xs bg-white/20 px-sm py-1 rounded-full">
                             Lucro: 63%
                        </span>
</div>
</div>
</div>
<!-- Main Content Area: Transactions Table -->
<div class="grid grid-cols-12 gap-lg">
<!-- Transactions List (8 cols) -->
<div class="col-span-12 lg:col-span-8 bg-surface-container rounded-xl shadow-xl overflow-hidden neo-glass flex flex-col">
<div class="px-xl py-lg border-b border-outline-variant/20 flex justify-between items-center bg-surface-container-high/50">
<h4 class="font-headline-sm font-semibold flex items-center gap-md">
<span class="material-symbols-outlined text-primary">list_alt</span>
                            Histórico da Sessão
                        </h4>
<div class="flex items-center gap-sm">
<button class="px-md py-xs bg-surface-container-lowest rounded-lg border border-outline-variant/30 text-body-sm hover:bg-surface-container-highest transition-all">Exportar PDF</button>
<button class="p-xs bg-surface-container-lowest rounded-lg border border-outline-variant/30 text-body-sm hover:bg-surface-container-highest transition-all material-symbols-outlined">filter_list</button>
</div>
</div>
<div class="overflow-x-auto">
<table class="w-full text-left border-collapse">
<thead class="bg-surface-container-lowest text-on-surface-variant font-label-bold uppercase text-[11px] tracking-widest">
<tr>
<th class="px-xl py-md border-b border-outline-variant/10">ID</th>
<th class="px-xl py-md border-b border-outline-variant/10">Descrição</th>
<th class="px-xl py-md border-b border-outline-variant/10">Categoria</th>
<th class="px-xl py-md border-b border-outline-variant/10">Hora</th>
<th class="px-xl py-md border-b border-outline-variant/10 text-right">Valor</th>
</tr>
</thead>
<tbody class="divide-y divide-outline-variant/10">
<tr class="hover:bg-surface-container-highest/40 transition-all cursor-pointer group">
<td class="px-xl py-md font-data-numeric text-primary">#TRX-8821</td>
<td class="px-xl py-md">
<div class="flex flex-col">
<span class="font-body-md font-medium">Propina Mensal - João Silva</span>
<span class="text-body-sm text-on-surface-variant/60">TPA • 10ª Classe A</span>
</div>
</td>
<td class="px-xl py-md">
<span class="px-sm py-1 rounded-full bg-primary/10 text-primary text-[11px] font-bold">RECEBIMENTO</span>
</td>
<td class="px-xl py-md text-on-surface-variant font-body-sm">10:24 AM</td>
<td class="px-xl py-md text-right font-data-numeric text-green-400">+ 45.000,00</td>
</tr>
<tr class="hover:bg-surface-container-highest/40 transition-all cursor-pointer group">
<td class="px-xl py-md font-data-numeric text-primary">#TRX-8819</td>
<td class="px-xl py-md">
<div class="flex flex-col">
<span class="font-body-md font-medium">Compra de Materiais de Limpeza</span>
<span class="text-body-sm text-on-surface-variant/60">Dinheiro • Manutenção</span>
</div>
</td>
<td class="px-xl py-md">
<span class="px-sm py-1 rounded-full bg-red-500/10 text-red-400 text-[11px] font-bold">DESPESA</span>
</td>
<td class="px-xl py-md text-on-surface-variant font-body-sm">09:50 AM</td>
<td class="px-xl py-md text-right font-data-numeric text-red-400">- 8.500,00</td>
</tr>
<tr class="hover:bg-surface-container-highest/40 transition-all cursor-pointer group">
<td class="px-xl py-md font-data-numeric text-primary">#TRX-8815</td>
<td class="px-xl py-md">
<div class="flex flex-col">
<span class="font-body-md font-medium">Uniforme Escolar (Kit Completo)</span>
<span class="text-body-sm text-on-surface-variant/60">Multicaixa • Stock</span>
</div>
</td>
<td class="px-xl py-md">
<span class="px-sm py-1 rounded-full bg-primary/10 text-primary text-[11px] font-bold">VENDA</span>
</td>
<td class="px-xl py-md text-on-surface-variant font-body-sm">09:15 AM</td>
<td class="px-xl py-md text-right font-data-numeric text-green-400">+ 12.000,00</td>
</tr>
<tr class="hover:bg-surface-container-highest/40 transition-all cursor-pointer group">
<td class="px-xl py-md font-data-numeric text-primary">#TRX-8812</td>
<td class="px-xl py-md">
<div class="flex flex-col">
<span class="font-body-md font-medium">Transporte Escolar (Mensalidade)</span>
<span class="text-body-sm text-on-surface-variant/60">Transferência • Maria Ndala</span>
</div>
</td>
<td class="px-xl py-md">
<span class="px-sm py-1 rounded-full bg-primary/10 text-primary text-[11px] font-bold">SERVIÇO</span>
</td>
<td class="px-xl py-md text-on-surface-variant font-body-sm">08:42 AM</td>
<td class="px-xl py-md text-right font-data-numeric text-green-400">+ 25.000,00</td>
</tr>
<tr class="hover:bg-surface-container-highest/40 transition-all cursor-pointer group">
<td class="px-xl py-md font-data-numeric text-primary">#TRX-8805</td>
<td class="px-xl py-md">
<div class="flex flex-col">
<span class="font-body-md font-medium">Reembolso - Inscrição Duplicada</span>
<span class="text-body-sm text-on-surface-variant/60">Dinheiro • Administrativo</span>
</div>
</td>
<td class="px-xl py-md">
<span class="px-sm py-1 rounded-full bg-red-500/10 text-red-400 text-[11px] font-bold">REEMBOLSO</span>
</td>
<td class="px-xl py-md text-on-surface-variant font-body-sm">08:10 AM</td>
<td class="px-xl py-md text-right font-data-numeric text-red-400">- 9.700,00</td>
</tr>
</tbody>
</table>
</div>
<div class="mt-auto px-xl py-md border-t border-outline-variant/10 bg-surface-container-lowest/30 flex justify-between items-center">
<span class="text-body-sm text-on-surface-variant">Mostrando 5 de 15 transações</span>
<div class="flex gap-xs">
<button class="p-1 rounded bg-surface-container hover:bg-primary/20 text-on-surface-variant material-symbols-outlined text-sm">chevron_left</button>
<button class="p-1 rounded bg-primary text-on-primary text-[12px] px-sm font-bold">1</button>
<button class="p-1 rounded bg-surface-container hover:bg-primary/20 text-on-surface-variant text-[12px] px-sm font-bold">2</button>
<button class="p-1 rounded bg-surface-container hover:bg-primary/20 text-on-surface-variant material-symbols-outlined text-sm">chevron_right</button>
</div>
</div>
</div>
<!-- Sidemenu Column (4 cols) -->
<div class="col-span-12 lg:col-span-4 space-y-lg">
<!-- New Entry Form -->
<div class="bg-surface-container rounded-xl p-lg neo-glass shadow-xl border-t border-l border-white/5">
<h4 class="font-headline-sm font-semibold mb-lg flex items-center gap-sm">
<span class="material-symbols-outlined text-primary">add_circle</span>
                            Nova Transação
                        </h4>
<div class="space-y-md">
<div>
<label class="font-label-bold text-on-surface-variant/70 mb-xs block uppercase text-[10px]">Tipo de Operação</label>
<div class="grid grid-cols-2 gap-sm">
<button class="flex items-center justify-center gap-sm py-md rounded-lg bg-surface-container-lowest border-2 border-primary text-primary font-bold shadow-[0_0_15px_rgba(173,198,255,0.2)]">
<span class="material-symbols-outlined">input</span> Entrada
                                    </button>
<button class="flex items-center justify-center gap-sm py-md rounded-lg bg-surface-container-lowest border-2 border-transparent text-on-surface-variant hover:border-outline-variant/50 transition-all">
<span class="material-symbols-outlined">output</span> Saída
                                    </button>
</div>
</div>
<div>
<label class="font-label-bold text-on-surface-variant/70 mb-xs block uppercase text-[10px]">Valor (AOA)</label>
<div class="neo-pressed rounded-lg p-xs">
<input class="w-full bg-transparent border-none focus:ring-0 text-display-md font-data-numeric text-on-surface placeholder:text-on-surface-variant/30 text-right" placeholder="0,00" type="text"/>
</div>
</div>
<div>
<label class="font-label-bold text-on-surface-variant/70 mb-xs block uppercase text-[10px]">Descrição / Beneficiário</label>
<div class="neo-pressed rounded-lg p-xs">
<input class="w-full bg-transparent border-none focus:ring-0 text-body-md text-on-surface placeholder:text-on-surface-variant/30" placeholder="Ex: Pagamento Propina..." type="text"/>
</div>
</div>
<button class="w-full py-md rounded-xl bg-gradient-to-r from-primary to-primary-container text-on-primary font-bold shadow-lg hover:shadow-primary/40 transition-all flex items-center justify-center gap-md">
<span class="material-symbols-outlined">check_circle</span>
                                REGISTRAR NO CAIXA
                            </button>
</div>
</div>
<!-- Session Info -->
<div class="bg-surface-container rounded-xl p-lg neo-glass shadow-xl border-t border-l border-white/5">
<h4 class="font-label-bold text-secondary uppercase tracking-widest mb-md">Resumo por Pagamento</h4>
<div class="space-y-sm">
<div class="flex justify-between items-center text-body-sm p-sm rounded-lg bg-surface-container-lowest/50">
<span class="flex items-center gap-sm"><span class="material-symbols-outlined text-sm">payments</span> Dinheiro</span>
<span class="font-data-numeric text-on-surface">42.500,00</span>
</div>
<div class="flex justify-between items-center text-body-sm p-sm rounded-lg bg-surface-container-lowest/50">
<span class="flex items-center gap-sm"><span class="material-symbols-outlined text-sm">credit_card</span> Multicaixa</span>
<span class="font-data-numeric text-on-surface">125.150,00</span>
</div>
<div class="flex justify-between items-center text-body-sm p-sm rounded-lg bg-surface-container-lowest/50">
<span class="flex items-center gap-sm"><span class="material-symbols-outlined text-sm">account_balance</span> Transferência</span>
<span class="font-data-numeric text-on-surface">31.500,00</span>
</div>
</div>
<div class="mt-lg pt-lg border-t border-outline-variant/10">
<div class="flex justify-between items-center">
<span class="font-body-sm text-on-surface-variant">Conferência Física:</span>
<span class="font-body-sm text-green-400 font-bold">Pendente</span>
</div>
</div>
</div>
</div>
</div>
</div>
</main>
<!-- Footer Status -->
<footer class="fixed bottom-0 right-0 w-[calc(100%-260px)] h-8 bg-surface-container-lowest flex justify-between items-center px-xl z-40 border-t border-outline-variant/10">
<div class="flex items-center gap-lg">
<p class="font-label-muted text-label-muted text-on-surface-variant/60">v2.4.1 | Status do Backup: Sincronizado</p>
<div class="flex items-center gap-xs">
<span class="w-1.5 h-1.5 rounded-full bg-blue-500"></span>
<span class="font-label-muted text-label-muted text-on-surface-variant/60">Última sync: há 2 min</span>
</div>
</div>
<div class="flex items-center gap-lg">
<a class="font-label-muted text-label-muted text-on-surface-variant/60 hover:text-primary transition-colors" href="#">Suporte</a>
<a class="font-label-muted text-label-muted text-on-surface-variant/60 hover:text-primary transition-colors" href="#">Documentação</a>
</div>
</footer>
<script>
        // Micro-interactions and state handling
        document.addEventListener('DOMContentLoaded', () => {
            const cashierBtn = document.querySelector('button.from-red-500');
            cashierBtn.addEventListener('click', () => {
                if(confirm('Deseja realmente fechar o caixa desta sessão? Todos os valores serão consolidados.')) {
                    alert('Caixa fechado com sucesso! Gerando relatório de encerramento...');
                    window.location.reload();
                }
            });

            // Smooth entrance for cards
            const cards = document.querySelectorAll('.neo-glass');
            cards.forEach((card, index) => {
                card.style.opacity = '0';
                card.style.transform = 'translateY(20px)';
                setTimeout(() => {
                    card.style.transition = 'all 0.5s cubic-bezier(0.16, 1, 0.3, 1)';
                    card.style.opacity = '1';
                    card.style.transform = 'translateY(0)';
                }, 100 * index);
            });
        });
    </script>
</body></html>



<!DOCTYPE html>

<html class="dark" lang="pt-BR"><head>
<meta charset="utf-8"/>
<meta content="width=device-width, initial-scale=1.0" name="viewport"/>
<title>Financeiro - Entradas | Gestão Escolar</title>
<script src="https://cdn.tailwindcss.com?plugins=forms,container-queries"></script>
<link href="https://fonts.googleapis.com/css2?family=Inter:wght@400;500;600;700&amp;family=Poppins:wght@500;600;700;800&amp;display=swap" rel="stylesheet"/>
<link href="https://fonts.googleapis.com/css2?family=Material+Symbols+Outlined:wght,FILL@100..700,0..1&amp;display=swap" rel="stylesheet"/>
<link href="https://fonts.googleapis.com/css2?family=Material+Symbols+Outlined:wght,FILL@100..700,0..1&amp;display=swap" rel="stylesheet"/>
<!-- Tailwind Configuration -->
<script id="tailwind-config">
      tailwind.config = {
        darkMode: "class",
        theme: {
          extend: {
            "colors": {
                    "on-secondary": "#263143",
                    "on-tertiary-fixed": "#001c39",
                    "error": "#ffb4ab",
                    "surface-container": "#171f33",
                    "error-container": "#93000a",
                    "on-surface-variant": "#c2c6d6",
                    "surface-bright": "#31394d",
                    "surface-tint": "#adc6ff",
                    "on-tertiary-container": "#002a51",
                    "on-primary-fixed-variant": "#004395",
                    "tertiary-fixed": "#d4e3ff",
                    "surface-dim": "#0b1326",
                    "outline": "#8c909f",
                    "secondary": "#bcc7de",
                    "on-tertiary": "#00315d",
                    "surface-container-low": "#131b2e",
                    "outline-variant": "#424754",
                    "secondary-fixed-dim": "#bcc7de",
                    "on-primary-container": "#00285d",
                    "on-background": "#dae2fd",
                    "surface-container-lowest": "#060e20",
                    "surface": "#0b1326",
                    "tertiary-fixed-dim": "#a4c9ff",
                    "on-primary": "#002e6a",
                    "primary-container": "#4d8eff",
                    "inverse-surface": "#dae2fd",
                    "surface-container-high": "#222a3d",
                    "on-error-container": "#ffdad6",
                    "on-secondary-fixed-variant": "#3c475a",
                    "background": "#0b1326",
                    "on-tertiary-fixed-variant": "#004883",
                    "on-error": "#690005",
                    "secondary-fixed": "#d8e3fb",
                    "primary-fixed": "#d8e2ff",
                    "on-primary-fixed": "#001a42",
                    "tertiary-container": "#4c93e7",
                    "primary": "#adc6ff",
                    "tertiary": "#a4c9ff",
                    "surface-container-highest": "#2d3449",
                    "surface-variant": "#2d3449",
                    "on-surface": "#dae2fd",
                    "secondary-container": "#3e495d",
                    "on-secondary-container": "#aeb9d0",
                    "inverse-primary": "#005ac2",
                    "primary-fixed-dim": "#adc6ff",
                    "on-secondary-fixed": "#111c2d",
                    "inverse-on-surface": "#283044"
            },
            "borderRadius": {
                    "DEFAULT": "0.25rem",
                    "lg": "0.5rem",
                    "xl": "0.75rem",
                    "full": "9999px"
            },
            "spacing": {
                    "sidebar_width": "260px",
                    "lg": "24px",
                    "base": "4px",
                    "sm": "12px",
                    "xl": "32px",
                    "md": "16px",
                    "xs": "8px",
                    "gutter": "20px"
            },
            "fontFamily": {
                    "body-lg": ["Inter"],
                    "label-muted": ["Inter"],
                    "display-lg": ["Poppins"],
                    "label-bold": ["Inter"],
                    "data-numeric": ["Inter"],
                    "headline-sm": ["Poppins"],
                    "body-md": ["Inter"],
                    "body-sm": ["Inter"],
                    "display-md": ["Poppins"]
            },
            "fontSize": {
                    "body-lg": ["16px", {"lineHeight": "1.5", "fontWeight": "400"}],
                    "label-muted": ["12px", {"lineHeight": "1", "fontWeight": "400"}],
                    "display-lg": ["32px", {"lineHeight": "1.2", "letterSpacing": "-0.02em", "fontWeight": "600"}],
                    "label-bold": ["12px", {"lineHeight": "1", "letterSpacing": "0.05em", "fontWeight": "600"}],
                    "data-numeric": ["20px", {"lineHeight": "1", "letterSpacing": "-0.01em", "fontWeight": "700"}],
                    "headline-sm": ["18px", {"lineHeight": "1.4", "fontWeight": "500"}],
                    "body-md": ["14px", {"lineHeight": "1.5", "fontWeight": "400"}],
                    "body-sm": ["13px", {"lineHeight": "1.5", "fontWeight": "400"}],
                    "display-md": ["24px", {"lineHeight": "1.3", "fontWeight": "600"}]
            }
          },
        },
      }
    </script>
<style>
        .neo-pressed {
            box-shadow: inset 4px 4px 8px rgba(0, 0, 0, 0.4), 
                        inset -2px -2px 4px rgba(255, 255, 255, 0.05);
        }
        .neo-float {
            box-shadow: 10px 10px 20px rgba(0, 0, 0, 0.3), 
                        -2px -2px 10px rgba(255, 255, 255, 0.05);
            border: 1px solid rgba(255, 255, 255, 0.05);
        }
        .glass-accent {
            background: rgba(255, 255, 255, 0.03);
            backdrop-filter: blur(8px);
        }
        ::-webkit-scrollbar {
            width: 8px;
        }
        ::-webkit-scrollbar-track {
            background: #0b1326;
        }
        ::-webkit-scrollbar-thumb {
            background: #2d3449;
            border-radius: 10px;
        }
        ::-webkit-scrollbar-thumb:hover {
            background: #3e495d;
        }
    </style>
</head>
<body class="bg-background text-on-surface font-body-md selection:bg-primary/30">
<!-- Desktop Side Navigation -->
<aside class="fixed left-0 top-0 h-full w-[260px] bg-surface-container-low flex flex-col py-lg shadow-[20px_0_40px_rgba(0,0,0,0.4)] z-50">
<div class="px-md mb-xl">
<div class="flex items-center gap-xs">
<div class="w-10 h-10 rounded-lg bg-primary-container flex items-center justify-center text-on-primary-container">
<span class="material-symbols-outlined" style="font-variation-settings: 'FILL' 1;">school</span>
</div>
<div>
<h1 class="font-display-md text-display-md font-bold text-primary">Gestão Escolar</h1>
<p class="font-label-muted text-label-muted text-on-surface-variant/60">Administração Central</p>
</div>
</div>
</div>
<nav class="flex-1 space-y-base">
<!-- Navigation Items Mapping from JSON -->
<a class="flex items-center gap-md px-md py-sm mx-xs text-on-surface-variant hover:text-on-surface transition-all active:scale-95 duration-200 group" href="../dashboard_school_manager_refatorado/code.html">
<span class="material-symbols-outlined">dashboard</span>
<span class="font-body-md">Dashboard</span>
</a>
<a class="flex items-center gap-md px-md py-sm mx-xs text-on-surface-variant hover:text-on-surface transition-all active:scale-95 duration-200 group" href="../gest_o_de_alunos_school_manager/code.html">
<span class="material-symbols-outlined">group</span>
<span class="font-body-md">Alunos</span>
</a>
<!-- Active Tab: Financeiro -->
<a class="flex items-center gap-md px-md py-sm mx-xs bg-primary text-on-primary rounded-r-full shadow-[0_4px_12px_rgba(59,130,246,0.3)] active:scale-95 duration-200" href="../financeiro_pagamentos_school_manager/code.html">
<span class="material-symbols-outlined" style="font-variation-settings: 'FILL' 1;">account_balance_wallet</span>
<span class="font-body-md font-semibold">Financeiro</span>
</a>
<a class="flex items-center gap-md px-md py-sm mx-xs text-on-surface-variant hover:text-on-surface transition-all active:scale-95 duration-200 group" href="../relat_rios_administrativos_school_manager/code.html">
<span class="material-symbols-outlined">analytics</span>
<span class="font-body-md">Relatórios</span>
</a>
<a class="flex items-center gap-md px-md py-sm mx-xs text-on-surface-variant hover:text-on-surface transition-all active:scale-95 duration-200 group" href="../escola_classes_school_manager/code.html">
<span class="material-symbols-outlined">school</span>
<span class="font-body-md">Escola</span>
</a>
<a class="flex items-center gap-md px-md py-sm mx-xs text-on-surface-variant hover:text-on-surface transition-all active:scale-95 duration-200 group" href="../configura_es_do_sistema_school_manager/code.html">
<span class="material-symbols-outlined">settings</span>
<span class="font-body-md">Configurações</span>
</a>
</nav>
<div class="px-md pt-lg border-t border-outline-variant/10">
<div class="neo-pressed p-sm rounded-xl flex items-center gap-sm">
<div class="w-8 h-8 rounded-full overflow-hidden bg-surface-container-high">
<img class="w-full h-full object-cover" data-alt="A professional close-up studio portrait of a serious Angolan school administrator in a navy blue suit, set against a blurred academic office background. The lighting is crisp and cinematic, emphasizing a professional and authoritative atmosphere within a modern school management context. Rich dark tones and subtle blue highlights reflect the UI's color palette." src="https://lh3.googleusercontent.com/aida-public/AB6AXuC6bGMaTrblyQ5-wKiKRN9ctWJ7h4MtyCKIZtVp-La-QBxZ06zkb5SQGIuN-ARMzbkLE8WFctSxJBHbv-JAZ-pgot33w9xcGt5MDFvFAXuYWN7ZVUd4ieT7WVpsmOL0JlD3xPciAKZZAiOMOhRgCJZleSepWY2nFdMwowJLveEjsJutGT1QuG-2xgmM72M69VssA_Ro5Hpt3HiJ-TPoEOXuhrfMg_ua9azIncGRUs52wIOslpsQH2b5MXARxZKTLyvxrICRoCK-5qLQ"/>
</div>
<div class="overflow-hidden">
<p class="font-label-bold text-label-bold text-on-surface truncate">Dr. Alberto Silva</p>
<p class="font-label-muted text-label-muted text-on-surface-variant truncate text-[10px]">Tesoureiro Sénior</p>
</div>
</div>
</div>
</aside>
<!-- Main Content Area -->
<main class="ml-[260px] min-h-screen pb-xl">
<!-- TopAppBar -->
<header class="h-16 flex justify-between items-center px-xl w-full bg-surface/80 backdrop-blur-md sticky top-0 z-40 border-b border-outline-variant/10">
<div class="flex items-center gap-md">
<h2 class="font-headline-sm text-headline-sm font-semibold text-on-surface">Módulo Financeiro <span class="text-on-surface-variant mx-xs">/</span> <span class="text-primary">Entradas</span></h2>
</div>
<div class="flex items-center gap-lg">
<div class="relative group">
<div class="absolute inset-y-0 left-3 flex items-center pointer-events-none text-on-surface-variant">
<span class="material-symbols-outlined text-[20px]">search</span>
</div>
<input class="bg-surface-container-lowest border-outline-variant/30 text-body-sm rounded-full pl-10 pr-md py-xs w-64 focus:ring-2 focus:ring-primary focus:border-transparent transition-all outline-none" placeholder="Procurar recibos..." type="text"/>
</div>
<div class="flex items-center gap-sm">
<button class="w-10 h-10 rounded-full flex items-center justify-center text-on-surface-variant hover:bg-surface-container-highest transition-all relative">
<span class="material-symbols-outlined">notifications</span>
<span class="absolute top-2 right-2 w-2 h-2 bg-error rounded-full"></span>
</button>
<button class="w-10 h-10 rounded-full flex items-center justify-center text-on-surface-variant hover:bg-surface-container-highest transition-all">
<span class="material-symbols-outlined">account_circle</span>
</button>
</div>
</div>
</header>
<div class="px-xl pt-lg">
<!-- Summary KPI Cards -->
<div class="grid grid-cols-1 md:grid-cols-3 gap-lg mb-xl">
<!-- Total Entradas Hoje -->
<div class="neo-float bg-surface-container-high rounded-xl p-lg relative overflow-hidden group">
<div class="flex justify-between items-start">
<div>
<p class="font-label-muted text-label-muted text-on-surface-variant mb-xs">Total Entradas Hoje</p>
<h3 class="font-display-md text-display-md font-bold text-on-surface">450.200,00 <span class="text-primary-fixed-dim text-[16px]">Kz</span></h3>
</div>
<div class="w-12 h-12 bg-primary/10 rounded-lg flex items-center justify-center text-primary">
<span class="material-symbols-outlined text-[28px]" style="font-variation-settings: 'FILL' 1;">payments</span>
</div>
</div>
<div class="mt-md flex items-center gap-xs">
<div class="glass-accent rounded px-xs py-[2px] flex items-center gap-1">
<span class="material-symbols-outlined text-[14px] text-green-400">trending_up</span>
<span class="text-[11px] font-bold text-green-400">+12.5%</span>
</div>
<span class="text-[11px] text-on-surface-variant/60">vs. ontem</span>
</div>
<div class="absolute -right-4 -bottom-4 opacity-5 group-hover:opacity-10 transition-opacity">
<span class="material-symbols-outlined text-[100px]">account_balance_wallet</span>
</div>
</div>
<!-- Entradas do Mês -->
<div class="neo-float bg-surface-container-high rounded-xl p-lg relative overflow-hidden group">
<div class="flex justify-between items-start">
<div>
<p class="font-label-muted text-label-muted text-on-surface-variant mb-xs">Entradas do Mês (Março)</p>
<h3 class="font-display-md text-display-md font-bold text-on-surface">12.840.000,00 <span class="text-primary-fixed-dim text-[16px]">Kz</span></h3>
</div>
<div class="w-12 h-12 bg-tertiary-container/20 rounded-lg flex items-center justify-center text-tertiary">
<span class="material-symbols-outlined text-[28px]" style="font-variation-settings: 'FILL' 1;">calendar_month</span>
</div>
</div>
<div class="mt-md flex items-center gap-xs">
<div class="glass-accent rounded px-xs py-[2px] flex items-center gap-1">
<span class="material-symbols-outlined text-[14px] text-green-400">trending_up</span>
<span class="text-[11px] font-bold text-green-400">+5.2%</span>
</div>
<span class="text-[11px] text-on-surface-variant/60">vs. mês anterior</span>
</div>
</div>
<!-- Action Card -->
<div class="bg-primary rounded-xl p-lg flex flex-col justify-between shadow-[0_8px_24px_rgba(59,130,246,0.3)] hover:shadow-[0_12px_32px_rgba(59,130,246,0.4)] transition-all cursor-pointer group active:scale-95">
<div class="flex justify-between items-center">
<span class="material-symbols-outlined text-on-primary text-[32px]">add_circle</span>
<span class="material-symbols-outlined text-on-primary/40 group-hover:translate-x-1 transition-transform">arrow_forward_ios</span>
</div>
<div>
<h3 class="font-headline-sm text-headline-sm font-bold text-on-primary">Nova Entrada</h3>
<p class="font-body-sm text-body-sm text-on-primary/80">Registrar novo pagamento ou receita</p>
</div>
</div>
</div>
<!-- Main Data Canvas -->
<div class="neo-float bg-surface-container rounded-xl overflow-hidden border border-outline-variant/10">
<!-- Table Headers & Filters -->
<div class="p-lg flex flex-wrap items-center justify-between gap-md border-b border-outline-variant/10">
<div class="flex items-center gap-md">
<div class="neo-pressed bg-background rounded-lg px-md py-xs flex items-center gap-sm">
<span class="material-symbols-outlined text-[20px] text-primary">filter_list</span>
<select class="bg-transparent border-none text-body-sm text-on-surface outline-none focus:ring-0 cursor-pointer">
<option class="bg-surface-container">Todas Categorias</option>
<option class="bg-surface-container">Matrículas</option>
<option class="bg-surface-container">Uniformes</option>
<option class="bg-surface-container">Cartões</option>
<option class="bg-surface-container">Multas</option>
</select>
</div>
<div class="neo-pressed bg-background rounded-lg px-md py-xs flex items-center gap-sm">
<span class="material-symbols-outlined text-[20px] text-primary">event</span>
<input class="bg-transparent border-none text-body-sm text-on-surface outline-none focus:ring-0 cursor-pointer" type="date"/>
</div>
</div>
<div class="flex items-center gap-sm">
<button class="neo-pressed bg-background text-on-surface px-md py-xs rounded-lg flex items-center gap-xs hover:bg-surface-container-highest transition-colors">
<span class="material-symbols-outlined text-[18px]">download</span>
<span class="font-label-bold text-label-bold">Exportar</span>
</button>
</div>
</div>
<!-- Table Content -->
<div class="overflow-x-auto">
<table class="w-full text-left border-collapse">
<thead>
<tr class="bg-surface-container-high/50">
<th class="px-lg py-md font-label-bold text-label-bold text-on-surface-variant uppercase tracking-wider">Recibo ID</th>
<th class="px-lg py-md font-label-bold text-label-bold text-on-surface-variant uppercase tracking-wider">Aluno / Origem</th>
<th class="px-lg py-md font-label-bold text-label-bold text-on-surface-variant uppercase tracking-wider">Categoria</th>
<th class="px-lg py-md font-label-bold text-label-bold text-on-surface-variant uppercase tracking-wider">Data</th>
<th class="px-lg py-md font-label-bold text-label-bold text-on-surface-variant uppercase tracking-wider">Valor (Kz)</th>
<th class="px-lg py-md font-label-bold text-label-bold text-on-surface-variant uppercase tracking-wider text-right">Ações</th>
</tr>
</thead>
<tbody class="divide-y divide-outline-variant/10">
<!-- Row 1 -->
<tr class="hover:bg-surface-container-highest/30 transition-colors group">
<td class="px-lg py-md text-primary font-mono text-[13px]">#RC-2024-00124</td>
<td class="px-lg py-md">
<div class="flex items-center gap-sm">
<div class="w-8 h-8 rounded bg-secondary-container/30 flex items-center justify-center text-secondary">
<span class="material-symbols-outlined text-[18px]">person</span>
</div>
<div>
<p class="font-body-md font-medium text-on-surface">Mauro Manuel G. Santos</p>
<p class="text-[11px] text-on-surface-variant/70">ID: 10423-S</p>
</div>
</div>
</td>
<td class="px-lg py-md">
<span class="px-sm py-[2px] rounded-full bg-blue-500/10 text-blue-400 text-[11px] font-bold border border-blue-500/20">Matrículas</span>
</td>
<td class="px-lg py-md text-on-surface-variant text-[13px]">12 Mar 2024, 09:45</td>
<td class="px-lg py-md font-data-numeric text-on-surface">75.000,00</td>
<td class="px-lg py-md text-right">
<button class="w-8 h-8 rounded-full hover:bg-surface-container-highest text-on-surface-variant hover:text-primary transition-all">
<span class="material-symbols-outlined">print</span>
</button>
</td>
</tr>
<!-- Row 2 -->
<tr class="hover:bg-surface-container-highest/30 transition-colors group">
<td class="px-lg py-md text-primary font-mono text-[13px]">#RC-2024-00125</td>
<td class="px-lg py-md">
<div class="flex items-center gap-sm">
<div class="w-8 h-8 rounded bg-secondary-container/30 flex items-center justify-center text-secondary">
<span class="material-symbols-outlined text-[18px]">person</span>
</div>
<div>
<p class="font-body-md font-medium text-on-surface">Isabel Catarina Bumba</p>
<p class="text-[11px] text-on-surface-variant/70">ID: 09812-P</p>
</div>
</div>
</td>
<td class="px-lg py-md">
<span class="px-sm py-[2px] rounded-full bg-purple-500/10 text-purple-400 text-[11px] font-bold border border-purple-500/20">Uniformes</span>
</td>
<td class="px-lg py-md text-on-surface-variant text-[13px]">12 Mar 2024, 10:12</td>
<td class="px-lg py-md font-data-numeric text-on-surface">15.500,00</td>
<td class="px-lg py-md text-right">
<button class="w-8 h-8 rounded-full hover:bg-surface-container-highest text-on-surface-variant hover:text-primary transition-all">
<span class="material-symbols-outlined">print</span>
</button>
</td>
</tr>
<!-- Row 3 -->
<tr class="hover:bg-surface-container-highest/30 transition-colors group">
<td class="px-lg py-md text-primary font-mono text-[13px]">#RC-2024-00126</td>
<td class="px-lg py-md">
<div class="flex items-center gap-sm">
<div class="w-8 h-8 rounded bg-secondary-container/30 flex items-center justify-center text-secondary">
<span class="material-symbols-outlined text-[18px]">person</span>
</div>
<div>
<p class="font-body-md font-medium text-on-surface">Edmilson Jorge Viera</p>
<p class="text-[11px] text-on-surface-variant/70">ID: 11005-S</p>
</div>
</div>
</td>
<td class="px-lg py-md">
<span class="px-sm py-[2px] rounded-full bg-amber-500/10 text-amber-400 text-[11px] font-bold border border-amber-500/20">Cartões</span>
</td>
<td class="px-lg py-md text-on-surface-variant text-[13px]">12 Mar 2024, 11:30</td>
<td class="px-lg py-md font-data-numeric text-on-surface">5.000,00</td>
<td class="px-lg py-md text-right">
<button class="w-8 h-8 rounded-full hover:bg-surface-container-highest text-on-surface-variant hover:text-primary transition-all">
<span class="material-symbols-outlined">print</span>
</button>
</td>
</tr>
<!-- Row 4 -->
<tr class="hover:bg-surface-container-highest/30 transition-colors group">
<td class="px-lg py-md text-primary font-mono text-[13px]">#RC-2024-00127</td>
<td class="px-lg py-md">
<div class="flex items-center gap-sm">
<div class="w-8 h-8 rounded bg-secondary-container/30 flex items-center justify-center text-secondary">
<span class="material-symbols-outlined text-[18px]">corporate_fare</span>
</div>
<div>
<p class="font-body-md font-medium text-on-surface">Venda Direta Cantina</p>
<p class="text-[11px] text-on-surface-variant/70">Externo</p>
</div>
</div>
</td>
<td class="px-lg py-md">
<span class="px-sm py-[2px] rounded-full bg-emerald-500/10 text-emerald-400 text-[11px] font-bold border border-emerald-500/20">Outras Receitas</span>
</td>
<td class="px-lg py-md text-on-surface-variant text-[13px]">12 Mar 2024, 12:05</td>
<td class="px-lg py-md font-data-numeric text-on-surface">124.700,00</td>
<td class="px-lg py-md text-right">
<button class="w-8 h-8 rounded-full hover:bg-surface-container-highest text-on-surface-variant hover:text-primary transition-all">
<span class="material-symbols-outlined">print</span>
</button>
</td>
</tr>
</tbody>
</table>
</div>
<!-- Pagination -->
<div class="p-md flex items-center justify-between border-t border-outline-variant/10">
<p class="text-[12px] text-on-surface-variant">Exibindo 1-10 de 145 entradas</p>
<div class="flex gap-xs">
<button class="w-8 h-8 rounded flex items-center justify-center neo-pressed bg-background text-on-surface-variant disabled:opacity-50" disabled="">
<span class="material-symbols-outlined text-[18px]">chevron_left</span>
</button>
<button class="w-8 h-8 rounded flex items-center justify-center bg-primary text-on-primary font-bold text-[12px]">1</button>
<button class="w-8 h-8 rounded flex items-center justify-center neo-pressed bg-background text-on-surface-variant hover:text-on-surface transition-colors text-[12px]">2</button>
<button class="w-8 h-8 rounded flex items-center justify-center neo-pressed bg-background text-on-surface-variant hover:text-on-surface transition-colors text-[12px]">3</button>
<button class="w-8 h-8 rounded flex items-center justify-center neo-pressed bg-background text-on-surface-variant hover:text-on-surface transition-colors">
<span class="material-symbols-outlined text-[18px]">chevron_right</span>
</button>
</div>
</div>
</div>
<!-- Visual Decorative Element (Neo-skeuomorphic Graph) -->
<div class="mt-xl grid grid-cols-1 md:grid-cols-2 gap-lg">
<div class="neo-float bg-surface-container rounded-xl p-lg">
<div class="flex justify-between items-center mb-lg">
<h4 class="font-headline-sm text-headline-sm font-semibold">Distribuição por Categoria</h4>
<span class="material-symbols-outlined text-on-surface-variant cursor-pointer">more_vert</span>
</div>
<div class="space-y-md">
<div class="space-y-xs">
<div class="flex justify-between text-body-sm">
<span>Matrículas</span>
<span class="font-bold">65%</span>
</div>
<div class="h-2 bg-background rounded-full neo-pressed overflow-hidden">
<div class="h-full bg-primary rounded-full shadow-[0_0_10px_rgba(173,198,255,0.5)]" style="width: 65%"></div>
</div>
</div>
<div class="space-y-xs">
<div class="flex justify-between text-body-sm">
<span>Uniformes</span>
<span class="font-bold">20%</span>
</div>
<div class="h-2 bg-background rounded-full neo-pressed overflow-hidden">
<div class="h-full bg-purple-500 rounded-full" style="width: 20%"></div>
</div>
</div>
<div class="space-y-xs">
<div class="flex justify-between text-body-sm">
<span>Outros</span>
<span class="font-bold">15%</span>
</div>
<div class="h-2 bg-background rounded-full neo-pressed overflow-hidden">
<div class="h-full bg-secondary rounded-full" style="width: 15%"></div>
</div>
</div>
</div>
</div>
<div class="neo-float bg-surface-container rounded-xl p-lg flex flex-col justify-center items-center text-center">
<div class="w-16 h-16 rounded-full neo-pressed flex items-center justify-center text-primary mb-md">
<span class="material-symbols-outlined text-[32px]">verified_user</span>
</div>
<h4 class="font-headline-sm text-headline-sm font-semibold mb-xs">Status do Sistema</h4>
<p class="text-body-sm text-on-surface-variant max-w-[280px]">As transações são processadas em tempo real e auditadas pelo Banco Nacional de Angola.</p>
<div class="mt-md px-md py-xs bg-green-500/10 text-green-400 rounded-full flex items-center gap-xs">
<span class="w-2 h-2 rounded-full bg-green-400 animate-pulse"></span>
<span class="text-[11px] font-bold">Servidores Online</span>
</div>
</div>
</div>
</div>
</main>
<!-- Footer -->
<footer class="fixed bottom-0 right-0 w-[calc(100%-260px)] h-8 bg-surface-container-lowest flex justify-between items-center px-xl z-40 border-t border-outline-variant/5">
<p class="font-label-muted text-label-muted text-on-surface-variant/60">v2.4.1 | Status do Backup: Sincronizado</p>
<div class="flex gap-lg">
<a class="font-label-muted text-label-muted text-on-surface-variant/60 hover:text-primary transition-colors" href="#">Suporte</a>
<a class="font-label-muted text-label-muted text-on-surface-variant/60 hover:text-primary transition-colors" href="#">Documentação</a>
</div>
</footer>
<!-- FAB for quick entry (Mobile intent) -->
<button class="fixed bottom-12 right-8 w-14 h-14 bg-primary text-on-primary rounded-full shadow-[0_8px_24px_rgba(59,130,246,0.5)] flex items-center justify-center z-50 hover:scale-110 active:scale-95 transition-all md:hidden">
<span class="material-symbols-outlined">add</span>
</button>
<script>
        // Micro-interaction for table rows
        document.querySelectorAll('tr').forEach(row => {
            row.addEventListener('click', () => {
                if(row.querySelector('td')) {
                    // Logic for opening detail view
                    console.log('Opening receipt details...');
                }
            });
        });

        // Search bar focus effect
        const searchInput = document.querySelector('input[type="text"]');
        searchInput.addEventListener('focus', () => {
            searchInput.parentElement.classList.add('neo-float');
            searchInput.parentElement.classList.add('ring-2', 'ring-primary/20');
        });
        searchInput.addEventListener('blur', () => {
            searchInput.parentElement.classList.remove('neo-float');
            searchInput.parentElement.classList.remove('ring-2', 'ring-primary/20');
        });
    </script>
</body></html>


<!DOCTYPE html>

<html class="dark" lang="pt-AO"><head>
<meta charset="utf-8"/>
<meta content="width=device-width, initial-scale=1.0" name="viewport"/>
<title>School Manager - Financeiro</title>
<script src="https://cdn.tailwindcss.com?plugins=forms,container-queries"></script>
<link href="https://fonts.googleapis.com/css2?family=Poppins:wght@400;500;600;700&amp;family=Inter:wght@400;600;700&amp;display=swap" rel="stylesheet"/>
<link href="https://fonts.googleapis.com/css2?family=Material+Symbols+Outlined:wght,FILL@100..700,0..1&amp;display=swap" rel="stylesheet"/>
<script id="tailwind-config">
        tailwind.config = {
            darkMode: "class",
            theme: {
                extend: {
                    "colors": {
                        "on-tertiary-container": "#002a51",
                        "on-primary-container": "#00285d",
                        "tertiary-container": "#4c93e7",
                        "on-tertiary-fixed-variant": "#004883",
                        "primary": "#adc6ff",
                        "secondary": "#bcc7de",
                        "inverse-on-surface": "#283044",
                        "surface-tint": "#adc6ff",
                        "surface-container-lowest": "#060e20",
                        "secondary-fixed": "#d8e3fb",
                        "on-error-container": "#ffdad6",
                        "on-primary-fixed-variant": "#004395",
                        "tertiary-fixed": "#d4e3ff",
                        "on-surface": "#dae2fd",
                        "surface-dim": "#0b1326",
                        "on-surface-variant": "#c2c6d6",
                        "surface-variant": "#2d3449",
                        "error-container": "#93000a",
                        "surface-bright": "#31394d",
                        "on-primary-fixed": "#001a42",
                        "on-secondary-fixed-variant": "#3c475a",
                        "surface-container-high": "#222a3d",
                        "surface": "#0b1326",
                        "primary-fixed": "#d8e2ff",
                        "on-tertiary-fixed": "#001c39",
                        "secondary-fixed-dim": "#bcc7de",
                        "tertiary-fixed-dim": "#a4c9ff",
                        "surface-container": "#171f33",
                        "on-tertiary": "#00315d",
                        "on-primary": "#002e6a",
                        "background": "#0b1326",
                        "tertiary": "#a4c9ff",
                        "on-secondary-container": "#aeb9d0",
                        "surface-container-low": "#131b2e",
                        "on-secondary-fixed": "#111c2d",
                        "on-secondary": "#263143",
                        "primary-fixed-dim": "#adc6ff",
                        "surface-container-highest": "#2d3449",
                        "inverse-surface": "#dae2fd",
                        "primary-container": "#4d8eff",
                        "error": "#ffb4ab",
                        "on-background": "#dae2fd",
                        "outline": "#8c909f",
                        "on-error": "#690005",
                        "outline-variant": "#424754",
                        "secondary-container": "#3e495d",
                        "inverse-primary": "#005ac2"
                    },
                    "borderRadius": {
                        "DEFAULT": "0.25rem",
                        "lg": "0.5rem",
                        "xl": "0.75rem",
                        "full": "9999px"
                    },
                    "spacing": {
                        "xs": "8px",
                        "xl": "32px",
                        "md": "16px",
                        "sm": "12px",
                        "gutter": "20px",
                        "sidebar_width": "260px",
                        "lg": "24px",
                        "base": "4px"
                    },
                    "fontFamily": {
                        "display-lg": ["Poppins"],
                        "data-numeric": ["Inter"],
                        "body-md": ["Inter"],
                        "body-sm": ["Inter"],
                        "body-lg": ["Inter"],
                        "headline-sm": ["Poppins"],
                        "label-muted": ["Inter"],
                        "display-md": ["Poppins"],
                        "label-bold": ["Inter"]
                    },
                    "fontSize": {
                        "display-lg": ["32px", {"lineHeight": "1.2", "letterSpacing": "-0.02em", "fontWeight": "600"}],
                        "data-numeric": ["20px", {"lineHeight": "1", "letterSpacing": "-0.01em", "fontWeight": "700"}],
                        "body-md": ["14px", {"lineHeight": "1.5", "fontWeight": "400"}],
                        "body-sm": ["13px", {"lineHeight": "1.5", "fontWeight": "400"}],
                        "body-lg": ["16px", {"lineHeight": "1.5", "fontWeight": "400"}],
                        "headline-sm": ["18px", {"lineHeight": "1.4", "fontWeight": "500"}],
                        "label-muted": ["12px", {"lineHeight": "1", "fontWeight": "400"}],
                        "display-md": ["24px", {"lineHeight": "1.3", "fontWeight": "600"}],
                        "label-bold": ["12px", {"lineHeight": "1", "letterSpacing": "0.05em", "fontWeight": "600"}]
                    }
                }
            }
        }
    </script>
<style>
        body {
            background-color: #0b1326;
            color: #dae2fd;
            -webkit-font-smoothing: antialiased;
        }
        .neo-inset {
            box-shadow: inset 2px 2px 5px rgba(0,0,0,0.4), inset -1px -1px 2px rgba(255,255,255,0.05);
        }
        .neo-glass {
            background: rgba(23, 31, 51, 0.7);
            backdrop-filter: blur(12px);
            border: 1px solid rgba(255,255,255,0.05);
            box-shadow: 0 10px 30px rgba(0,0,0,0.3);
        }
        .active-nav-glow {
            box-shadow: 0 4px 12px rgba(77, 142, 255, 0.3);
        }
        .scroll-hide::-webkit-scrollbar {
            display: none;
        }
        .material-symbols-outlined {
            font-variation-settings: 'FILL' 0, 'wght' 400, 'GRAD' 0, 'opsz' 24;
            vertical-align: middle;
        }
    </style>
</head>
<body class="font-body-md text-body-md overflow-hidden h-screen flex">
<!-- SideNavBar -->
<aside class="w-[260px] h-screen flex flex-col border-r border-outline-variant/10 bg-surface-container-low fixed left-0 top-0 p-md space-y-lg shadow-xl z-50">
<div class="flex flex-col gap-xs mb-lg px-xs">
<div class="flex items-center gap-sm">
<div class="w-10 h-10 rounded-lg bg-primary-container flex items-center justify-center shadow-lg">
<span class="material-symbols-outlined text-on-primary-container" style="font-variation-settings: 'FILL' 1;">school</span>
</div>
<div>
<h1 class="text-headline-sm font-display-lg text-on-surface tracking-tight">School Manager</h1>
<p class="font-label-muted text-label-muted">Gestão Escolar</p>
</div>
</div>
</div>
<nav class="flex-1 space-y-base overflow-y-auto scroll-hide">
<a class="flex items-center gap-sm px-md py-sm text-on-surface-variant hover:text-on-surface hover:bg-surface-variant/50 transition-all duration-200 rounded-lg group" href="../dashboard_school_manager_refatorado/code.html">
<span class="material-symbols-outlined">dashboard</span>
<span class="font-label-bold">Dashboard</span>
</a>
<a class="flex items-center gap-sm px-md py-sm text-on-surface-variant hover:text-on-surface hover:bg-surface-variant/50 transition-all duration-200 rounded-lg" href="../gest_o_de_alunos_school_manager/code.html">
<span class="material-symbols-outlined">group</span>
<span class="font-label-bold">Alunos</span>
</a>
<a class="flex items-center gap-sm px-md py-sm bg-primary-container text-on-primary-container rounded-lg font-label-bold active-nav-glow scale-[0.98]" href="code.html">
<span class="material-symbols-outlined" style="font-variation-settings: 'FILL' 1;">payments</span>
<span>Financeiro</span>
</a>
<a class="flex items-center gap-sm px-md py-sm text-on-surface-variant hover:text-on-surface hover:bg-surface-variant/50 transition-all duration-200 rounded-lg" href="../relat_rios_administrativos_school_manager/code.html">
<span class="material-symbols-outlined">analytics</span>
<span class="font-label-bold">Relatórios</span>
</a>
<a class="flex items-center gap-sm px-md py-sm text-on-surface-variant hover:text-on-surface hover:bg-surface-variant/50 transition-all duration-200 rounded-lg" href="../escola_classes_school_manager/code.html">
<span class="material-symbols-outlined">account_balance</span>
<span class="font-label-bold">Escola</span>
</a>
<a class="flex items-center gap-sm px-md py-sm text-on-surface-variant hover:text-on-surface hover:bg-surface-variant/50 transition-all duration-200 rounded-lg" href="../configura_es_do_sistema_school_manager/code.html">
<span class="material-symbols-outlined">settings</span>
<span class="font-label-bold">Configurações</span>
</a>
</nav>
<div class="pt-md border-t border-outline-variant/10 space-y-xs">
<div class="flex items-center gap-sm p-sm rounded-xl bg-surface-container-high neo-inset mb-sm">
<img alt="Secretaria" class="w-10 h-10 rounded-full object-cover border border-primary/20" src="https://lh3.googleusercontent.com/aida-public/AB6AXuAEewJOnS6IOMgyE-GURbmc2zCHYOYX0WSnMudqMCZ4JSlvr44dJPVdMIW04Dqki_c077lXCSfS4yFJn2_Ozc_a2fSD6JygryojI1ulBC71-UjTrNhTlL3VviyMFdoHhMncbh0rP246MdTKTblbNeTjyKoFAsWGjF-0NUwB2FjdQCBrJpHxF8rkcfXacvRVyD1OKb6JiRG7ViW_DJ7B6c2VKXcHT5CP12lB8Csq-O1w3G9rOqJbU171MbF7QI9lxSjDmct_CYLnbXgu"/>
<div class="flex flex-col">
<span class="font-label-bold text-on-surface">Secretaria</span>
<span class="text-label-muted text-[10px] uppercase tracking-widest text-primary">Administrador</span>
</div>
</div>
<a class="flex items-center gap-sm px-md py-sm text-on-surface-variant hover:text-error transition-colors duration-200" href="#">
<span class="material-symbols-outlined">logout</span>
<span class="font-label-bold">Sair do Sistema</span>
</a>
</div>
</aside>
<!-- Main Content Canvas -->
<main class="flex-1 ml-[260px] h-screen flex flex-col bg-surface-dim relative overflow-hidden">
<!-- TopAppBar -->
<header class="w-full h-auto flex flex-row justify-between items-center py-lg px-xl bg-transparent relative z-10">
<div class="flex flex-col">
<h2 class="font-display-md text-display-md text-on-surface">Módulo Financeiro</h2>
<p class="font-body-sm text-label-muted">Gestão de receitas, despesas e fluxo de caixa</p>
</div>
<div class="flex items-center gap-md">
<div class="relative w-96">
<div class="absolute left-sm top-1/2 -translate-y-1/2">
<span class="material-symbols-outlined text-on-surface-variant">search</span>
</div>
<input class="w-full bg-surface-container-low border border-outline-variant/30 rounded-full py-sm pl-xl pr-md text-body-sm text-on-surface focus:outline-none focus:ring-2 focus:ring-primary/50 transition-all neo-inset" placeholder="Pesquisar registros..." type="text"/>
</div>
<div class="flex items-center gap-xs">
<button class="hover:bg-surface-container-high rounded-full p-xs transition-colors text-on-surface-variant">
<span class="material-symbols-outlined">calendar_today</span>
</button>
<button class="hover:bg-surface-container-high rounded-full p-xs transition-colors text-on-surface-variant relative">
<span class="material-symbols-outlined">notifications</span>
<span class="absolute top-0 right-0 w-2 h-2 bg-primary rounded-full"></span>
</button>
</div>
</div>
</header>
<!-- Sub-navigation Tabs -->
<div class="px-xl mb-lg">
<div class="flex gap-md border-b border-outline-variant/10">
<button class="px-md py-sm border-b-2 border-primary text-primary font-label-bold transition-all">Pagamentos</button>
<button class="px-md py-sm text-on-surface-variant hover:text-on-surface font-label-bold transition-all">Entradas</button>
<button class="px-md py-sm text-on-surface-variant hover:text-on-surface font-label-bold transition-all">Saídas</button>
<button class="px-md py-sm text-on-surface-variant hover:text-on-surface font-label-bold transition-all">Caixa</button>
</div>
</div>
<!-- Dynamic Content -->
<section class="flex-1 p-xl pt-0 overflow-y-auto space-y-lg scroll-hide">
<div class="grid grid-cols-12 gap-lg h-full">
<!-- Left Column: Student Info & Payment Table (8 cols) -->
<div class="col-span-8 flex flex-col gap-lg">
<!-- Student Highlights Card -->
<div class="neo-glass rounded-xl p-lg flex items-center justify-between">
<div class="flex items-center gap-lg">
<div class="w-24 h-24 rounded-2xl overflow-hidden border-2 border-primary/20 shadow-xl">
<img alt="Student Portrait" class="w-full h-full object-cover" src="https://lh3.googleusercontent.com/aida-public/AB6AXuAjhn-VJy7Q0ZdQd5kKcXo0272BZlVEfhUuhIzzVXdFHCS1P8p1Z4WBsmS6MDZzsdytVUkBJHTTWD3FRbIVarNXWzvHviKrvc_JLP-1VY99Yo7qYxKR04sULVD0LsmsrAcqjXzNlwl0gYUrmsRyFBTD4DrAbSL3FCXxEYds9qzpR2SYimarrCxikO510pGeuQX2yUjiH4ngEIbmCEV1ybekGDcs_Gj-P3w2u0LSxaZb12PSAMVdqZbRtjD5Jx8RY9XRdbrBABqq0OOa"/>
</div>
<div>
<h3 class="font-display-md text-display-md text-on-surface">João Pedro da Silva</h3>
<div class="flex gap-md mt-xs">
<span class="px-sm py-1 bg-surface-variant rounded-full text-label-muted font-label-bold">ID: #SM-2024-089</span>
<span class="px-sm py-1 bg-primary/10 text-primary rounded-full text-label-muted font-label-bold">8ª Classe - Turma A</span>
</div>
</div>
</div>
<!-- Dívida Atual Highlight -->
<div class="bg-surface-container-highest neo-inset rounded-xl p-md text-right border border-outline-variant/10">
<p class="text-label-muted font-label-bold uppercase tracking-widest text-[10px]">Dívida Atual</p>
<h4 class="text-display-md text-error font-data-numeric">120.000 Kz</h4>
<p class="text-[10px] text-on-surface-variant mt-xs">Referente a 3 meses em atraso</p>
</div>
</div>
<!-- Payment Status Table -->
<div class="neo-glass rounded-xl flex-1 flex flex-col overflow-hidden">
<div class="p-md border-b border-outline-variant/10 flex justify-between items-center bg-surface-container/30">
<h5 class="font-label-bold text-on-surface">Histórico de Mensalidades - Ano Lectivo 2024</h5>
<div class="flex gap-xs">
<span class="flex items-center gap-1 text-[10px] font-label-bold text-tertiary"><span class="w-2 h-2 rounded-full bg-tertiary"></span> PAGO</span>
<span class="flex items-center gap-1 text-[10px] font-label-bold text-error"><span class="w-2 h-2 rounded-full bg-error"></span> EM ATRASO</span>
</div>
</div>
<div class="flex-1 overflow-y-auto">
<table class="w-full text-left border-collapse">
<thead class="sticky top-0 bg-surface-container-low text-label-muted uppercase text-[10px] tracking-widest">
<tr>
<th class="px-lg py-md font-semibold">Mês Referente</th>
<th class="px-lg py-md font-semibold text-center">Data Vencimento</th>
<th class="px-lg py-md font-semibold text-center">Status</th>
<th class="px-lg py-md font-semibold text-right">Valor</th>
<th class="px-lg py-md font-semibold text-center">Ações</th>
</tr>
</thead>
<tbody class="divide-y divide-outline-variant/10">
<tr class="hover:bg-surface-variant/30 transition-colors group">
<td class="px-lg py-md font-label-bold text-on-surface">Janeiro / 2024</td>
<td class="px-lg py-md text-center text-label-muted">05 Jan 2024</td>
<td class="px-lg py-md text-center">
<span class="bg-tertiary/10 text-tertiary text-[10px] font-label-bold px-sm py-1 rounded-full border border-tertiary/20">Liquidado</span>
</td>
<td class="px-lg py-md text-right font-data-numeric text-on-surface">40.000 Kz</td>
<td class="px-lg py-md text-center">
<button class="material-symbols-outlined text-on-surface-variant hover:text-primary transition-colors">print</button>
</td>
</tr>
<tr class="hover:bg-surface-variant/30 transition-colors group">
<td class="px-lg py-md font-label-bold text-on-surface">Fevereiro / 2024</td>
<td class="px-lg py-md text-center text-label-muted">05 Fev 2024</td>
<td class="px-lg py-md text-center">
<span class="bg-tertiary/10 text-tertiary text-[10px] font-label-bold px-sm py-1 rounded-full border border-tertiary/20">Liquidado</span>
</td>
<td class="px-lg py-md text-right font-data-numeric text-on-surface">40.000 Kz</td>
<td class="px-lg py-md text-center">
<button class="material-symbols-outlined text-on-surface-variant hover:text-primary transition-colors">print</button>
</td>
</tr>
<tr class="hover:bg-surface-variant/30 transition-colors group">
<td class="px-lg py-md font-label-bold text-on-surface">Março / 2024</td>
<td class="px-lg py-md text-center text-label-muted">05 Mar 2024</td>
<td class="px-lg py-md text-center">
<span class="bg-error/10 text-error text-[10px] font-label-bold px-sm py-1 rounded-full border border-error/20">Em Atraso</span>
</td>
<td class="px-lg py-md text-right font-data-numeric text-error">40.000 Kz</td>
<td class="px-lg py-md text-center">
<button class="material-symbols-outlined text-on-surface-variant hover:text-primary transition-colors">info</button>
</td>
</tr>
<tr class="hover:bg-surface-variant/30 transition-colors group">
<td class="px-lg py-md font-label-bold text-on-surface">Abril / 2024</td>
<td class="px-lg py-md text-center text-label-muted">05 Abr 2024</td>
<td class="px-lg py-md text-center">
<span class="bg-error/10 text-error text-[10px] font-label-bold px-sm py-1 rounded-full border border-error/20">Em Atraso</span>
</td>
<td class="px-lg py-md text-right font-data-numeric text-error">40.000 Kz</td>
<td class="px-lg py-md text-center">
<button class="material-symbols-outlined text-on-surface-variant hover:text-primary transition-colors">info</button>
</td>
</tr>
<tr class="hover:bg-surface-variant/30 transition-colors group">
<td class="px-lg py-md font-label-bold text-on-surface">Maio / 2024</td>
<td class="px-lg py-md text-center text-label-muted">05 Mai 2024</td>
<td class="px-lg py-md text-center">
<span class="bg-error/10 text-error text-[10px] font-label-bold px-sm py-1 rounded-full border border-error/20">Em Atraso</span>
</td>
<td class="px-lg py-md text-right font-data-numeric text-error">40.000 Kz</td>
<td class="px-lg py-md text-center">
<button class="material-symbols-outlined text-on-surface-variant hover:text-primary transition-colors">info</button>
</td>
</tr>
</tbody>
</table>
</div>
</div>
</div>
<!-- Right Column: Payment Action Area (4 cols) -->
<div class="col-span-4 flex flex-col gap-lg">
<div class="neo-glass rounded-xl p-lg flex flex-col gap-md sticky top-0">
<div class="flex items-center gap-sm border-b border-outline-variant/10 pb-md">
<span class="material-symbols-outlined text-primary" style="font-variation-settings: 'FILL' 1;">add_card</span>
<h5 class="font-display-md text-headline-sm text-on-surface">Efectuar Pagamento</h5>
</div>
<div class="space-y-md mt-sm">
<!-- Input Field: Selecionar Mês -->
<div class="space-y-xs">
<label class="text-[10px] font-label-bold text-label-muted uppercase tracking-widest">Selecionar Meses</label>
<div class="bg-surface-container-low neo-inset rounded-lg p-sm border border-outline-variant/30 flex items-center justify-between">
<span class="text-body-sm text-on-surface-variant">Março, Abril, Maio</span>
<span class="material-symbols-outlined text-on-surface-variant">expand_more</span>
</div>
</div>
<!-- Input Field: Valor a Receber -->
<div class="space-y-xs">
<label class="text-[10px] font-label-bold text-label-muted uppercase tracking-widest">Valor a Receber (Kz)</label>
<div class="bg-surface-container-lowest neo-inset rounded-lg border border-primary/30 p-md flex items-baseline justify-between">
<span class="text-primary font-bold text-lg">Kz</span>
<input class="bg-transparent border-none text-right text-display-md font-data-numeric focus:ring-0 w-full text-on-surface" type="text" value="120.000"/>
</div>
</div>
<!-- Input Field: Forma de Pagamento -->
<div class="space-y-xs">
<label class="text-[10px] font-label-bold text-label-muted uppercase tracking-widest">Forma de Pagamento</label>
<div class="grid grid-cols-2 gap-sm">
<button class="flex flex-col items-center gap-xs p-md rounded-xl bg-primary-container text-on-primary-container active-nav-glow transition-all">
<span class="material-symbols-outlined">payments</span>
<span class="text-[10px] font-label-bold uppercase">Numerário</span>
</button>
<button class="flex flex-col items-center gap-xs p-md rounded-xl bg-surface-container-high text-on-surface-variant hover:bg-surface-variant transition-all border border-outline-variant/20">
<span class="material-symbols-outlined">credit_card</span>
<span class="text-[10px] font-label-bold uppercase">TPA / Transfer</span>
</button>
</div>
</div>
<!-- Payment Summary Details -->
<div class="p-md rounded-lg bg-surface-container-low/50 border border-outline-variant/5 space-y-sm mt-md">
<div class="flex justify-between text-body-sm">
<span class="text-label-muted">Subtotal:</span>
<span class="text-on-surface font-data-numeric">120.000 Kz</span>
</div>
<div class="flex justify-between text-body-sm">
<span class="text-label-muted">Multas (0%):</span>
<span class="text-on-surface font-data-numeric">0 Kz</span>
</div>
<div class="flex justify-between border-t border-outline-variant/10 pt-sm">
<span class="font-label-bold text-on-surface">TOTAL:</span>
<span class="text-primary font-data-numeric text-lg">120.000 Kz</span>
</div>
</div>
<!-- Confirm Action Button -->
<button class="w-full py-md rounded-xl bg-gradient-to-r from-[#3B82F6] to-[#2563EB] text-white font-label-bold uppercase tracking-widest shadow-[0_4px_12px_rgba(59,130,246,0.3)] hover:scale-[1.02] active:scale-[0.98] transition-all flex items-center justify-center gap-sm mt-md">
<span class="material-symbols-outlined">check_circle</span>
                            Confirmar Pagamento
                        </button>
</div>
</div>
<!-- Receipt Quick Link Card -->
<div class="neo-glass rounded-xl p-md flex items-center gap-md hover:bg-primary/5 transition-colors cursor-pointer group">
<div class="p-sm rounded-lg bg-surface-container-highest neo-inset text-tertiary">
<span class="material-symbols-outlined">print</span>
</div>
<div class="flex-1">
<p class="font-label-bold text-on-surface group-hover:text-primary transition-colors">Visualizar Último Recibo</p>
<p class="text-[10px] text-label-muted uppercase">Recibo Nº 4521/2024</p>
</div>
<span class="material-symbols-outlined text-label-muted">chevron_right</span>
</div>
</div>
</div>
</section>
<!-- Footer -->
<footer class="flex justify-between items-center px-xl w-full py-md mt-auto border-t border-outline-variant/10 bg-transparent">
<span class="font-label-muted text-label-muted">School Manager v1.0.0 | Todos os direitos reservados</span>
<div class="flex items-center gap-md">
<span class="flex items-center gap-xs font-label-muted text-label-muted hover:text-on-surface transition-colors cursor-pointer">
<span class="w-1.5 h-1.5 rounded-full bg-tertiary animate-pulse"></span>
                Backup: OK
            </span>
<span class="font-label-muted text-label-muted">Angola, 2024</span>
</div>
</footer>
<!-- Atmospheric Elements -->
<div class="absolute top-[-10%] right-[-10%] w-[40%] h-[40%] bg-primary/5 rounded-full blur-[120px] pointer-events-none"></div>
<div class="absolute bottom-[-10%] left-[-10%] w-[30%] h-[30%] bg-tertiary/5 rounded-full blur-[100px] pointer-events-none"></div>
</main>
<script>
    // Simple Interaction logic
    document.querySelectorAll('button, a').forEach(el => {
        el.addEventListener('mousedown', () => {
            el.style.transform = 'scale(0.96)';
        });
        el.addEventListener('mouseup', () => {
            el.style.transform = '';
        });
        el.addEventListener('mouseleave', () => {
            el.style.transform = '';
        });
    });

    // Search Bar Focus Effect
    const searchInput = document.querySelector('input[type="text"]');
    if (searchInput) {
        searchInput.addEventListener('focus', () => {
            searchInput.parentElement.classList.add('ring-2', 'ring-primary/40');
        });
        searchInput.addEventListener('blur', () => {
            searchInput.parentElement.classList.remove('ring-2', 'ring-primary/40');
        });
    }
</script>
</body></html>






<!DOCTYPE html>

<html class="dark" lang="pt-ao"><head>
<meta charset="utf-8"/>
<meta content="width=device-width, initial-scale=1.0" name="viewport"/>
<title>Módulo Financeiro - Aba Saídas | Gestão Escolar</title>
<script src="https://cdn.tailwindcss.com?plugins=forms,container-queries"></script>
<link href="https://fonts.googleapis.com/css2?family=Inter:wght@400;500;600;700&amp;family=Poppins:wght@500;600;700&amp;display=swap" rel="stylesheet"/>
<link href="https://fonts.googleapis.com/css2?family=Material+Symbols+Outlined:wght,FILL@100..700,0..1&amp;display=swap" rel="stylesheet"/>
<link href="https://fonts.googleapis.com/css2?family=Material+Symbols+Outlined:wght,FILL@100..700,0..1&amp;display=swap" rel="stylesheet"/>
<script id="tailwind-config">
      tailwind.config = {
        darkMode: "class",
        theme: {
          extend: {
            "colors": {
                    "on-secondary": "#263143",
                    "on-tertiary-fixed": "#001c39",
                    "error": "#ffb4ab",
                    "surface-container": "#171f33",
                    "error-container": "#93000a",
                    "on-surface-variant": "#c2c6d6",
                    "surface-bright": "#31394d",
                    "surface-tint": "#adc6ff",
                    "on-tertiary-container": "#002a51",
                    "on-primary-fixed-variant": "#004395",
                    "tertiary-fixed": "#d4e3ff",
                    "surface-dim": "#0b1326",
                    "outline": "#8c909f",
                    "secondary": "#bcc7de",
                    "on-tertiary": "#00315d",
                    "surface-container-low": "#131b2e",
                    "outline-variant": "#424754",
                    "secondary-fixed-dim": "#bcc7de",
                    "on-primary-container": "#00285d",
                    "on-background": "#dae2fd",
                    "surface-container-lowest": "#060e20",
                    "surface": "#0b1326",
                    "tertiary-fixed-dim": "#a4c9ff",
                    "on-primary": "#002e6a",
                    "primary-container": "#4d8eff",
                    "inverse-surface": "#dae2fd",
                    "surface-container-high": "#222a3d",
                    "on-error-container": "#ffdad6",
                    "on-secondary-fixed-variant": "#3c475a",
                    "background": "#0b1326",
                    "on-tertiary-fixed-variant": "#004883",
                    "on-error": "#690005",
                    "secondary-fixed": "#d8e3fb",
                    "primary-fixed": "#d8e2ff",
                    "on-primary-fixed": "#001a42",
                    "tertiary-container": "#4c93e7",
                    "primary": "#adc6ff",
                    "tertiary": "#a4c9ff",
                    "surface-container-highest": "#2d3449",
                    "surface-variant": "#2d3449",
                    "on-surface": "#dae2fd",
                    "secondary-container": "#3e495d",
                    "on-secondary-container": "#aeb9d0",
                    "inverse-primary": "#005ac2",
                    "primary-fixed-dim": "#adc6ff",
                    "on-secondary-fixed": "#111c2d",
                    "inverse-on-surface": "#283044"
            },
            "borderRadius": {
                    "DEFAULT": "0.25rem",
                    "lg": "0.5rem",
                    "xl": "0.75rem",
                    "full": "9999px"
            },
            "spacing": {
                    "sidebar_width": "260px",
                    "lg": "24px",
                    "base": "4px",
                    "sm": "12px",
                    "xl": "32px",
                    "md": "16px",
                    "xs": "8px",
                    "gutter": "20px"
            },
            "fontFamily": {
                    "body-lg": ["Inter"],
                    "label-muted": ["Inter"],
                    "display-lg": ["Poppins"],
                    "label-bold": ["Inter"],
                    "data-numeric": ["Inter"],
                    "headline-sm": ["Poppins"],
                    "body-md": ["Inter"],
                    "body-sm": ["Inter"],
                    "display-md": ["Poppins"]
            },
            "fontSize": {
                    "body-lg": ["16px", {"lineHeight": "1.5", "fontWeight": "400"}],
                    "label-muted": ["12px", {"lineHeight": "1", "fontWeight": "400"}],
                    "display-lg": ["32px", {"lineHeight": "1.2", "letterSpacing": "-0.02em", "fontWeight": "600"}],
                    "label-bold": ["12px", {"lineHeight": "1", "letterSpacing": "0.05em", "fontWeight": "600"}],
                    "data-numeric": ["20px", {"lineHeight": "1", "letterSpacing": "-0.01em", "fontWeight": "700"}],
                    "headline-sm": ["18px", {"lineHeight": "1.4", "fontWeight": "500"}],
                    "body-md": ["14px", {"lineHeight": "1.5", "fontWeight": "400"}],
                    "body-sm": ["13px", {"lineHeight": "1.5", "fontWeight": "400"}],
                    "display-md": ["24px", {"lineHeight": "1.3", "fontWeight": "600"}]
            }
          },
        },
      }
    </script>
<style>
        .neo-depth {
            box-shadow: 20px 0 40px rgba(0,0,0,0.4);
        }
        .neo-card {
            background: #171f33;
            border-top: 1px solid rgba(255, 255, 255, 0.05);
            border-left: 1px solid rgba(255, 255, 255, 0.05);
            box-shadow: 0 10px 30px rgba(0, 0, 0, 0.3);
        }
        .neo-input {
            background: #0b1326;
            border: 1px solid #334155;
            box-shadow: inset 0 2px 4px rgba(0,0,0,0.5);
        }
        .neo-button-primary {
            background: linear-gradient(135deg, #3B82F6 0%, #2563EB 100%);
            box-shadow: 0 4px 12px rgba(59, 130, 246, 0.3);
        }
        .neo-button-primary:active {
            transform: scale(0.96);
            box-shadow: inset 0 2px 4px rgba(0,0,0,0.3);
        }
        .glass-trend {
            background: rgba(255, 255, 255, 0.03);
            backdrop-filter: blur(8px);
            border: 1px solid rgba(255, 255, 255, 0.1);
        }
        .custom-scrollbar::-webkit-scrollbar {
            width: 6px;
        }
        .custom-scrollbar::-webkit-scrollbar-track {
            background: #0b1326;
        }
        .custom-scrollbar::-webkit-scrollbar-thumb {
            background: #334155;
            border-radius: 10px;
        }
    </style>
</head>
<body class="bg-background text-on-surface font-body-md overflow-hidden selection:bg-primary selection:text-on-primary">
<!-- Desktop Sidebar -->
<aside class="fixed left-0 top-0 h-full w-[260px] bg-surface-container-low flex flex-col py-lg neo-depth z-50">
<div class="px-xl mb-xl">
<h1 class="font-display-md text-display-md font-bold text-primary">Gestão Escolar</h1>
<p class="font-body-md text-on-surface-variant opacity-70">Administração Central</p>
</div>
<nav class="flex-1 flex flex-col space-y-1">
<a class="flex items-center px-xl py-md text-on-surface-variant hover:bg-surface-container-high transition-all" href="../dashboard_school_manager_refatorado/code.html">
<span class="material-symbols-outlined mr-md">dashboard</span>
<span class="font-body-md">Dashboard</span>
</a>
<a class="flex items-center px-xl py-md text-on-surface-variant hover:bg-surface-container-high transition-all" href="../gest_o_de_alunos_school_manager/code.html">
<span class="material-symbols-outlined mr-md">group</span>
<span class="font-body-md">Alunos</span>
</a>
<a class="flex items-center px-xl py-md bg-primary text-on-primary rounded-r-full shadow-[0_4px_12px_rgba(59,130,246,0.3)]" href="../financeiro_pagamentos_school_manager/code.html">
<span class="material-symbols-outlined mr-md" style="font-variation-settings: 'FILL' 1;">account_balance_wallet</span>
<span class="font-body-md">Financeiro</span>
</a>
<a class="flex items-center px-xl py-md text-on-surface-variant hover:bg-surface-container-high transition-all" href="../relat_rios_administrativos_school_manager/code.html">
<span class="material-symbols-outlined mr-md">analytics</span>
<span class="font-body-md">Relatórios</span>
</a>
<a class="flex items-center px-xl py-md text-on-surface-variant hover:bg-surface-container-high transition-all" href="../escola_classes_school_manager/code.html">
<span class="material-symbols-outlined mr-md">school</span>
<span class="font-body-md">Escola</span>
</a>
<a class="flex items-center px-xl py-md text-on-surface-variant hover:bg-surface-container-high transition-all" href="../configura_es_do_sistema_school_manager/code.html">
<span class="material-symbols-outlined mr-md">settings</span>
<span class="font-body-md">Configurações</span>
</a>
</nav>
<div class="px-xl mt-auto pt-md">
<div class="flex items-center space-x-sm p-sm rounded-lg bg-surface-container-highest">
<div class="w-10 h-10 rounded-full overflow-hidden bg-primary-container flex items-center justify-center text-on-primary-container font-bold">
                    AD
                </div>
<div class="flex-1">
<p class="font-body-sm font-bold truncate">Admin Escola</p>
<p class="font-label-muted text-on-surface-variant">Diretor Financeiro</p>
</div>
</div>
</div>
</aside>
<!-- Top App Bar -->
<header class="fixed top-0 right-0 w-[calc(100%-260px)] h-16 bg-surface flex justify-between items-center px-xl border-b border-outline-variant/20 z-40 shadow-sm">
<div class="flex items-center gap-md">
<span class="material-symbols-outlined text-on-surface-variant cursor-pointer">menu_open</span>
<div class="flex items-center text-primary font-bold text-headline-sm">
                Financeiro <span class="mx-xs text-on-surface-variant opacity-30">/</span> <span class="text-on-surface font-medium">Saídas</span>
</div>
</div>
<div class="flex items-center gap-xl">
<div class="relative group">
<div class="flex items-center neo-input px-md py-xs rounded-full w-64 transition-all focus-within:ring-2 focus-within:ring-primary focus-within:w-80">
<span class="material-symbols-outlined text-on-surface-variant text-md mr-xs">search</span>
<input class="bg-transparent border-none outline-none text-body-sm text-on-surface w-full placeholder:text-on-surface-variant/40" placeholder="Pesquisar despesa..." type="text"/>
</div>
</div>
<div class="flex items-center gap-md">
<button class="material-symbols-outlined p-sm text-on-surface-variant hover:bg-surface-container-highest rounded-full transition-all relative">
                    notifications
                    <span class="absolute top-2 right-2 w-2 h-2 bg-error rounded-full"></span>
</button>
<button class="material-symbols-outlined p-sm text-on-surface-variant hover:bg-surface-container-highest rounded-full transition-all">account_circle</button>
</div>
</div>
</header>
<!-- Main Content -->
<main class="fixed inset-0 left-[260px] top-16 bg-background p-xl overflow-y-auto custom-scrollbar">
<!-- Action Header -->
<div class="flex flex-col md:flex-row md:items-center justify-between mb-xl gap-lg">
<div>
<h2 class="font-display-lg text-display-lg text-on-surface">Gestão de Saídas</h2>
<p class="text-on-surface-variant font-body-md">Controlo de despesas operacionais e folha de pagamentos.</p>
</div>
<button class="neo-button-primary px-lg py-md rounded-xl flex items-center gap-sm text-white font-bold transition-all group">
<span class="material-symbols-outlined group-hover:rotate-90 transition-transform">add_circle</span>
                Nova Saída
            </button>
</div>
<!-- KPI Bento Grid -->
<div class="grid grid-cols-1 md:grid-cols-12 gap-lg mb-xl">
<div class="md:col-span-4 neo-card rounded-2xl p-lg relative overflow-hidden group">
<div class="absolute top-0 right-0 p-lg opacity-10">
<span class="material-symbols-outlined text-[64px]" style="font-variation-settings: 'FILL' 1;">payments</span>
</div>
<p class="font-label-bold text-on-surface-variant uppercase mb-xs">Total Saídas Hoje</p>
<div class="flex items-baseline gap-xs mb-md">
<span class="text-on-surface-variant font-body-lg">Kz</span>
<span class="font-display-lg text-display-lg text-error">425.000,00</span>
</div>
<div class="glass-trend px-sm py-xs rounded-lg inline-flex items-center gap-xs">
<span class="material-symbols-outlined text-error text-md">trending_up</span>
<span class="text-error font-body-sm font-bold">+12% vs Ontem</span>
</div>
</div>
<div class="md:col-span-4 neo-card rounded-2xl p-lg relative overflow-hidden">
<div class="absolute top-0 right-0 p-lg opacity-10">
<span class="material-symbols-outlined text-[64px]" style="font-variation-settings: 'FILL' 1;">calendar_month</span>
</div>
<p class="font-label-bold text-on-surface-variant uppercase mb-xs">Saídas do Mês</p>
<div class="flex items-baseline gap-xs mb-md">
<span class="text-on-surface-variant font-body-lg">Kz</span>
<span class="font-display-lg text-display-lg text-primary">2.840.150,00</span>
</div>
<div class="glass-trend px-sm py-xs rounded-lg inline-flex items-center gap-xs">
<span class="material-symbols-outlined text-primary text-md">trending_down</span>
<span class="text-primary font-body-sm font-bold">-5% vs Mês Anterior</span>
</div>
</div>
<div class="md:col-span-4 neo-card rounded-2xl p-lg bg-surface-container-high border-primary/20">
<p class="font-label-bold text-on-surface-variant uppercase mb-sm">Próximos Vencimentos</p>
<div class="space-y-sm">
<div class="flex justify-between items-center bg-surface-container-lowest/50 p-sm rounded-lg">
<div class="flex items-center gap-sm">
<span class="material-symbols-outlined text-secondary">flash_on</span>
<span class="font-body-sm">Conta de Luz (ENDE)</span>
</div>
<span class="font-label-bold text-error">Em 2 dias</span>
</div>
<div class="flex justify-between items-center bg-surface-container-lowest/50 p-sm rounded-lg">
<div class="flex items-center gap-sm">
<span class="material-symbols-outlined text-secondary">water_drop</span>
<span class="font-body-sm">Conta de Água (EPAL)</span>
</div>
<span class="font-label-bold text-on-surface-variant">Em 5 dias</span>
</div>
</div>
</div>
</div>
<!-- Filters Section -->
<div class="neo-card rounded-2xl p-lg mb-lg flex flex-col md:flex-row items-center gap-lg">
<div class="w-full md:w-auto flex-1">
<label class="font-label-muted text-on-surface-variant block mb-xs ml-base">Categoria</label>
<div class="relative">
<select class="w-full neo-input rounded-xl px-md py-md text-on-surface appearance-none focus:outline-none cursor-pointer">
<option>Todas as Categorias</option>
<option>Salários</option>
<option>Água e Luz</option>
<option>Manutenção</option>
<option>Materiais Pedagógicos</option>
<option>Impostos</option>
</select>
<span class="material-symbols-outlined absolute right-md top-1/2 -translate-y-1/2 pointer-events-none text-on-surface-variant">expand_more</span>
</div>
</div>
<div class="w-full md:w-auto flex-1">
<label class="font-label-muted text-on-surface-variant block mb-xs ml-base">Fornecedor</label>
<div class="relative">
<select class="w-full neo-input rounded-xl px-md py-md text-on-surface appearance-none focus:outline-none cursor-pointer">
<option>Todos os Fornecedores</option>
<option>ENDE - Empresa Nacional</option>
<option>EPAL - Águas de Luanda</option>
<option>Limpezas Lda</option>
<option>Papelaria Central</option>
</select>
<span class="material-symbols-outlined absolute right-md top-1/2 -translate-y-1/2 pointer-events-none text-on-surface-variant">expand_more</span>
</div>
</div>
<div class="w-full md:w-48">
<label class="font-label-muted text-on-surface-variant block mb-xs ml-base">Período</label>
<div class="relative">
<button class="w-full neo-input rounded-xl px-md py-md text-on-surface flex items-center justify-between">
<span class="text-body-sm">Últimos 30 dias</span>
<span class="material-symbols-outlined text-on-surface-variant text-md">calendar_today</span>
</button>
</div>
</div>
<button class="w-full md:w-auto self-end h-[50px] px-lg rounded-xl border border-outline-variant hover:bg-surface-container-high transition-all flex items-center justify-center gap-sm">
<span class="material-symbols-outlined">filter_list</span>
                Aplicar
            </button>
</div>
<!-- Data Grid -->
<div class="neo-card rounded-2xl overflow-hidden mb-xl">
<div class="overflow-x-auto">
<table class="w-full border-collapse">
<thead>
<tr class="bg-surface-container-high/50 border-b border-outline-variant/30">
<th class="px-lg py-md text-left font-label-bold text-on-surface-variant uppercase tracking-wider">Data</th>
<th class="px-lg py-md text-left font-label-bold text-on-surface-variant uppercase tracking-wider">Descrição / Fornecedor</th>
<th class="px-lg py-md text-left font-label-bold text-on-surface-variant uppercase tracking-wider">Categoria</th>
<th class="px-lg py-md text-left font-label-bold text-on-surface-variant uppercase tracking-wider">Status</th>
<th class="px-lg py-md text-right font-label-bold text-on-surface-variant uppercase tracking-wider">Valor (Kz)</th>
<th class="px-lg py-md text-center font-label-bold text-on-surface-variant uppercase tracking-wider">Ações</th>
</tr>
</thead>
<tbody class="divide-y divide-outline-variant/20">
<!-- Item 1 -->
<tr class="hover:bg-surface-container-high/40 transition-colors group">
<td class="px-lg py-md">
<p class="text-on-surface font-medium">12 Out 2023</p>
<p class="text-label-muted text-on-surface-variant">14:30</p>
</td>
<td class="px-lg py-md">
<div class="flex items-center gap-md">
<div class="w-10 h-10 rounded-lg bg-surface-container-highest flex items-center justify-center text-primary">
<span class="material-symbols-outlined" style="font-variation-settings: 'FILL' 1;">group</span>
</div>
<div>
<p class="text-on-surface font-bold">Folha de Salários - Outubro</p>
<p class="text-label-muted text-on-surface-variant">Pagamento Mensal Staff</p>
</div>
</div>
</td>
<td class="px-lg py-md">
<span class="px-sm py-xs bg-secondary-container text-on-secondary-container rounded-full text-body-sm font-medium">Salários</span>
</td>
<td class="px-lg py-md">
<div class="flex items-center gap-xs text-primary font-medium">
<span class="w-2 h-2 rounded-full bg-primary animate-pulse"></span>
                                    Liquidado
                                </div>
</td>
<td class="px-lg py-md text-right">
<span class="font-data-numeric text-data-numeric text-on-surface">1.250.000,00</span>
</td>
<td class="px-lg py-md text-center">
<div class="flex justify-center gap-xs opacity-0 group-hover:opacity-100 transition-opacity">
<button class="p-xs hover:text-primary transition-colors"><span class="material-symbols-outlined">visibility</span></button>
<button class="p-xs hover:text-primary transition-colors"><span class="material-symbols-outlined">print</span></button>
<button class="p-xs hover:text-error transition-colors"><span class="material-symbols-outlined">delete</span></button>
</div>
</td>
</tr>
<!-- Item 2 -->
<tr class="hover:bg-surface-container-high/40 transition-colors group">
<td class="px-lg py-md">
<p class="text-on-surface font-medium">10 Out 2023</p>
<p class="text-label-muted text-on-surface-variant">10:15</p>
</td>
<td class="px-lg py-md">
<div class="flex items-center gap-md">
<div class="w-10 h-10 rounded-lg bg-surface-container-highest flex items-center justify-center text-primary">
<span class="material-symbols-outlined" style="font-variation-settings: 'FILL' 1;">flash_on</span>
</div>
<div>
<p class="text-on-surface font-bold">Conta de Energia Elétrica</p>
<p class="text-label-muted text-on-surface-variant">ENDE - Ref: 9022341</p>
</div>
</div>
</td>
<td class="px-lg py-md">
<span class="px-sm py-xs bg-tertiary-container/20 text-tertiary rounded-full text-body-sm font-medium">Água/Luz</span>
</td>
<td class="px-lg py-md">
<div class="flex items-center gap-xs text-on-surface-variant font-medium">
<span class="w-2 h-2 rounded-full bg-outline"></span>
                                    Pendente
                                </div>
</td>
<td class="px-lg py-md text-right">
<span class="font-data-numeric text-data-numeric text-on-surface">85.400,00</span>
</td>
<td class="px-lg py-md text-center">
<div class="flex justify-center gap-xs opacity-0 group-hover:opacity-100 transition-opacity">
<button class="p-xs hover:text-primary transition-colors"><span class="material-symbols-outlined">visibility</span></button>
<button class="p-xs hover:text-primary transition-colors"><span class="material-symbols-outlined">edit</span></button>
<button class="p-xs hover:text-error transition-colors"><span class="material-symbols-outlined">delete</span></button>
</div>
</td>
</tr>
<!-- Item 3 -->
<tr class="hover:bg-surface-container-high/40 transition-colors group">
<td class="px-lg py-md">
<p class="text-on-surface font-medium">08 Out 2023</p>
<p class="text-label-muted text-on-surface-variant">16:45</p>
</td>
<td class="px-lg py-md">
<div class="flex items-center gap-md">
<div class="w-10 h-10 rounded-lg bg-surface-container-highest flex items-center justify-center text-primary">
<span class="material-symbols-outlined" style="font-variation-settings: 'FILL' 1;">build</span>
</div>
<div>
<p class="text-on-surface font-bold">Manutenção do Gerador</p>
<p class="text-label-muted text-on-surface-variant">PowerService Lda</p>
</div>
</div>
</td>
<td class="px-lg py-md">
<span class="px-sm py-xs bg-surface-container-highest text-secondary rounded-full text-body-sm font-medium">Manutenção</span>
</td>
<td class="px-lg py-md">
<div class="flex items-center gap-xs text-primary font-medium">
<span class="w-2 h-2 rounded-full bg-primary"></span>
                                    Liquidado
                                </div>
</td>
<td class="px-lg py-md text-right">
<span class="font-data-numeric text-data-numeric text-on-surface">150.000,00</span>
</td>
<td class="px-lg py-md text-center">
<div class="flex justify-center gap-xs opacity-0 group-hover:opacity-100 transition-opacity">
<button class="p-xs hover:text-primary transition-colors"><span class="material-symbols-outlined">visibility</span></button>
<button class="p-xs hover:text-primary transition-colors"><span class="material-symbols-outlined">print</span></button>
<button class="p-xs hover:text-error transition-colors"><span class="material-symbols-outlined">delete</span></button>
</div>
</td>
</tr>
<!-- Item 4 -->
<tr class="hover:bg-surface-container-high/40 transition-colors group">
<td class="px-lg py-md">
<p class="text-on-surface font-medium">05 Out 2023</p>
<p class="text-label-muted text-on-surface-variant">09:00</p>
</td>
<td class="px-lg py-md">
<div class="flex items-center gap-md">
<div class="w-10 h-10 rounded-lg bg-surface-container-highest flex items-center justify-center text-primary">
<span class="material-symbols-outlined" style="font-variation-settings: 'FILL' 1;">water_drop</span>
</div>
<div>
<p class="text-on-surface font-bold">Fornecimento de Água Potável</p>
<p class="text-label-muted text-on-surface-variant">EPAL Luanda</p>
</div>
</div>
</td>
<td class="px-lg py-md">
<span class="px-sm py-xs bg-tertiary-container/20 text-tertiary rounded-full text-body-sm font-medium">Água/Luz</span>
</td>
<td class="px-lg py-md">
<div class="flex items-center gap-xs text-primary font-medium">
<span class="w-2 h-2 rounded-full bg-primary"></span>
                                    Liquidado
                                </div>
</td>
<td class="px-lg py-md text-right">
<span class="font-data-numeric text-data-numeric text-on-surface">22.650,00</span>
</td>
<td class="px-lg py-md text-center">
<div class="flex justify-center gap-xs opacity-0 group-hover:opacity-100 transition-opacity">
<button class="p-xs hover:text-primary transition-colors"><span class="material-symbols-outlined">visibility</span></button>
<button class="p-xs hover:text-primary transition-colors"><span class="material-symbols-outlined">print</span></button>
<button class="p-xs hover:text-error transition-colors"><span class="material-symbols-outlined">delete</span></button>
</div>
</td>
</tr>
</tbody>
</table>
</div>
<!-- Pagination -->
<div class="px-xl py-md bg-surface-container-high/30 border-t border-outline-variant/30 flex items-center justify-between">
<p class="text-body-sm text-on-surface-variant">Mostrando 1-4 de 128 despesas</p>
<div class="flex items-center gap-sm">
<button class="w-10 h-10 rounded-lg border border-outline-variant flex items-center justify-center hover:bg-surface-container-highest transition-all disabled:opacity-30" disabled="">
<span class="material-symbols-outlined">chevron_left</span>
</button>
<button class="w-10 h-10 rounded-lg bg-primary text-on-primary flex items-center justify-center font-bold">1</button>
<button class="w-10 h-10 rounded-lg border border-outline-variant flex items-center justify-center hover:bg-surface-container-highest transition-all">2</button>
<button class="w-10 h-10 rounded-lg border border-outline-variant flex items-center justify-center hover:bg-surface-container-highest transition-all">3</button>
<button class="w-10 h-10 rounded-lg border border-outline-variant flex items-center justify-center hover:bg-surface-container-highest transition-all">
<span class="material-symbols-outlined">chevron_right</span>
</button>
</div>
</div>
</div>
<div class="h-16"></div> <!-- Spacer for footer -->
</main>
<!-- Footer -->
<footer class="fixed bottom-0 right-0 w-[calc(100%-260px)] h-8 bg-surface-container-lowest flex justify-between items-center px-xl z-50 text-label-muted text-on-surface-variant/60">
<div class="flex items-center gap-md">
<span>v2.4.1 | Status do Backup: Sincronizado</span>
<span class="w-2 h-2 rounded-full bg-primary"></span>
</div>
<div class="flex gap-lg">
<a class="hover:text-primary transition-all" href="#">Suporte</a>
<a class="hover:text-primary transition-all" href="#">Documentação</a>
</div>
</footer>
<!-- New Expense Modal (Simplified/Hidden) -->
<div class="fixed inset-0 bg-black/60 backdrop-blur-sm z-[100] hidden items-center justify-center p-xl" id="modal-overlay">
<div class="w-full max-w-lg neo-card rounded-2xl p-xl scale-95 opacity-0 transition-all duration-300" id="modal-content">
<div class="flex justify-between items-center mb-lg">
<h3 class="text-display-md font-bold text-on-surface">Nova Saída de Caixa</h3>
<button class="material-symbols-outlined text-on-surface-variant hover:text-error transition-colors" onclick="toggleModal()">close</button>
</div>
<div class="space-y-md">
<div>
<label class="font-label-muted text-on-surface-variant block mb-xs">Descrição</label>
<input class="w-full neo-input rounded-xl px-md py-md text-on-surface outline-none" placeholder="Ex: Pagamento Internet Unitel" type="text"/>
</div>
<div class="grid grid-cols-2 gap-md">
<div>
<label class="font-label-muted text-on-surface-variant block mb-xs">Valor (Kz)</label>
<input class="w-full neo-input rounded-xl px-md py-md text-on-surface outline-none" placeholder="0,00" type="number"/>
</div>
<div>
<label class="font-label-muted text-on-surface-variant block mb-xs">Data</label>
<input class="w-full neo-input rounded-xl px-md py-md text-on-surface outline-none" type="date"/>
</div>
</div>
<div>
<label class="font-label-muted text-on-surface-variant block mb-xs">Categoria</label>
<select class="w-full neo-input rounded-xl px-md py-md text-on-surface outline-none">
<option>Manutenção</option>
<option>Salários</option>
<option>Água/Luz</option>
<option>Outros</option>
</select>
</div>
<div class="pt-lg">
<button class="w-full neo-button-primary py-md rounded-xl text-white font-bold text-body-lg">Registrar Pagamento</button>
</div>
</div>
</div>
</div>
<script>
        // Simple Interaction Script
        const btnNovaSaida = document.querySelector('.neo-button-primary');
        const modalOverlay = document.getElementById('modal-overlay');
        const modalContent = document.getElementById('modal-content');

        function toggleModal() {
            if(modalOverlay.classList.contains('hidden')) {
                modalOverlay.classList.remove('hidden');
                modalOverlay.classList.add('flex');
                setTimeout(() => {
                    modalContent.classList.remove('scale-95', 'opacity-0');
                    modalContent.classList.add('scale-100', 'opacity-100');
                }, 10);
            } else {
                modalContent.classList.add('scale-95', 'opacity-0');
                modalContent.classList.</script></body></html>




<!DOCTYPE html>

<html class="dark" lang="pt"><head>
<meta charset="utf-8"/>
<meta content="width=device-width, initial-scale=1.0" name="viewport"/>
<title>School Manager - Alunos</title>
<script src="https://cdn.tailwindcss.com?plugins=forms,container-queries"></script>
<link href="https://fonts.googleapis.com/css2?family=Poppins:wght@500;600;700&amp;family=Inter:wght@400;600;700&amp;display=swap" rel="stylesheet"/>
<link href="https://fonts.googleapis.com/css2?family=Material+Symbols+Outlined:wght,FILL@100..700,0..1&amp;display=swap" rel="stylesheet"/>
<script id="tailwind-config">
        tailwind.config = {
            darkMode: "class",
            theme: {
                extend: {
                    "colors": {
                        "primary-container": "#4d8eff",
                        "surface": "#0b1326",
                        "primary-fixed-dim": "#adc6ff",
                        "on-secondary-fixed-variant": "#3c475a",
                        "on-surface": "#dae2fd",
                        "surface-container": "#171f33",
                        "on-primary-container": "#00285d",
                        "secondary-fixed-dim": "#bcc7de",
                        "surface-container-lowest": "#060e20",
                        "error-container": "#93000a",
                        "inverse-surface": "#dae2fd",
                        "on-error": "#690005",
                        "on-primary": "#002e6a",
                        "surface-tint": "#adc6ff",
                        "primary": "#adc6ff",
                        "on-tertiary-fixed-variant": "#004883",
                        "error": "#ffb4ab",
                        "tertiary-fixed": "#d4e3ff",
                        "background": "#0b1326",
                        "tertiary-fixed-dim": "#a4c9ff",
                        "on-secondary": "#263143",
                        "secondary-container": "#3e495d",
                        "surface-container-low": "#131b2e",
                        "surface-container-highest": "#2d3449",
                        "on-tertiary": "#00315d",
                        "primary-fixed": "#d8e2ff",
                        "outline": "#8c909f",
                        "on-primary-fixed": "#001a42",
                        "secondary-fixed": "#d8e3fb",
                        "outline-variant": "#424754",
                        "on-tertiary-container": "#002a51",
                        "on-error-container": "#ffdad6",
                        "on-background": "#dae2fd",
                        "on-surface-variant": "#c2c6d6",
                        "tertiary": "#a4c9ff",
                        "secondary": "#bcc7de",
                        "inverse-primary": "#005ac2",
                        "surface-container-high": "#222a3d",
                        "tertiary-container": "#4c93e7",
                        "inverse-on-surface": "#283044",
                        "surface-bright": "#31394d",
                        "surface-dim": "#0b1326",
                        "on-secondary-container": "#aeb9d0",
                        "on-secondary-fixed": "#111c2d",
                        "surface-variant": "#2d3449",
                        "on-tertiary-fixed": "#001c39",
                        "on-primary-fixed-variant": "#004395"
                    },
                    "borderRadius": {
                        "DEFAULT": "0.25rem",
                        "lg": "0.5rem",
                        "xl": "0.75rem",
                        "full": "9999px"
                    },
                    "spacing": {
                        "xl": "32px",
                        "lg": "24px",
                        "md": "16px",
                        "gutter": "20px",
                        "xs": "8px",
                        "base": "4px",
                        "sm": "12px",
                        "sidebar_width": "260px"
                    },
                    "fontFamily": {
                        "body-lg": ["Inter"],
                        "display-lg": ["Poppins"],
                        "body-sm": ["Inter"],
                        "headline-sm": ["Poppins"],
                        "display-md": ["Poppins"],
                        "data-numeric": ["Inter"],
                        "label-muted": ["Inter"],
                        "body-md": ["Inter"],
                        "label-bold": ["Inter"]
                    },
                    "fontSize": {
                        "body-lg": ["16px", {"lineHeight": "1.5", "fontWeight": "400"}],
                        "display-lg": ["32px", {"lineHeight": "1.2", "letterSpacing": "-0.02em", "fontWeight": "600"}],
                        "body-sm": ["13px", {"lineHeight": "1.5", "fontWeight": "400"}],
                        "headline-sm": ["18px", {"lineHeight": "1.4", "fontWeight": "500"}],
                        "display-md": ["24px", {"lineHeight": "1.3", "fontWeight": "600"}],
                        "data-numeric": ["20px", {"lineHeight": "1", "letterSpacing": "-0.01em", "fontWeight": "700"}],
                        "label-muted": ["12px", {"lineHeight": "1", "fontWeight": "400"}],
                        "body-md": ["14px", {"lineHeight": "1.5", "fontWeight": "400"}],
                        "label-bold": ["12px", {"lineHeight": "1", "letterSpacing": "0.05em", "fontWeight": "600"}]
                    }
                },
            },
        }
    </script>
<style>
        body {
            background-color: #0b1326;
            color: #dae2fd;
            font-family: 'Inter', sans-serif;
            overflow: hidden;
        }
        .material-symbols-outlined {
            font-variation-settings: 'FILL' 0, 'wght' 400, 'GRAD' 0, 'opsz' 24;
        }
        .neo-pressed {
            box-shadow: inset 2px 2px 5px rgba(0,0,0,0.4), inset -1px -1px 2px rgba(255,255,255,0.05);
        }
        .neo-float {
            box-shadow: 0 4px 20px rgba(0,0,0,0.4), inset 1px 1px 0px rgba(255,255,255,0.05);
        }
        .glass-panel {
            background: rgba(19, 27, 46, 0.7);
            backdrop-filter: blur(12px);
            border: 1px solid rgba(255, 255, 255, 0.05);
        }
        .custom-scrollbar::-webkit-scrollbar {
            width: 6px;
        }
        .custom-scrollbar::-webkit-scrollbar-track {
            background: rgba(255,255,255,0.02);
        }
        .custom-scrollbar::-webkit-scrollbar-thumb {
            background: #2d3449;
            border-radius: 10px;
        }
        .custom-scrollbar::-webkit-scrollbar-thumb:hover {
            background: #3e495d;
        }
    </style>
</head>
<body class="flex h-screen w-full select-none">
<!-- Sidebar Navigation -->
<aside class="w-[260px] h-screen flex flex-col border-r border-outline-variant/10 bg-surface-container-low dark:bg-surface-container-low fixed left-0 top-0 p-md space-y-lg shadow-xl z-50">
<div class="flex items-center gap-sm mb-lg px-xs">
<div class="w-10 h-10 bg-primary-container rounded-lg flex items-center justify-center neo-float">
<span class="material-symbols-outlined text-on-primary-container" style="font-variation-settings: 'FILL' 1;">school</span>
</div>
<div>
<h1 class="text-headline-sm font-display-lg text-on-surface tracking-tight">School Manager</h1>
<p class="font-label-muted text-label-muted opacity-60">Gestão Escolar</p>
</div>
</div>
<nav class="flex-1 space-y-xs overflow-y-auto custom-scrollbar">
<!-- Dashboard -->
<a class="flex items-center gap-md p-sm text-on-surface-variant hover:text-on-surface hover:bg-surface-variant/50 transition-all duration-200 rounded-lg group" href="../dashboard_school_manager_refatorado/code.html">
<span class="material-symbols-outlined group-hover:scale-110 transition-transform">dashboard</span>
<span class="font-label-bold text-label-bold">Dashboard</span>
</a>
<!-- Alunos (Active) -->
<a class="flex items-center gap-md p-sm bg-primary-container text-on-primary-container rounded-lg font-label-bold shadow-[0_4px_12px_rgba(77,142,255,0.3)] scale-[0.98] transition-transform" href="code.html">
<span class="material-symbols-outlined" style="font-variation-settings: 'FILL' 1;">group</span>
<span class="font-label-bold text-label-bold">Alunos</span>
</a>
<!-- Financeiro -->
<a class="flex items-center gap-md p-sm text-on-surface-variant hover:text-on-surface hover:bg-surface-variant/50 transition-all duration-200 rounded-lg group" href="../financeiro_pagamentos_school_manager/code.html">
<span class="material-symbols-outlined group-hover:scale-110 transition-transform">payments</span>
<span class="font-label-bold text-label-bold">Financeiro</span>
</a>
<!-- Relatórios -->
<a class="flex items-center gap-md p-sm text-on-surface-variant hover:text-on-surface hover:bg-surface-variant/50 transition-all duration-200 rounded-lg group" href="../relat_rios_administrativos_school_manager/code.html">
<span class="material-symbols-outlined group-hover:scale-110 transition-transform">analytics</span>
<span class="font-label-bold text-label-bold">Relatórios</span>
</a>
<!-- Escola -->
<a class="flex items-center gap-md p-sm text-on-surface-variant hover:text-on-surface hover:bg-surface-variant/50 transition-all duration-200 rounded-lg group" href="../escola_classes_school_manager/code.html">
<span class="material-symbols-outlined group-hover:scale-110 transition-transform">account_balance</span>
<span class="font-label-bold text-label-bold">Escola</span>
</a>
<!-- Configurações -->
<a class="flex items-center gap-md p-sm text-on-surface-variant hover:text-on-surface hover:bg-surface-variant/50 transition-all duration-200 rounded-lg group" href="../configura_es_do_sistema_school_manager/code.html">
<span class="material-symbols-outlined group-hover:scale-110 transition-transform">settings</span>
<span class="font-label-bold text-label-bold">Configurações</span>
</a>
</nav>
<div class="pt-lg border-t border-outline-variant/10 space-y-xs">
<div class="flex items-center gap-sm p-sm bg-surface-container rounded-xl neo-float mb-md">
<img alt="Admin headshot" class="w-10 h-10 rounded-full object-cover ring-2 ring-primary/20" src="https://lh3.googleusercontent.com/aida-public/AB6AXuDTW-liZCx_EDQzw5VCNLk6RsFni8Lj10rifJNpVPDWJr2i-fueoEF1yNSKCKM7mEPUUAgYM1m4c1oyIk-ETu-vsRnp1UTVYpVfUynzm9lk7N871fyiZGZojrao7zjqviUux4DIf2vPbfyi2AQguQT3QiphSjsJ41gWjYhWAop_aOb05UhShEC7ROPab_v-78UWMcKywGCGDrqkiIzzVnnHRJ69aH4I_lrfZd7dyw-46fDy5QcnMxISvZ8zZICkuRKcr2sV_B54fu7P"/>
<div class="flex-1 min-w-0">
<p class="font-label-bold text-on-surface truncate">Secretaria</p>
<p class="font-label-muted text-label-muted text-[10px] uppercase tracking-widest opacity-50">Administrador</p>
</div>
<div class="w-2 h-2 rounded-full bg-emerald-500 shadow-[0_0_8px_rgba(16,185,129,0.5)]"></div>
</div>
<a class="flex items-center gap-md p-sm text-on-surface-variant hover:text-error transition-colors duration-200" href="#">
<span class="material-symbols-outlined">logout</span>
<span class="font-label-bold text-label-bold">Sair do Sistema</span>
</a>
</div>
</aside>
<!-- Main Content Area -->
<main class="flex-1 ml-[260px] flex flex-col min-h-screen relative overflow-hidden bg-background">
<!-- Background Atmospheric Effect -->
<div class="absolute -top-[20%] -right-[10%] w-[600px] h-[600px] bg-primary/5 rounded-full blur-[120px] pointer-events-none"></div>
<div class="absolute -bottom-[20%] -left-[10%] w-[400px] h-[400px] bg-tertiary/5 rounded-full blur-[100px] pointer-events-none"></div>
<!-- Top AppBar -->
<header class="w-full h-auto flex flex-row justify-between items-center py-lg px-xl bg-transparent z-40">
<div class="flex flex-col">
<h2 class="font-display-md text-display-md text-on-surface">Gestão de Alunos</h2>
<div class="flex items-center gap-xs text-label-muted font-body-sm opacity-60">
<span>Dashboard</span>
<span class="material-symbols-outlined text-[14px]">chevron_right</span>
<span class="text-primary font-label-bold">Lista de Alunos</span>
</div>
</div>
<div class="flex items-center gap-md">
<div class="flex items-center bg-surface-container-low px-md py-xs rounded-xl neo-pressed border border-outline-variant/10">
<span class="material-symbols-outlined text-label-muted mr-xs">calendar_today</span>
<span class="font-body-sm text-on-surface">27 de Maio, 2026</span>
</div>
<button class="w-10 h-10 flex items-center justify-center rounded-full bg-surface-container-high hover:bg-surface-variant transition-colors neo-float relative">
<span class="material-symbols-outlined text-on-surface">notifications</span>
<span class="absolute top-2 right-2 w-2 h-2 bg-primary rounded-full"></span>
</button>
<button class="bg-primary-container text-on-primary-container px-lg py-sm rounded-xl font-label-bold flex items-center gap-xs shadow-[0_8px_16px_rgba(77,142,255,0.2)] hover:scale-105 active:scale-95 transition-all">
<span class="material-symbols-outlined text-[20px]">person_add</span>
                Novo Aluno
            </button>
</div>
</header>
<!-- Filters & Tools Bar -->
<div class="px-xl py-md flex flex-wrap items-center gap-md z-30">
<!-- Search -->
<div class="flex-1 min-w-[300px] relative group">
<span class="material-symbols-outlined absolute left-md top-1/2 -translate-y-1/2 text-on-surface-variant group-focus-within:text-primary transition-colors">search</span>
<input class="w-full bg-surface-container-low border border-outline-variant/10 rounded-xl py-sm pl-xl pr-md text-body-md focus:ring-2 focus:ring-primary/20 focus:border-primary outline-none transition-all neo-pressed" placeholder="Pesquisar por nome, nº de estudante ou encarregado..." type="text"/>
</div>
<!-- Filters -->
<div class="flex items-center gap-sm bg-surface-container-low p-xs rounded-xl border border-outline-variant/10 neo-float">
<select class="bg-transparent border-none text-body-sm text-on-surface-variant focus:ring-0 cursor-pointer px-md py-xs font-label-bold">
<option>Todas as Classes</option>
<option>10ª Classe A</option>
<option>9ª Classe B</option>
<option>8ª Classe C</option>
</select>
<div class="w-[1px] h-4 bg-outline-variant/20"></div>
<select class="bg-transparent border-none text-body-sm text-on-surface-variant focus:ring-0 cursor-pointer px-md py-xs font-label-bold">
<option>Status: Todos</option>
<option>Ativos</option>
<option>Inativos</option>
<option>Pendentes</option>
</select>
<div class="w-[1px] h-4 bg-outline-variant/20"></div>
<select class="bg-transparent border-none text-body-sm text-on-surface-variant focus:ring-0 cursor-pointer px-md py-xs font-label-bold">
<option>Ano: 2025/2026</option>
<option>Ano: 2024/2025</option>
</select>
</div>
<button class="p-sm rounded-xl bg-surface-container-high hover:bg-surface-variant text-on-surface transition-all neo-float flex items-center gap-xs">
<span class="material-symbols-outlined">filter_list</span>
<span class="text-label-bold">Mais Filtros</span>
</button>
<button class="p-sm rounded-xl bg-surface-container-high hover:bg-surface-variant text-on-surface transition-all neo-float">
<span class="material-symbols-outlined">download</span>
</button>
</div>
<!-- Data Grid Table -->
<div class="px-xl flex-1 overflow-hidden flex flex-col pb-xl z-20">
<div class="flex-1 glass-panel rounded-2xl overflow-hidden flex flex-col neo-float border border-outline-variant/20">
<div class="overflow-x-auto custom-scrollbar flex-1">
<table class="w-full text-left border-collapse">
<thead class="sticky top-0 bg-surface-container-high/90 backdrop-blur-md z-10">
<tr>
<th class="px-lg py-md font-label-bold text-label-bold text-on-surface-variant uppercase tracking-widest border-b border-outline-variant/10">Nº Estudante</th>
<th class="px-lg py-md font-label-bold text-label-bold text-on-surface-variant uppercase tracking-widest border-b border-outline-variant/10">Nome</th>
<th class="px-lg py-md font-label-bold text-label-bold text-on-surface-variant uppercase tracking-widest border-b border-outline-variant/10">Classe</th>
<th class="px-lg py-md font-label-bold text-label-bold text-on-surface-variant uppercase tracking-widest border-b border-outline-variant/10">Encarregado</th>
<th class="px-lg py-md font-label-bold text-label-bold text-on-surface-variant uppercase tracking-widest border-b border-outline-variant/10">Telefone</th>
<th class="px-lg py-md font-label-bold text-label-bold text-on-surface-variant uppercase tracking-widest border-b border-outline-variant/10">Estado</th>
<th class="px-lg py-md font-label-bold text-label-bold text-on-surface-variant uppercase tracking-widest border-b border-outline-variant/10 text-right">Ações</th>
</tr>
</thead>
<tbody class="divide-y divide-outline-variant/5">
<!-- Row 1 -->
<tr class="hover:bg-primary/5 transition-colors group cursor-default">
<td class="px-lg py-sm font-data-numeric text-on-surface opacity-80">2026/0842</td>
<td class="px-lg py-sm">
<div class="flex items-center gap-sm">
<div class="w-8 h-8 rounded-full overflow-hidden bg-surface-container border border-outline-variant/20">
<img class="w-full h-full object-cover" data-alt="Student profile" src="https://lh3.googleusercontent.com/aida-public/AB6AXuCJwUmYy9FZm5ZfW6szeVspjCGf4JZ8MzOtW2t9Zu8EG8ehufQAulu2Vq3TyGeYi_Slshh8qON0BsL8DhLBWBiLhzhNmOdCZPtJsqMsU9U72lga6-t_bsZsnDSsNz4fzUc7n2ePEJ0uNYc-JJqKutGL2jjIz0KEfd3OJfOzcv4I478HuV15lFxurXdZoW01nJJvq5R3z03SmaE5ZbvaKOabJwoLCOf0lKbMON-LYy3UAp02eP2ItwNDls57JJhbrpXVbHTETLeZ9SBG"/>
</div>
<span class="font-body-md text-on-surface font-semibold group-hover:text-primary transition-colors">João Pedro da Silva</span>
</div>
</td>
<td class="px-lg py-sm text-body-sm text-on-surface-variant">10ª Classe A</td>
<td class="px-lg py-sm text-body-sm text-on-surface-variant">Ricardo da Silva</td>
<td class="px-lg py-sm font-data-numeric text-on-surface-variant">+244 923 000 111</td>
<td class="px-lg py-sm">
<span class="px-sm py-1 bg-emerald-500/10 text-emerald-400 text-[10px] font-bold uppercase rounded-full border border-emerald-500/20 shadow-[0_0_8px_rgba(16,185,129,0.1)]">Ativo</span>
</td>
<td class="px-lg py-sm text-right">
<button class="p-xs hover:bg-primary/10 rounded-lg text-on-surface-variant hover:text-primary transition-all">
<span class="material-symbols-outlined">edit_square</span>
</button>
<button class="p-xs hover:bg-surface-variant rounded-lg text-on-surface-variant transition-all">
<span class="material-symbols-outlined">more_vert</span>
</button>
</td>
</tr>
<!-- Row 2 -->
<tr class="hover:bg-primary/5 transition-colors group cursor-default">
<td class="px-lg py-sm font-data-numeric text-on-surface opacity-80">2026/1205</td>
<td class="px-lg py-sm">
<div class="flex items-center gap-sm">
<div class="w-8 h-8 rounded-full overflow-hidden bg-surface-container border border-outline-variant/20">
<img class="w-full h-full object-cover" data-alt="Student profile" src="https://lh3.googleusercontent.com/aida-public/AB6AXuCBMAqJoIEGfS9dwZMFdyRRbgoJAYc-yKS6C_fkZgsbV1moVMlQdPvTLp-BaxZ4QRHSC0Er9bpUHx5rcgm91k9vVeMShG2yvw_DvVbMx-gNFMYRDlGFXOD_ReNfitBwuG2CP1OyG489x6g3RROYd32RHIv8HtI1GwbYmCuw6GOE0LBSX7WFs1Pem4bgWBoQltnTz4oYysXgQkTKC_z1CKYdmxZyYYqIRPPlr-ao2or9_eQiGDdysq399KE6cQIyfRVwJV6KP_XMT0Ck"/>
</div>
<span class="font-body-md text-on-surface font-semibold group-hover:text-primary transition-colors">Maria Luísa Alberto</span>
</div>
</td>
<td class="px-lg py-sm text-body-sm text-on-surface-variant">9ª Classe B</td>
<td class="px-lg py-sm text-body-sm text-on-surface-variant">Isabel Alberto</td>
<td class="px-lg py-sm font-data-numeric text-on-surface-variant">+244 931 444 222</td>
<td class="px-lg py-sm">
<span class="px-sm py-1 bg-emerald-500/10 text-emerald-400 text-[10px] font-bold uppercase rounded-full border border-emerald-500/20">Ativo</span>
</td>
<td class="px-lg py-sm text-right">
<button class="p-xs hover:bg-primary/10 rounded-lg text-on-surface-variant hover:text-primary transition-all">
<span class="material-symbols-outlined">edit_square</span>
</button>
<button class="p-xs hover:bg-surface-variant rounded-lg text-on-surface-variant transition-all">
<span class="material-symbols-outlined">more_vert</span>
</button>
</td>
</tr>
<!-- Row 3 (Inactive) -->
<tr class="hover:bg-primary/5 transition-colors group cursor-default opacity-80">
<td class="px-lg py-sm font-data-numeric text-on-surface opacity-80">2026/0591</td>
<td class="px-lg py-sm">
<div class="flex items-center gap-sm">
<div class="w-8 h-8 rounded-full overflow-hidden bg-surface-container border border-outline-variant/20 grayscale">
<img class="w-full h-full object-cover" data-alt="Student profile" src="https://lh3.googleusercontent.com/aida-public/AB6AXuA-DWLmjf57QoQP5Zwx8Q8NF7Y4lqB4t9gDGBg1nS8i50IqdV6U7_TSJXIpCabpXhIywbxGVYFW-kPYM4GXsc5_fklLynnB-7e8bEetD0AL_zAEwlXYQxPLgNv2303lrAi7XOeNbgRrWUA3wAOpSYIlWJX3DipnMMFu2y4JKSsprBIRws7fulinAn7OQOBSNBk7NStZq2E9ygBp3v-IuAxSXnFzpQf6wy2fyuWjVQEaL4wIg81JFRQ8F5Age4d5rbdKsNBRYQKPT3IJ"/>
</div>
<span class="font-body-md text-on-surface-variant font-semibold group-hover:text-primary transition-colors">Manuel Francisco</span>
</div>
</td>
<td class="px-lg py-sm text-body-sm text-on-surface-variant">7ª Classe A</td>
<td class="px-lg py-sm text-body-sm text-on-surface-variant">Ana Francisco</td>
<td class="px-lg py-sm font-data-numeric text-on-surface-variant">+244 944 888 777</td>
<td class="px-lg py-sm">
<span class="px-sm py-1 bg-error/10 text-error text-[10px] font-bold uppercase rounded-full border border-error/20">Inativo</span>
</td>
<td class="px-lg py-sm text-right">
<button class="p-xs hover:bg-primary/10 rounded-lg text-on-surface-variant hover:text-primary transition-all">
<span class="material-symbols-outlined">edit_square</span>
</button>
<button class="p-xs hover:bg-surface-variant rounded-lg text-on-surface-variant transition-all">
<span class="material-symbols-outlined">more_vert</span>
</button>
</td>
</tr>
<!-- Row 4 -->
<tr class="hover:bg-primary/5 transition-colors group cursor-default">
<td class="px-lg py-sm font-data-numeric text-on-surface opacity-80">2026/0998</td>
<td class="px-lg py-sm">
<div class="flex items-center gap-sm">
<div class="w-8 h-8 rounded-full overflow-hidden bg-surface-container border border-outline-variant/20">
<img class="w-full h-full object-cover" data-alt="Student profile" src="https://lh3.googleusercontent.com/aida-public/AB6AXuDwjNDdGnS_I5pufsLJ-sw4xxPiRwf-nt6JAstWgrw2S-wAIe1WQ7ju7bXiZ4PQf7ThURwlvCHkyUHPYPLVXd7N14TQAaqk0rcAhexlKoddkYZJHnBB6YYxJ1BJfFhJll9AQ0ap8RTBZFCnpBNIoWeF58_gkvE-q8Zdv0SlD5ARgNDzsBvz6uhmh6H4CxW25As7FslKEuZRsRV1f889-xSqNDe7iKOE7DlquoqCb3ywt1a3X5M7vZFDpAwxvCIyPB3uhbDei13XTigp"/>
</div>
<span class="font-body-md text-on-surface font-semibold group-hover:text-primary transition-colors">Ana Paula Domingos</span>
</div>
</td>
<td class="px-lg py-sm text-body-sm text-on-surface-variant">10ª Classe A</td>
<td class="px-lg py-sm text-body-sm text-on-surface-variant">José Domingos</td>
<td class="px-lg py-sm font-data-numeric text-on-surface-variant">+244 923 111 222</td>
<td class="px-lg py-sm">
<span class="px-sm py-1 bg-emerald-500/10 text-emerald-400 text-[10px] font-bold uppercase rounded-full border border-emerald-500/20">Ativo</span>
</td>
<td class="px-lg py-sm text-right">
<button class="p-xs hover:bg-primary/10 rounded-lg text-on-surface-variant hover:text-primary transition-all">
<span class="material-symbols-outlined">edit_square</span>
</button>
<button class="p-xs hover:bg-surface-variant rounded-lg text-on-surface-variant transition-all">
<span class="material-symbols-outlined">more_vert</span>
</button>
</td>
</tr>
<!-- Row 5 -->
<tr class="hover:bg-primary/5 transition-colors group cursor-default">
<td class="px-lg py-sm font-data-numeric text-on-surface opacity-80">2026/1423</td>
<td class="px-lg py-sm">
<div class="flex items-center gap-sm">
<div class="w-8 h-8 rounded-full overflow-hidden bg-surface-container border border-outline-variant/20">
<img class="w-full h-full object-cover" data-alt="Student profile" src="https://lh3.googleusercontent.com/aida-public/AB6AXuBmqlDBZV1qOHjDzsOlfhv-8iOiTKzGlKKR_SG3JTRFSVShfdnlXeFHJkQrz4j74Cs4T7B2TB8uKs3i57vU8no5zIWRiCpnRCRqppCyAoYqswWlKaKCr8sTsNKDy8-aFiXyX2LOPId5ZuJaul5BDS0nTYHkA1qrSPs5x_p-I6Euxx_dvglfszklM4y6MQzNedMkGIHdZ8hQZzphNsPX1xtbY3fA6Hp-r-lt1iLHHWDoovgicXtrqU-ycV-DCNz7sKzKj6gFDRFTfkzL"/>
</div>
<span class="font-body-md text-on-surface font-semibold group-hover:text-primary transition-colors">Carlos Manuel</span>
</div>
</td>
<td class="px-lg py-sm text-body-sm text-on-surface-variant">6ª Classe B</td>
<td class="px-lg py-sm text-body-sm text-on-surface-variant">Isabel Manuel</td>
<td class="px-lg py-sm font-data-numeric text-on-surface-variant">+244 928 555 333</td>
<td class="px-lg py-sm">
<span class="px-sm py-1 bg-emerald-500/10 text-emerald-400 text-[10px] font-bold uppercase rounded-full border border-emerald-500/20">Ativo</span>
</td>
<td class="px-lg py-sm text-right">
<button class="p-xs hover:bg-primary/10 rounded-lg text-on-surface-variant hover:text-primary transition-all">
<span class="material-symbols-outlined">edit_square</span>
</button>
<button class="p-xs hover:bg-surface-variant rounded-lg text-on-surface-variant transition-all">
<span class="material-symbols-outlined">more_vert</span>
</button>
</td>
</tr>
</tbody>
</table>
</div>
<!-- Pagination Footer -->
<div class="px-lg py-md bg-surface-container-high/50 border-t border-outline-variant/10 flex items-center justify-between">
<p class="text-label-muted font-body-sm">A exibir <span class="font-bold text-on-surface">1-10</span> de <span class="font-bold text-on-surface">1,240</span> alunos</p>
<div class="flex items-center gap-xs">
<button class="w-8 h-8 flex items-center justify-center rounded-lg bg-surface-container hover:bg-surface-variant text-on-surface-variant neo-float transition-all disabled:opacity-30" disabled="">
<span class="material-symbols-outlined text-[18px]">chevron_left</span>
</button>
<button class="w-8 h-8 flex items-center justify-center rounded-lg bg-primary-container text-on-primary-container font-label-bold neo-float">1</button>
<button class="w-8 h-8 flex items-center justify-center rounded-lg bg-surface-container hover:bg-surface-variant text-on-surface-variant transition-all">2</button>
<button class="w-8 h-8 flex items-center justify-center rounded-lg bg-surface-container hover:bg-surface-variant text-on-surface-variant transition-all">3</button>
<span class="text-label-muted px-xs">...</span>
<button class="w-8 h-8 flex items-center justify-center rounded-lg bg-surface-container hover:bg-surface-variant text-on-surface-variant transition-all">124</button>
<button class="w-8 h-8 flex items-center justify-center rounded-lg bg-surface-container hover:bg-surface-variant text-on-surface-variant neo-float transition-all">
<span class="material-symbols-outlined text-[18px]">chevron_right</span>
</button>
</div>
</div>
</div>
</div>
<!-- Global Footer -->
<footer class="flex justify-between items-center px-xl w-full py-md mt-auto border-t border-outline-variant/10 bg-transparent z-40">
<p class="font-label-muted text-label-muted text-[11px] uppercase tracking-widest opacity-60">School Manager v1.0.0 | Todos os direitos reservados</p>
<div class="flex items-center gap-md">
<div class="flex items-center gap-xs">
<div class="w-2 h-2 rounded-full bg-emerald-500 animate-pulse"></div>
<span class="font-label-muted text-label-muted text-[11px] uppercase tracking-widest opacity-80">Backup: OK</span>
</div>
<div class="w-[1px] h-3 bg-outline-variant/30"></div>
<span class="font-label-muted text-label-muted text-[11px] uppercase tracking-widest opacity-60">Servidor: Online</span>
</div>
</footer>
<!-- Floating Action Button -->
<button class="fixed bottom-12 right-12 w-14 h-14 bg-primary rounded-2xl flex items-center justify-center text-on-primary shadow-[0_12px_24px_rgba(173,198,255,0.4)] hover:scale-110 active:scale-95 transition-all z-50 group">
<span class="material-symbols-outlined text-[28px]" style="font-variation-settings: 'FILL' 1;">add_circle</span>
<span class="absolute right-full mr-4 px-3 py-1 bg-surface-container-highest text-white text-xs rounded-lg opacity-0 group-hover:opacity-100 transition-opacity pointer-events-none whitespace-nowrap">Matricular Aluno</span>
</button>
</main>
<script>
    // Micro-interactions and row focus simulation
    document.querySelectorAll('tbody tr').forEach(row => {
        row.addEventListener('mousedown', () => {
            row.classList.add('scale-[0.995]', 'bg-primary/10');
            row.style.transition = 'transform 0.1s ease';
        });
        row.addEventListener('mouseup', () => {
            row.classList.remove('scale-[0.995]', 'bg-primary/10');
        });
    });

    // Search highlight simulation
    const searchInput = document.querySelector('input[type="text"]');
    searchInput.addEventListener('input', (e) => {
        if(e.target.value.length > 0) {
            searchInput.classList.add('border-primary/50');
        } else {
            searchInput.classList.remove('border-primary/50');
        }
    });
</script>
</body></html>




<!DOCTYPE html>

<html class="dark" lang="pt-BR"><head>
<meta charset="utf-8"/>
<meta content="width=device-width, initial-scale=1.0" name="viewport"/>
<title>Módulo Financeiro - Histórico de Fechamentos</title>
<script src="https://cdn.tailwindcss.com?plugins=forms,container-queries"></script>
<link href="https://fonts.googleapis.com/css2?family=Inter:wght@400;500;600;700&amp;family=Poppins:wght@500;600;700&amp;family=Material+Symbols+Outlined:wght,FILL@100..700,0..1&amp;display=swap" rel="stylesheet"/>
<link href="https://fonts.googleapis.com/css2?family=Material+Symbols+Outlined:wght,FILL@100..700,0..1&amp;display=swap" rel="stylesheet"/>
<link href="https://fonts.googleapis.com/css2?family=Inter:wght@100..900&amp;family=Poppins:wght@100..900&amp;display=swap" rel="stylesheet"/>
<script id="tailwind-config">
        tailwind.config = {
            darkMode: "class",
            theme: {
                extend: {
                    "colors": {
                        "on-secondary": "#263143",
                        "on-tertiary-fixed": "#001c39",
                        "error": "#ffb4ab",
                        "surface-container": "#171f33",
                        "error-container": "#93000a",
                        "on-surface-variant": "#c2c6d6",
                        "surface-bright": "#31394d",
                        "surface-tint": "#adc6ff",
                        "on-tertiary-container": "#002a51",
                        "on-primary-fixed-variant": "#004395",
                        "tertiary-fixed": "#d4e3ff",
                        "surface-dim": "#0b1326",
                        "outline": "#8c909f",
                        "secondary": "#bcc7de",
                        "on-tertiary": "#00315d",
                        "surface-container-low": "#131b2e",
                        "outline-variant": "#424754",
                        "secondary-fixed-dim": "#bcc7de",
                        "on-primary-container": "#00285d",
                        "on-background": "#dae2fd",
                        "surface-container-lowest": "#060e20",
                        "surface": "#0b1326",
                        "tertiary-fixed-dim": "#a4c9ff",
                        "on-primary": "#002e6a",
                        "primary-container": "#4d8eff",
                        "inverse-surface": "#dae2fd",
                        "surface-container-high": "#222a3d",
                        "on-error-container": "#ffdad6",
                        "on-secondary-fixed-variant": "#3c475a",
                        "background": "#0b1326",
                        "on-tertiary-fixed-variant": "#004883",
                        "on-error": "#690005",
                        "secondary-fixed": "#d8e3fb",
                        "primary-fixed": "#d8e2ff",
                        "on-primary-fixed": "#001a42",
                        "tertiary-container": "#4c93e7",
                        "primary": "#adc6ff",
                        "tertiary": "#a4c9ff",
                        "surface-container-highest": "#2d3449",
                        "surface-variant": "#2d3449",
                        "on-surface": "#dae2fd",
                        "secondary-container": "#3e495d",
                        "on-secondary-container": "#aeb9d0",
                        "inverse-primary": "#005ac2",
                        "primary-fixed-dim": "#adc6ff",
                        "on-secondary-fixed": "#111c2d",
                        "inverse-on-surface": "#283044"
                    },
                    "borderRadius": {
                        "DEFAULT": "0.25rem",
                        "lg": "0.5rem",
                        "xl": "0.75rem",
                        "full": "9999px"
                    },
                    "spacing": {
                        "sidebar_width": "260px",
                        "lg": "24px",
                        "base": "4px",
                        "sm": "12px",
                        "xl": "32px",
                        "md": "16px",
                        "xs": "8px",
                        "gutter": "20px"
                    },
                    "fontFamily": {
                        "body-lg": ["Inter"],
                        "label-muted": ["Inter"],
                        "display-lg": ["Poppins"],
                        "label-bold": ["Inter"],
                        "data-numeric": ["Inter"],
                        "headline-sm": ["Poppins"],
                        "body-md": ["Inter"],
                        "body-sm": ["Inter"],
                        "display-md": ["Poppins"]
                    },
                    "fontSize": {
                        "body-lg": ["16px", { "lineHeight": "1.5", "fontWeight": "400" }],
                        "label-muted": ["12px", { "lineHeight": "1", "fontWeight": "400" }],
                        "display-lg": ["32px", { "lineHeight": "1.2", "letterSpacing": "-0.02em", "fontWeight": "600" }],
                        "label-bold": ["12px", { "lineHeight": "1", "letterSpacing": "0.05em", "fontWeight": "600" }],
                        "data-numeric": ["20px", { "lineHeight": "1", "letterSpacing": "-0.01em", "fontWeight": "700" }],
                        "headline-sm": ["18px", { "lineHeight": "1.4", "fontWeight": "500" }],
                        "body-md": ["14px", { "lineHeight": "1.5", "fontWeight": "400" }],
                        "body-sm": ["13px", { "lineHeight": "1.5", "fontWeight": "400" }],
                        "display-md": ["24px", { "lineHeight": "1.3", "fontWeight": "600" }]
                    }
                },
            },
        }
    </script>
<style>
        .neo-glass {
            background: linear-gradient(135deg, rgba(255, 255, 255, 0.05) 0%, rgba(255, 255, 255, 0) 100%);
            border-top: 1px solid rgba(255, 255, 255, 0.1);
            border-left: 1px solid rgba(255, 255, 255, 0.1);
            backdrop-filter: blur(8px);
        }
        .neo-pressed {
            background: #111827;
            box-shadow: inset 2px 2px 5px rgba(0, 0, 0, 0.5), inset -1px -1px 3px rgba(255, 255, 255, 0.05);
        }
        .material-symbols-outlined {
            font-variation-settings: 'FILL' 0, 'wght' 400, 'GRAD' 0, 'opsz' 24;
        }
        ::-webkit-scrollbar {
            width: 8px;
        }
        ::-webkit-scrollbar-track {
            background: #0b1326;
        }
        ::-webkit-scrollbar-thumb {
            background: #2d3449;
            border-radius: 4px;
        }
    </style>
</head>
<body class="bg-background text-on-background font-body-md min-h-screen">
<!-- Desktop Sidebar (Fixed) -->
<aside class="fixed left-0 top-0 h-full w-[260px] bg-surface-container-low flex flex-col py-lg shadow-[20px_0_40px_rgba(0,0,0,0.4)] z-50">
<div class="px-xl mb-xl">
<h1 class="font-display-md text-display-md font-bold text-primary">Gestão Escolar</h1>
<p class="font-body-sm text-on-surface-variant opacity-70">Administração Central</p>
</div>
<nav class="flex-1 flex flex-col space-y-1 pr-md">
<a class="flex items-center gap-md px-xl py-md text-on-surface-variant hover:text-on-surface hover:bg-surface-container-high transition-all group" href="../dashboard_school_manager_refatorado/code.html">
<span class="material-symbols-outlined">dashboard</span>
<span class="font-body-md">Dashboard</span>
</a>
<a class="flex items-center gap-md px-xl py-md text-on-surface-variant hover:text-on-surface hover:bg-surface-container-high transition-all" href="../gest_o_de_alunos_school_manager/code.html">
<span class="material-symbols-outlined">group</span>
<span class="font-body-md">Alunos</span>
</a>
<a class="flex items-center gap-md px-xl py-md bg-primary text-on-primary rounded-r-full shadow-[0_4px_12px_rgba(59,130,246,0.3)] transition-all" href="../financeiro_pagamentos_school_manager/code.html">
<span class="material-symbols-outlined">account_balance_wallet</span>
<span class="font-body-md">Financeiro</span>
</a>
<a class="flex items-center gap-md px-xl py-md text-on-surface-variant hover:text-on-surface hover:bg-surface-container-high transition-all" href="../relat_rios_administrativos_school_manager/code.html">
<span class="material-symbols-outlined">analytics</span>
<span class="font-body-md">Relatórios</span>
</a>
<a class="flex items-center gap-md px-xl py-md text-on-surface-variant hover:text-on-surface hover:bg-surface-container-high transition-all" href="../escola_classes_school_manager/code.html">
<span class="material-symbols-outlined">school</span>
<span class="font-body-md">Escola</span>
</a>
<a class="flex items-center gap-md px-xl py-md text-on-surface-variant hover:text-on-surface hover:bg-surface-container-high transition-all" href="../configura_es_do_sistema_school_manager/code.html">
<span class="material-symbols-outlined">settings</span>
<span class="font-body-md">Configurações</span>
</a>
</nav>
<div class="px-xl mt-auto">
<div class="p-md rounded-xl bg-surface-container-high neo-glass">
<p class="font-label-muted text-label-muted text-secondary mb-xs">STATUS DO SISTEMA</p>
<div class="flex items-center gap-xs">
<span class="w-2 h-2 rounded-full bg-green-500 animate-pulse"></span>
<span class="font-body-sm text-on-surface">Servidor Online</span>
</div>
</div>
</div>
</aside>
<!-- Top App Bar -->
<header class="fixed top-0 right-0 w-[calc(100%-260px)] h-16 bg-surface flex justify-between items-center px-xl shadow-sm border-b border-outline-variant/20 z-40">
<div class="flex items-center gap-md">
<span class="material-symbols-outlined text-primary">account_balance_wallet</span>
<h2 class="font-headline-sm text-headline-sm font-semibold text-on-surface">Histórico de Fechamentos</h2>
</div>
<div class="flex items-center gap-xl">
<div class="relative group">
<span class="absolute left-md top-1/2 -translate-y-1/2 material-symbols-outlined text-on-surface-variant text-sm">search</span>
<input class="bg-surface-container-lowest border-outline-variant/30 rounded-full pl-xl pr-md py-xs text-body-sm focus:ring-2 focus:ring-primary w-64 transition-all" placeholder="Buscar transação..." type="text"/>
</div>
<div class="flex items-center gap-md">
<button class="material-symbols-outlined p-xs hover:bg-surface-container-highest rounded-full transition-all text-on-surface-variant">notifications</button>
<div class="flex items-center gap-sm">
<div class="text-right hidden lg:block">
<p class="font-body-sm font-bold text-on-surface leading-none">Admin Principal</p>
<p class="font-label-muted text-label-muted text-on-surface-variant">Tesouraria</p>
</div>
<span class="material-symbols-outlined text-display-md text-primary">account_circle</span>
</div>
</div>
</div>
</header>
<!-- Main Content Canvas -->
<main class="ml-[260px] pt-16 pb-12 px-xl min-h-screen"><div class="max-w-7xl mx-auto space-y-lg py-xl"><div class="flex flex-col md:flex-row md:items-center justify-between gap-md mb-xl"><div><h3 class="font-display-md text-display-md font-bold text-on-surface mb-xs">Histórico de Fechamentos</h3><p class="font-body-sm text-on-surface-variant">Consulte o registro de encerramentos de caixa anteriores.</p></div><div class="flex items-center gap-sm"><button class="bg-primary text-on-primary px-lg py-md rounded-xl font-label-bold flex items-center gap-md shadow-lg hover:scale-[1.02] transition-all"><span class="material-symbols-outlined">calendar_today</span> FILTRAR POR DATA</button></div></div><div class="grid grid-cols-1 md:grid-cols-4 gap-lg mb-lg"><div class="p-md rounded-xl bg-surface-container neo-glass border border-outline-variant/10"><label class="font-label-bold text-on-surface-variant/70 mb-xs block uppercase text-[10px]">Operador</label><select class="w-full bg-surface-container-lowest border-outline-variant/30 rounded-lg text-body-sm text-on-surface focus:ring-primary"><option>Todos os Operadores</option><option>Admin Principal</option><option>Tesouraria 01</option></select></div><div class="p-md rounded-xl bg-surface-container neo-glass border border-outline-variant/10"><label class="font-label-bold text-on-surface-variant/70 mb-xs block uppercase text-[10px]">Status</label><select class="w-full bg-surface-container-lowest border-outline-variant/30 rounded-lg text-body-sm text-on-surface focus:ring-primary"><option>Todos</option><option>Concluído</option><option>Divergente</option></select></div></div><div class="bg-surface-container rounded-xl shadow-xl overflow-hidden neo-glass flex flex-col"><div class="overflow-x-auto"><table class="w-full text-left border-collapse"><thead class="bg-surface-container-lowest text-on-surface-variant font-label-bold uppercase text-[11px] tracking-widest"><tr><th class="px-xl py-md border-b border-outline-variant/10">Data/Hora</th><th class="px-xl py-md border-b border-outline-variant/10">Operador</th><th class="px-xl py-md border-b border-outline-variant/10">Saldo Inicial</th><th class="px-xl py-md border-b border-outline-variant/10">Entradas</th><th class="px-xl py-md border-b border-outline-variant/10">Saídas</th><th class="px-xl py-md border-b border-outline-variant/10">Saldo Final</th><th class="px-xl py-md border-b border-outline-variant/10">Status</th><th class="px-xl py-md border-b border-outline-variant/10 text-right">Ações</th></tr></thead><tbody class="divide-y divide-outline-variant/10"><tr class="hover:bg-surface-container-highest/40 transition-all cursor-pointer group"><td class="px-xl py-md font-body-sm">15/05/2024 18:30</td><td class="px-xl py-md font-body-sm">Admin Principal</td><td class="px-xl py-md font-data-numeric text-sm">75.000,00</td><td class="px-xl py-md font-data-numeric text-sm text-green-400">+142.350,00</td><td class="px-xl py-md font-data-numeric text-sm text-red-400">-18.200,00</td><td class="px-xl py-md font-data-numeric text-sm">199.150,00</td><td class="px-xl py-md"><span class="px-sm py-1 rounded-full bg-green-500/10 text-green-400 text-[10px] font-bold">CONCLUÍDO</span></td><td class="px-xl py-md text-right"><div class="flex justify-end gap-xs"><button class="p-1 hover:bg-primary/20 rounded transition-colors material-symbols-outlined text-primary" title="Ver Detalhes">visibility</button><button class="p-1 hover:bg-primary/20 rounded transition-colors material-symbols-outlined text-primary" title="Re-imprimir">print</button></div></td></tr><tr class="hover:bg-surface-container-highest/40 transition-all cursor-pointer group"><td class="px-xl py-md font-body-sm">14/05/2024 18:15</td><td class="px-xl py-md font-body-sm">Tesouraria 01</td><td class="px-xl py-md font-data-numeric text-sm">50.000,00</td><td class="px-xl py-md font-data-numeric text-sm text-green-400">+95.000,00</td><td class="px-xl py-md font-data-numeric text-sm text-red-400">-12.000,00</td><td class="px-xl py-md font-data-numeric text-sm">133.000,00</td><td class="px-xl py-md"><span class="px-sm py-1 rounded-full bg-red-500/10 text-red-400 text-[10px] font-bold">DIVERGENTE</span></td><td class="px-xl py-md text-right"><div class="flex justify-end gap-xs"><button class="p-1 hover:bg-primary/20 rounded transition-colors material-symbols-outlined text-primary">visibility</button><button class="p-1 hover:bg-primary/20 rounded transition-colors material-symbols-outlined text-primary">print</button></div></td></tr></tbody></table></div><div class="mt-auto px-xl py-md border-t border-outline-variant/10 bg-surface-container-lowest/30 flex justify-between items-center"><span class="text-body-sm text-on-surface-variant">Mostrando 2 de 48 fechamentos</span><div class="flex gap-xs"><button class="p-1 rounded bg-surface-container hover:bg-primary/20 text-on-surface-variant material-symbols-outlined text-sm">chevron_left</button><button class="p-1 rounded bg-primary text-on-primary text-[12px] px-sm font-bold">1</button><button class="p-1 rounded bg-surface-container hover:bg-primary/20 text-on-surface-variant text-[12px] px-sm font-bold">2</button><button class="p-1 rounded bg-surface-container hover:bg-primary/20 text-on-surface-variant material-symbols-outlined text-sm">chevron_right</button></div></div></div></div></main>
<!-- Footer Status -->
<footer class="fixed bottom-0 right-0 w-[calc(100%-260px)] h-8 bg-surface-container-lowest flex justify-between items-center px-xl z-40 border-t border-outline-variant/10">
<div class="flex items-center gap-lg">
<p class="font-label-muted text-label-muted text-on-surface-variant/60">v2.4.1 | Status do Backup: Sincronizado</p>
<div class="flex items-center gap-xs">
<span class="w-1.5 h-1.5 rounded-full bg-blue-500"></span>
<span class="font-label-muted text-label-muted text-on-surface-variant/60">Última sync: há 2 min</span>
</div>
</div>
<div class="flex items-center gap-lg">
<a class="font-label-muted text-label-muted text-on-surface-variant/60 hover:text-primary transition-colors" href="#">Suporte</a>
<a class="font-label-muted text-label-muted text-on-surface-variant/60 hover:text-primary transition-colors" href="#">Documentação</a>
</div>
</footer>
<script>
        // Micro-interactions and state handling
        document.addEventListener('DOMContentLoaded', () => {
            const cashierBtn = document.querySelector('button.from-red-500');
            cashierBtn.addEventListener('click', () => {
                if(confirm('Deseja realmente fechar o caixa desta sessão? Todos os valores serão consolidados.')) {
                    alert('Caixa fechado com sucesso! Gerando relatório de encerramento...');
                    window.location.reload();
                }
            });

            // Smooth entrance for cards
            const cards = document.querySelectorAll('.neo-glass');
            cards.forEach((card, index) => {
                card.style.opacity = '0';
                card.style.transform = 'translateY(20px)';
                setTimeout(() => {
                    card.style.transition = 'all 0.5s cubic-bezier(0.16, 1, 0.3, 1)';
                    card.style.opacity = '1';
                    card.style.transform = 'translateY(0)';
                }, 100 * index);
            });
        });
    </script>
</body></html>





<!DOCTYPE html>

<html class="dark" lang="pt-PT"><head>
<meta charset="utf-8"/>
<meta content="width=device-width, initial-scale=1.0" name="viewport"/>
<title>Fechamento de Caixa - Gestão Escolar</title>
<script src="https://cdn.tailwindcss.com?plugins=forms,container-queries"></script>
<link href="https://fonts.googleapis.com/css2?family=Inter:wght@400;500;600;700&amp;family=Poppins:wght@500;600;700;800&amp;display=swap" rel="stylesheet"/>
<link href="https://fonts.googleapis.com/css2?family=Material+Symbols+Outlined:wght,FILL@100..700,0..1&amp;display=swap" rel="stylesheet"/>
<link href="https://fonts.googleapis.com/css2?family=Material+Symbols+Outlined:wght,FILL@100..700,0..1&amp;display=swap" rel="stylesheet"/>
<style>
        .material-symbols-outlined {
            font-variation-settings: 'FILL' 0, 'wght' 400, 'GRAD' 0, 'opsz' 24;
        }
        
        .neo-pressed {
            box-shadow: inset 2px 2px 5px rgba(0, 0, 0, 0.4), inset -1px -1px 3px rgba(255, 255, 255, 0.05);
            background-color: #111827;
        }

        .neo-card {
            background: linear-gradient(145deg, #171f33, #0b1326);
            box-shadow: 20px 20px 60px #080e1b, -5px -5px 20px rgba(255, 255, 255, 0.02);
            border: 1px solid rgba(255, 255, 255, 0.05);
        }

        .glass-accent {
            backdrop-filter: blur(12px);
            background: rgba(173, 198, 255, 0.05);
            border: 1px solid rgba(173, 198, 255, 0.1);
        }

        ::-webkit-scrollbar {
            width: 8px;
        }
        ::-webkit-scrollbar-track {
            background: #0b1326;
        }
        ::-webkit-scrollbar-thumb {
            background: #2d3449;
            border-radius: 10px;
        }
        ::-webkit-scrollbar-thumb:hover {
            background: #3B82F6;
        }
    </style>
<script id="tailwind-config">
        tailwind.config = {
            darkMode: "class",
            theme: {
                extend: {
                    "colors": {
                        "on-secondary": "#263143",
                        "on-tertiary-fixed": "#001c39",
                        "error": "#ffb4ab",
                        "surface-container": "#171f33",
                        "error-container": "#93000a",
                        "on-surface-variant": "#c2c6d6",
                        "surface-bright": "#31394d",
                        "surface-tint": "#adc6ff",
                        "on-tertiary-container": "#002a51",
                        "on-primary-fixed-variant": "#004395",
                        "tertiary-fixed": "#d4e3ff",
                        "surface-dim": "#0b1326",
                        "outline": "#8c909f",
                        "secondary": "#bcc7de",
                        "on-tertiary": "#00315d",
                        "surface-container-low": "#131b2e",
                        "outline-variant": "#424754",
                        "secondary-fixed-dim": "#bcc7de",
                        "on-primary-container": "#00285d",
                        "on-background": "#dae2fd",
                        "surface-container-lowest": "#060e20",
                        "surface": "#0b1326",
                        "tertiary-fixed-dim": "#a4c9ff",
                        "on-primary": "#002e6a",
                        "primary-container": "#4d8eff",
                        "inverse-surface": "#dae2fd",
                        "surface-container-high": "#222a3d",
                        "on-error-container": "#ffdad6",
                        "on-secondary-fixed-variant": "#3c475a",
                        "background": "#0b1326",
                        "on-tertiary-fixed-variant": "#004883",
                        "on-error": "#690005",
                        "secondary-fixed": "#d8e3fb",
                        "primary-fixed": "#d8e2ff",
                        "on-primary-fixed": "#001a42",
                        "tertiary-container": "#4c93e7",
                        "primary": "#adc6ff",
                        "tertiary": "#a4c9ff",
                        "surface-container-highest": "#2d3449",
                        "surface-variant": "#2d3449",
                        "on-surface": "#dae2fd",
                        "secondary-container": "#3e495d",
                        "on-secondary-container": "#aeb9d0",
                        "inverse-primary": "#005ac2",
                        "primary-fixed-dim": "#adc6ff",
                        "on-secondary-fixed": "#111c2d",
                        "inverse-on-surface": "#283044"
                    },
                    "borderRadius": {
                        "DEFAULT": "0.25rem",
                        "lg": "0.5rem",
                        "xl": "0.75rem",
                        "full": "9999px"
                    },
                    "spacing": {
                        "sidebar_width": "260px",
                        "lg": "24px",
                        "base": "4px",
                        "sm": "12px",
                        "xl": "32px",
                        "md": "16px",
                        "xs": "8px",
                        "gutter": "20px"
                    },
                    "fontFamily": {
                        "body-lg": ["Inter"],
                        "label-muted": ["Inter"],
                        "display-lg": ["Poppins"],
                        "label-bold": ["Inter"],
                        "data-numeric": ["Inter"],
                        "headline-sm": ["Poppins"],
                        "body-md": ["Inter"],
                        "body-sm": ["Inter"],
                        "display-md": ["Poppins"]
                    },
                    "fontSize": {
                        "body-lg": ["16px", {"lineHeight": "1.5", "fontWeight": "400"}],
                        "label-muted": ["12px", {"lineHeight": "1", "fontWeight": "400"}],
                        "display-lg": ["32px", {"lineHeight": "1.2", "letterSpacing": "-0.02em", "fontWeight": "600"}],
                        "label-bold": ["12px", {"lineHeight": "1", "letterSpacing": "0.05em", "fontWeight": "600"}],
                        "data-numeric": ["20px", {"lineHeight": "1", "letterSpacing": "-0.01em", "fontWeight": "700"}],
                        "headline-sm": ["18px", {"lineHeight": "1.4", "fontWeight": "500"}],
                        "body-md": ["14px", {"lineHeight": "1.5", "fontWeight": "400"}],
                        "body-sm": ["13px", {"lineHeight": "1.5", "fontWeight": "400"}],
                        "display-md": ["24px", {"lineHeight": "1.3", "fontWeight": "600"}]
                    }
                },
            },
        }
    </script>
</head>
<body class="bg-background text-on-surface font-body-md selection:bg-primary/30 overflow-hidden">
<!-- Desktop Sidebar Anchor -->
<aside class="fixed left-0 top-0 h-full w-[260px] bg-surface-container-low flex flex-col py-lg shadow-[20px_0_40px_rgba(0,0,0,0.4)] z-40">
<div class="px-xl mb-xl">
<h1 class="font-display-md text-display-md font-bold text-primary">Gestão Escolar</h1>
<p class="font-label-muted text-label-muted text-on-surface-variant/60">Administração Central</p>
</div>
<nav class="flex-1 px-md space-y-xs">
<!-- Financial (Financeiro) is likely where Cashier lives -->
<div class="flex items-center gap-md px-md py-sm cursor-pointer transition-all hover:bg-surface-container-high rounded-lg text-on-surface-variant">
<span class="material-symbols-outlined">dashboard</span>
<span class="font-body-md">Dashboard</span>
</div>
<div class="flex items-center gap-md px-md py-sm cursor-pointer transition-all hover:bg-surface-container-high rounded-lg text-on-surface-variant">
<span class="material-symbols-outlined">group</span>
<span class="font-body-md">Alunos</span>
</div>
<!-- Active State Navigation Logic: Maps to "Financeiro" as this is a cashier closing task -->
<div class="flex items-center gap-md px-md py-sm cursor-pointer transition-all bg-primary text-on-primary rounded-r-full shadow-[0_4px_12px_rgba(59,130,246,0.3)]">
<span class="material-symbols-outlined" style="font-variation-settings: 'FILL' 1;">account_balance_wallet</span>
<span class="font-body-md">Financeiro</span>
</div>
<div class="flex items-center gap-md px-md py-sm cursor-pointer transition-all hover:bg-surface-container-high rounded-lg text-on-surface-variant">
<span class="material-symbols-outlined">analytics</span>
<span class="font-body-md">Relatórios</span>
</div>
<div class="flex items-center gap-md px-md py-sm cursor-pointer transition-all hover:bg-surface-container-high rounded-lg text-on-surface-variant">
<span class="material-symbols-outlined">school</span>
<span class="font-body-md">Escola</span>
</div>
<div class="flex items-center gap-md px-md py-sm cursor-pointer transition-all hover:bg-surface-container-high rounded-lg text-on-surface-variant">
<span class="material-symbols-outlined">settings</span>
<span class="font-body-md">Configurações</span>
</div>
</nav>
</aside>
<!-- Top App Bar -->
<header class="fixed top-0 right-0 w-[calc(100%-260px)] h-16 bg-surface flex justify-between items-center px-xl border-b border-outline-variant/20 z-30">
<h2 class="font-headline-sm text-headline-sm font-semibold text-on-surface">Gestão de Caixa</h2>
<div class="flex items-center gap-md">
<div class="relative group">
<div class="flex items-center bg-surface-container-highest px-md py-xs rounded-full border border-outline-variant/30 focus-within:ring-2 focus-within:ring-primary transition-all">
<span class="material-symbols-outlined text-on-surface-variant">search</span>
<input class="bg-transparent border-none focus:ring-0 text-body-sm w-48" placeholder="Procurar transação..." type="text"/>
</div>
</div>
<button class="p-xs hover:bg-surface-container-highest rounded-full transition-all text-on-surface-variant">
<span class="material-symbols-outlined">notifications</span>
</button>
<button class="flex items-center gap-xs p-xs hover:bg-surface-container-highest rounded-full transition-all text-on-surface-variant">
<span class="material-symbols-outlined">account_circle</span>
</button>
</div>
</header>
<!-- Main Content Canvas (Background of Modal) -->
<main class="ml-[260px] pt-16 p-xl min-h-screen">
<!-- Summary Cards (Blurred background effect) -->
<div class="grid grid-cols-12 gap-lg mb-xl opacity-40 grayscale-[0.5] blur-[1px] pointer-events-none">
<div class="col-span-3 neo-card p-lg rounded-xl">
<div class="flex justify-between items-start mb-md">
<div class="p-xs bg-primary-container/20 rounded-lg">
<span class="material-symbols-outlined text-primary">payments</span>
</div>
<span class="text-label-muted text-green-400 font-label-bold">+12%</span>
</div>
<div class="font-label-muted text-on-surface-variant uppercase mb-xs">Saldo Atual</div>
<div class="font-data-numeric text-data-numeric">1.450.000,00 Kz</div>
</div>
<div class="col-span-3 neo-card p-lg rounded-xl">
<div class="flex justify-between items-start mb-md">
<div class="p-xs bg-tertiary-container/20 rounded-lg">
<span class="material-symbols-outlined text-tertiary">trending_up</span>
</div>
<span class="text-label-muted text-on-surface-variant font-label-bold">Hoje</span>
</div>
<div class="font-label-muted text-on-surface-variant uppercase mb-xs">Entradas</div>
<div class="font-data-numeric text-data-numeric">850.200,00 Kz</div>
</div>
<div class="col-span-3 neo-card p-lg rounded-xl">
<div class="flex justify-between items-start mb-md">
<div class="p-xs bg-error-container/20 rounded-lg">
<span class="material-symbols-outlined text-error">trending_down</span>
</div>
<span class="text-label-muted text-on-surface-variant font-label-bold">Hoje</span>
</div>
<div class="font-label-muted text-on-surface-variant uppercase mb-xs">Saídas</div>
<div class="font-data-numeric text-data-numeric">120.450,00 Kz</div>
</div>
<div class="col-span-3 neo-card p-lg rounded-xl">
<div class="flex justify-between items-start mb-md">
<div class="p-xs bg-secondary-container/20 rounded-lg">
<span class="material-symbols-outlined text-secondary">history</span>
</div>
</div>
<div class="font-label-muted text-on-surface-variant uppercase mb-xs">Última Abertura</div>
<div class="font-body-md text-on-surface">07:45 - Operador: Admin</div>
</div>
</div>
<!-- Background Data Grid (Blurred) -->
<div class="neo-card rounded-xl overflow-hidden opacity-30 blur-[2px] pointer-events-none">
<div class="p-lg border-b border-outline-variant/20 flex justify-between items-center">
<h3 class="font-headline-sm text-headline-sm">Fluxo de Caixa Diário</h3>
</div>
<div class="overflow-x-auto">
<table class="w-full text-left">
<thead class="bg-surface-container-highest/50">
<tr>
<th class="px-lg py-md font-label-bold text-on-surface-variant uppercase tracking-wider">Hora</th>
<th class="px-lg py-md font-label-bold text-on-surface-variant uppercase tracking-wider">Descrição</th>
<th class="px-lg py-md font-label-bold text-on-surface-variant uppercase tracking-wider">Tipo</th>
<th class="px-lg py-md font-label-bold text-on-surface-variant uppercase tracking-wider">Valor</th>
</tr>
</thead>
<tbody class="divide-y divide-outline-variant/10">
<tr><td class="px-lg py-md">08:20</td><td class="px-lg py-md">Matrícula - João Silva</td><td class="px-lg py-md text-primary">Entrada</td><td class="px-lg py-md font-data-numeric">45.000,00 Kz</td></tr>
<tr><td class="px-lg py-md">09:15</td><td class="px-lg py-md">Propina Mensal - Maria Sousa</td><td class="px-lg py-md text-primary">Entrada</td><td class="px-lg py-md font-data-numeric">12.500,00 Kz</td></tr>
</tbody>
</table>
</div>
</div>
</main>
<!-- OVERLAY MODAL: Fechamento de Caixa -->
<div class="fixed inset-0 z-50 flex items-center justify-center p-xl" id="modal-container">
<!-- Backdrop Blur -->
<div class="absolute inset-0 bg-surface-dim/80 backdrop-blur-md"></div>
<!-- Modal Content -->
<div class="relative w-full max-w-5xl neo-card rounded-2xl overflow-hidden flex flex-col max-h-[921px] animate-in fade-in zoom-in duration-300">
<!-- Header -->
<div class="p-xl glass-accent flex justify-between items-start border-b border-primary/20">
<div>
<div class="flex items-center gap-md mb-xs">
<div class="bg-primary p-xs rounded-lg shadow-[0_0_15px_rgba(173,198,255,0.4)]">
<span class="material-symbols-outlined text-on-primary">lock_open</span>
</div>
<h2 class="font-display-md text-display-md text-on-surface">Fechamento de Caixa</h2>
</div>
<p class="text-on-surface-variant font-body-md">Sessão iniciada em: <span class="text-on-surface font-semibold">14 Junho 2024, 07:45:12</span></p>
</div>
<button class="p-xs hover:bg-error/10 hover:text-error transition-all rounded-full" onclick="document.getElementById('modal-container').classList.add('hidden')">
<span class="material-symbols-outlined">close</span>
</button>
</div>
<!-- Scrollable Table Body -->
<div class="flex-1 overflow-y-auto p-xl">
<div class="mb-lg flex justify-between items-end">
<h3 class="font-headline-sm text-headline-sm text-primary">Histórico Financeiro da Sessão</h3>
<div class="text-label-muted text-on-surface-variant/60 italic">Total de 14 operações processadas</div>
</div>
<div class="neo-pressed rounded-xl border border-outline-variant/30 overflow-hidden">
<table class="w-full text-left">
<thead class="bg-surface-container-high">
<tr>
<th class="px-lg py-md font-label-bold text-on-surface-variant uppercase text-[10px] tracking-widest border-b border-outline-variant/20">Hora</th>
<th class="px-lg py-md font-label-bold text-on-surface-variant uppercase text-[10px] tracking-widest border-b border-outline-variant/20">Descrição do Lançamento</th>
<th class="px-lg py-md font-label-bold text-on-surface-variant uppercase text-[10px] tracking-widest border-b border-outline-variant/20">Modo</th>
<th class="px-lg py-md font-label-bold text-on-surface-variant uppercase text-[10px] tracking-widest border-b border-outline-variant/20 text-right">Valor</th>
</tr>
</thead>
<tbody class="text-body-sm font-body-sm divide-y divide-outline-variant/10">
<tr class="hover:bg-surface-container-highest/30 transition-colors">
<td class="px-lg py-md text-on-surface-variant">08:20:45</td>
<td class="px-lg py-md font-medium">Matrícula - João Carlos Pereira (Ref: 2024/001)</td>
<td class="px-lg py-md"><span class="px-sm py-0.5 rounded-full bg-primary/10 text-primary text-[10px] font-bold uppercase">Entrada</span></td>
<td class="px-lg py-md font-data-numeric text-right text-primary">+ 45.000,00 Kz</td>
</tr>
<tr class="hover:bg-surface-container-highest/30 transition-colors">
<td class="px-lg py-md text-on-surface-variant">09:12:12</td>
<td class="px-lg py-md font-medium">Propina Junho - Maria Antonieta Sousa</td>
<td class="px-lg py-md"><span class="px-sm py-0.5 rounded-full bg-primary/10 text-primary text-[10px] font-bold uppercase">Entrada</span></td>
<td class="px-lg py-md font-data-numeric text-right text-primary">+ 12.500,00 Kz</td>
</tr>
<tr class="hover:bg-surface-container-highest/30 transition-colors">
<td class="px-lg py-md text-on-surface-variant">10:05:30</td>
<td class="px-lg py-md font-medium">Compra de material de limpeza (Papelaria Central)</td>
<td class="px-lg py-md"><span class="px-sm py-0.5 rounded-full bg-error/10 text-error text-[10px] font-bold uppercase">Saída</span></td>
<td class="px-lg py-md font-data-numeric text-right text-error">- 8.450,00 Kz</td>
</tr>
<tr class="hover:bg-surface-container-highest/30 transition-colors">
<td class="px-lg py-md text-on-surface-variant">11:30:15</td>
<td class="px-lg py-md font-medium">Uniforme Escolar (Kit Completo) - Pedro Ndala</td>
<td class="px-lg py-md"><span class="px-sm py-0.5 rounded-full bg-primary/10 text-primary text-[10px] font-bold uppercase">Entrada</span></td>
<td class="px-lg py-md font-data-numeric text-right text-primary">+ 22.000,00 Kz</td>
</tr>
<tr class="hover:bg-surface-container-highest/30 transition-colors">
<td class="px-lg py-md text-on-surface-variant">13:45:00</td>
<td class="px-lg py-md font-medium">Transporte Escolar - Mês de Junho</td>
<td class="px-lg py-md"><span class="px-sm py-0.5 rounded-full bg-primary/10 text-primary text-[10px] font-bold uppercase">Entrada</span></td>
<td class="px-lg py-md font-data-numeric text-right text-primary">+ 15.000,00 Kz</td>
</tr>
<tr class="hover:bg-surface-container-highest/30 transition-colors">
<td class="px-lg py-md text-on-surface-variant">15:20:10</td>
<td class="px-lg py-md font-medium">Pagamento Manutenção Ar-Condicionado (Sala 04)</td>
<td class="px-lg py-md"><span class="px-sm py-0.5 rounded-full bg-error/10 text-error text-[10px] font-bold uppercase">Saída</span></td>
<td class="px-lg py-md font-data-numeric text-right text-error">- 12.000,00 Kz</td>
</tr>
<tr class="hover:bg-surface-container-highest/30 transition-colors">
<td class="px-lg py-md text-on-surface-variant">16:55:04</td>
<td class="px-lg py-md font-medium">Matrícula - Ana Maria Fernandes</td>
<td class="px-lg py-md"><span class="px-sm py-0.5 rounded-full bg-primary/10 text-primary text-[10px] font-bold uppercase">Entrada</span></td>
<td class="px-lg py-md font-data-numeric text-right text-primary">+ 45.000,00 Kz</td>
</tr>
</tbody>
</table>
</div>
</div>
<!-- Footer Summary & Actions -->
<div class="p-xl bg-surface-container-high border-t border-outline-variant/20">
<div class="grid grid-cols-12 gap-lg mb-xl">
<div class="col-span-4 p-md rounded-xl glass-accent">
<div class="font-label-bold text-on-surface-variant text-[10px] uppercase mb-1">Total Entradas</div>
<div class="font-data-numeric text-primary text-display-md">+ 174.500,00 Kz</div>
</div>
<div class="col-span-4 p-md rounded-xl glass-accent">
<div class="font-label-bold text-on-surface-variant text-[10px] uppercase mb-1">Total Saídas</div>
<div class="font-data-numeric text-error text-display-md">- 20.450,00 Kz</div>
</div>
<div class="col-span-4 p-md rounded-xl bg-primary shadow-[inset_0_2px_10px_rgba(255,255,255,0.2)]">
<div class="font-label-bold text-on-primary/70 text-[10px] uppercase mb-1">Saldo Final p/ Depósito</div>
<div class="font-data-numeric text-on-primary text-display-md">154.050,00 Kz</div>
</div>
</div>
<div class="flex justify-between items-center">
<div class="flex items-center gap-md text-on-surface-variant/70">
<span class="material-symbols-outlined" style="font-variation-settings: 'FILL' 1;">verified_user</span>
<span class="text-body-sm italic">Conferido por: Admin do Sistema (Autenticação biométrica ativa)</span>
</div>
<div class="flex gap-md">
<button class="px-lg py-md rounded-lg font-label-bold bg-surface-container-highest text-on-surface border border-outline-variant/30 hover:bg-surface-bright transition-all" onclick="document.getElementById('modal-container').classList.add('hidden')">
                            CANCELAR
                        </button>
<button class="px-lg py-md rounded-lg font-label-bold bg-gradient-to-r from-primary to-primary-container text-on-primary flex items-center gap-md shadow-[0_4px_12px_rgba(59,130,246,0.3)] hover:scale-[1.02] active:scale-[0.98] transition-all">
<span class="material-symbols-outlined">picture_as_pdf</span>
                            CONFIRMAR FECHAMENTO E GERAR PDF
                        </button>
</div>
</div>
</div>
</div>
</div>
<!-- Footer System Info -->
<footer class="fixed bottom-0 right-0 w-[calc(100%-260px)] h-8 bg-surface-container-lowest flex justify-between items-center px-xl z-20">
<div class="font-label-muted text-label-muted text-on-surface-variant/60">v2.4.1 | Status do Backup: Sincronizado</div>
<div class="flex gap-lg">
<a class="font-label-muted text-label-muted text-on-surface-variant/60 hover:text-primary transition-colors" href="#">Suporte</a>
<a class="font-label-muted text-label-muted text-on-surface-variant/60 hover:text-primary transition-colors" href="#">Documentação</a>
</div>
</footer>
<script>
        // Micro-interaction for buttons
        document.querySelectorAll('button').forEach(button => {
            button.addEventListener('mousedown', () => button.classList.add('scale-95'));
            button.addEventListener('mouseup', () => button.classList.remove('scale-95</script></body></html>




<!DOCTYPE html>

<html class="dark" lang="pt-BR"><head>
<meta charset="utf-8"/>
<meta content="width=device-width, initial-scale=1.0" name="viewport"/>
<title>School Manager - Perfil do Aluno</title>
<script src="https://cdn.tailwindcss.com?plugins=forms,container-queries"></script>
<link href="https://fonts.googleapis.com/css2?family=Poppins:wght@400;500;600;700&amp;family=Inter:wght@400;600;700&amp;display=swap" rel="stylesheet"/>
<link href="https://fonts.googleapis.com/css2?family=Material+Symbols+Outlined:wght,FILL@100..700,0..1&amp;display=swap" rel="stylesheet"/>
<script id="tailwind-config">
      tailwind.config = {
        darkMode: "class",
        theme: {
          extend: {
            "colors": {
                    "on-tertiary-container": "#002a51",
                    "on-primary-container": "#00285d",
                    "tertiary-container": "#4c93e7",
                    "on-tertiary-fixed-variant": "#004883",
                    "primary": "#adc6ff",
                    "secondary": "#bcc7de",
                    "inverse-on-surface": "#283044",
                    "surface-tint": "#adc6ff",
                    "surface-container-lowest": "#060e20",
                    "secondary-fixed": "#d8e3fb",
                    "on-error-container": "#ffdad6",
                    "on-primary-fixed-variant": "#004395",
                    "tertiary-fixed": "#d4e3ff",
                    "on-surface": "#dae2fd",
                    "surface-dim": "#0b1326",
                    "on-surface-variant": "#c2c6d6",
                    "surface-variant": "#2d3449",
                    "error-container": "#93000a",
                    "surface-bright": "#31394d",
                    "on-primary-fixed": "#001a42",
                    "on-secondary-fixed-variant": "#3c475a",
                    "surface-container-high": "#222a3d",
                    "surface": "#0b1326",
                    "primary-fixed": "#d8e2ff",
                    "on-tertiary-fixed": "#001c39",
                    "secondary-fixed-dim": "#bcc7de",
                    "tertiary-fixed-dim": "#a4c9ff",
                    "surface-container": "#171f33",
                    "on-tertiary": "#00315d",
                    "on-primary": "#002e6a",
                    "background": "#0b1326",
                    "tertiary": "#a4c9ff",
                    "on-secondary-container": "#aeb9d0",
                    "surface-container-low": "#131b2e",
                    "on-secondary-fixed": "#111c2d",
                    "on-secondary": "#263143",
                    "primary-fixed-dim": "#adc6ff",
                    "surface-container-highest": "#2d3449",
                    "inverse-surface": "#dae2fd",
                    "primary-container": "#4d8eff",
                    "error": "#ffb4ab",
                    "on-background": "#dae2fd",
                    "outline": "#8c909f",
                    "on-error": "#690005",
                    "outline-variant": "#424754",
                    "secondary-container": "#3e495d",
                    "inverse-primary": "#005ac2"
            },
            "borderRadius": {
                    "DEFAULT": "0.25rem",
                    "lg": "0.5rem",
                    "xl": "0.75rem",
                    "full": "9999px"
            },
            "spacing": {
                    "xs": "8px",
                    "xl": "32px",
                    "md": "16px",
                    "sm": "12px",
                    "gutter": "20px",
                    "sidebar_width": "260px",
                    "lg": "24px",
                    "base": "4px"
            },
            "fontFamily": {
                    "display-lg": ["Poppins"],
                    "data-numeric": ["Inter"],
                    "body-md": ["Inter"],
                    "body-sm": ["Inter"],
                    "body-lg": ["Inter"],
                    "headline-sm": ["Poppins"],
                    "label-muted": ["Inter"],
                    "display-md": ["Poppins"],
                    "label-bold": ["Inter"]
            },
            "fontSize": {
                    "display-lg": ["32px", {"lineHeight": "1.2", "letterSpacing": "-0.02em", "fontWeight": "600"}],
                    "data-numeric": ["20px", {"lineHeight": "1", "letterSpacing": "-0.01em", "fontWeight": "700"}],
                    "body-md": ["14px", {"lineHeight": "1.5", "fontWeight": "400"}],
                    "body-sm": ["13px", {"lineHeight": "1.5", "fontWeight": "400"}],
                    "body-lg": ["16px", {"lineHeight": "1.5", "fontWeight": "400"}],
                    "headline-sm": ["18px", {"lineHeight": "1.4", "fontWeight": "500"}],
                    "label-muted": ["12px", {"lineHeight": "1", "fontWeight": "400"}],
                    "display-md": ["24px", {"lineHeight": "1.3", "fontWeight": "600"}],
                    "label-bold": ["12px", {"lineHeight": "1", "letterSpacing": "0.05em", "fontWeight": "600"}]
            }
          },
        },
      }
    </script>
<style>
        body {
            background-color: #0b1326;
            color: #dae2fd;
            -webkit-font-smoothing: antialiased;
        }
        .neo-card {
            background: #131b2e;
            box-shadow: 
                inset 1px 1px 0px rgba(255, 255, 255, 0.05),
                0 10px 30px -5px rgba(0, 0, 0, 0.5);
            border: 1px solid rgba(66, 71, 84, 0.2);
        }
        .neo-inset {
            background: #0b1326;
            box-shadow: inset 2px 2px 5px rgba(0,0,0,0.4);
            border: 1px solid rgba(66, 71, 84, 0.4);
        }
        .active-nav-glow {
            box-shadow: 0 0 15px rgba(77, 142, 255, 0.3);
        }
        .material-symbols-outlined {
            font-variation-settings: 'FILL' 0, 'wght' 400, 'GRAD' 0, 'opsz' 24;
        }
        ::-webkit-scrollbar {
            width: 8px;
        }
        ::-webkit-scrollbar-track {
            background: #0b1326;
        }
        ::-webkit-scrollbar-thumb {
            background: #2d3449;
            border-radius: 4px;
        }
        ::-webkit-scrollbar-thumb:hover {
            background: #3e495d;
        }
    </style>
</head>
<body class="flex min-h-screen">
<!-- Side Navigation Bar -->
<aside class="w-[260px] h-screen flex flex-col border-r border-outline-variant/10 fixed left-0 top-0 p-md space-y-lg bg-surface-container-low shadow-xl z-50">
<div class="flex items-center gap-sm px-xs py-sm">
<div class="w-10 h-10 rounded-lg bg-primary-container flex items-center justify-center shadow-lg active:scale-95 transition-transform">
<span class="material-symbols-outlined text-on-primary-container" style="font-variation-settings: 'FILL' 1;">school</span>
</div>
<div>
<h1 class="text-headline-sm font-display-lg text-on-surface tracking-tight leading-none">School Manager</h1>
<p class="font-label-muted text-label-muted mt-1 uppercase tracking-widest text-[10px]">Gestão Escolar</p>
</div>
</div>
<nav class="flex-1 space-y-xs overflow-y-auto">
<a class="flex items-center gap-md p-sm text-on-surface-variant hover:text-on-surface hover:bg-surface-variant/50 transition-all duration-200 rounded-lg" href="../dashboard_school_manager_refatorado/code.html">
<span class="material-symbols-outlined">dashboard</span>
<span class="font-label-bold text-label-bold">Dashboard</span>
</a>
<a class="flex items-center gap-md p-sm bg-primary-container text-on-primary-container rounded-lg font-label-bold shadow-[0_4px_12px_rgba(77,142,255,0.3)] active:scale-[0.98] transition-transform" href="../gest_o_de_alunos_school_manager/code.html">
<span class="material-symbols-outlined" style="font-variation-settings: 'FILL' 1;">group</span>
<span class="font-label-bold text-label-bold">Alunos</span>
</a>
<a class="flex items-center gap-md p-sm text-on-surface-variant hover:text-on-surface hover:bg-surface-variant/50 transition-all duration-200 rounded-lg" href="../financeiro_pagamentos_school_manager/code.html">
<span class="material-symbols-outlined">payments</span>
<span class="font-label-bold text-label-bold">Financeiro</span>
</a>
<a class="flex items-center gap-md p-sm text-on-surface-variant hover:text-on-surface hover:bg-surface-variant/50 transition-all duration-200 rounded-lg" href="../relat_rios_administrativos_school_manager/code.html">
<span class="material-symbols-outlined">analytics</span>
<span class="font-label-bold text-label-bold">Relatórios</span>
</a>
<a class="flex items-center gap-md p-sm text-on-surface-variant hover:text-on-surface hover:bg-surface-variant/50 transition-all duration-200 rounded-lg" href="../escola_classes_school_manager/code.html">
<span class="material-symbols-outlined">apartment</span>
<span class="font-label-bold text-label-bold">Escola</span>
</a>
<a class="flex items-center gap-md p-sm text-on-surface-variant hover:text-on-surface hover:bg-surface-variant/50 transition-all duration-200 rounded-lg" href="../configura_es_do_sistema_school_manager/code.html">
<span class="material-symbols-outlined">settings</span>
<span class="font-label-bold text-label-bold">Configurações</span>
</a>
</nav>
<div class="pt-md border-t border-outline-variant/10 space-y-xs">
<div class="flex items-center gap-md p-sm rounded-lg bg-surface-container-high transition-all">
<img class="w-8 h-8 rounded-full border-2 border-primary-container shadow-sm object-cover" data-alt="Close up professional portrait of a school administrative secretary, female, with a warm friendly smile, wearing business attire, soft cinematic lighting, corporate professional environment background, dark blue and gray color palette." src="https://lh3.googleusercontent.com/aida-public/AB6AXuDkVV4Rz0l5ncaE_KIZyMyL4O2ObcZAoEbf8tSvLKsgeIq1pQZR4X0E8ajpypoixKOB-yZPAAK0oT7TVx0Ga1mkmh0bCkb-pJKGp-J8fsbYf17MRmmm97Xbr_J0e8oTWTIllOI8fNrLprlWMEHkhFIm8GTJdOG1rRLJVCsepW4JW7ZiTDHWO7RaU1yKz7btiEN_rL7LZQTfdufHOHjVIr64apWkxk4sYk4NSl5IVUFl4lndV4XALkjrlNBc3wXu87VPxBx233CIYCQG"/>
<div class="flex-1">
<p class="font-label-bold text-label-bold text-on-surface leading-none">Secretaria</p>
<p class="font-label-muted text-label-muted text-[10px]">Administrador</p>
</div>
<div class="w-2 h-2 rounded-full bg-emerald-500 shadow-[0_0_8px_#10b981]"></div>
</div>
<a class="flex items-center gap-md p-sm text-on-surface-variant hover:text-error transition-colors rounded-lg" href="#">
<span class="material-symbols-outlined">logout</span>
<span class="font-label-bold text-label-bold">Sair do Sistema</span>
</a>
</div>
</aside>
<!-- Main Content Canvas -->
<main class="flex-1 ml-[260px] flex flex-col min-h-screen">
<!-- Top App Bar -->
<header class="w-full max-w-[calc(100vw-260px)] flex justify-between items-center py-lg px-xl bg-transparent">
<div class="flex items-center gap-md">
<a class="w-10 h-10 flex items-center justify-center rounded-lg bg-surface-container-high border border-outline-variant/20 text-on-surface-variant hover:text-primary hover:border-primary/50 transition-all group" href="#">
<span class="material-symbols-outlined text-[20px] group-hover:-translate-x-1 transition-transform">arrow_back</span>
</a>
<div>
<div class="flex items-center gap-sm text-primary mb-1">
<span class="material-symbols-outlined text-[20px]">person</span>
<span class="font-label-bold text-label-bold tracking-widest uppercase">Perfil do Aluno</span>
</div>
<h2 class="font-display-md text-display-md text-on-surface">Gestão de Alunos</h2>
</div>
</div>
<div class="flex items-center gap-md">
<div class="relative group">
<input class="neo-inset bg-surface-container-lowest text-on-surface rounded-full py-2 pl-10 pr-4 w-64 focus:ring-2 focus:ring-primary/50 border-none font-body-sm transition-all outline-none" placeholder="Pesquisar aluno..." type="text"/>
<span class="material-symbols-outlined absolute left-3 top-1/2 -translate-y-1/2 text-on-surface-variant text-[20px]">search</span>
</div>
<button class="w-10 h-10 flex items-center justify-center rounded-full hover:bg-surface-container-high text-on-surface-variant transition-colors relative">
<span class="material-symbols-outlined">notifications</span>
<span class="absolute top-2 right-2 w-2 h-2 bg-error rounded-full"></span>
</button>
<div class="w-[1px] h-8 bg-outline-variant/30"></div>
<button class="flex items-center gap-sm bg-primary-container text-on-primary-container font-label-bold text-label-bold px-4 py-2 rounded-lg shadow-lg hover:scale-105 active:scale-95 transition-all">
<span class="material-symbols-outlined text-[18px]">add</span>
                    Nova Matrícula
                </button>
</div>
</header>
<!-- Content Area -->
<div class="px-xl pb-xl space-y-lg">
<!-- Student Header Section -->
<div class="neo-card rounded-xl p-lg flex flex-col md:flex-row gap-lg items-center relative overflow-hidden">
<!-- Abstract BG Glow -->
<div class="absolute -top-24 -right-24 w-64 h-64 bg-primary/10 rounded-full blur-[100px]"></div>
<div class="relative group">
<img class="w-40 h-40 rounded-xl object-cover shadow-2xl border-4 border-surface-container-high" data-alt="A focused headshot of a young student, male, around 15 years old, looking directly at the camera with a confident expression, wearing a navy blue school uniform, studio lighting against a soft dark blue background, high clarity enterprise CRM profile style." src="https://lh3.googleusercontent.com/aida-public/AB6AXuB5gXPgBE9zQrYI2dW77BIyfquiENw-wNcFwtGC9FrXntFc6bcwdf137RYoz9Xe_PJzsR5m957xSNM4QuSNeK4mndlSU4fFa1IfTAnO8m4v0GL5e5faVhjG_u3UCLhad5yNIiUjc4mJmpweJWN8vjQJvXkrdAq4TFJrFAdI6MLdJxbAKWvkJNIysl-doR67RoYlauWYujBdjTuNMP5M-zDhcqAnDZcNfYyRBklUAKxknF1aGewqQDlDdQ5VyqOINJSivRl-YtSHrNIp"/>
<button class="absolute -bottom-2 -right-2 bg-primary text-on-primary-fixed w-10 h-10 rounded-lg flex items-center justify-center shadow-lg hover:scale-110 transition-transform">
<span class="material-symbols-outlined text-[20px]">photo_camera</span>
</button>
</div>
<div class="flex-1 space-y-sm text-center md:text-left">
<div class="space-y-1">
<div class="flex items-center justify-center md:justify-start gap-md">
<h3 class="font-display-lg text-display-lg text-on-surface">João Pedro da Silva</h3>
<span class="bg-primary/20 text-primary px-3 py-1 rounded-full font-label-bold text-[10px] uppercase tracking-widest border border-primary/30">Ativo</span>
</div>
<p class="text-on-surface-variant font-body-md">ID: SM-2024-00125 • Inscrito em: 15 de Jan, 2024</p>
</div>
<div class="flex flex-wrap justify-center md:justify-start gap-md">
<div class="neo-inset rounded-lg px-4 py-2 flex items-center gap-3">
<span class="material-symbols-outlined text-primary" style="font-variation-settings: 'FILL' 1;">school</span>
<div>
<p class="font-label-muted text-label-muted text-[10px] uppercase">Turma</p>
<p class="font-label-bold text-on-surface">8ª Classe - Sala A</p>
</div>
</div>
<div class="neo-inset rounded-lg px-4 py-2 flex items-center gap-3">
<span class="material-symbols-outlined text-tertiary" style="font-variation-settings: 'FILL' 1;">calendar_today</span>
<div>
<p class="font-label-muted text-label-muted text-[10px] uppercase">Período</p>
<p class="font-label-bold text-on-surface">Manhã (07:30 - 12:30)</p>
</div>
</div>
</div>
</div>
<div class="flex flex-col gap-sm w-full md:w-auto">
<button class="flex items-center justify-center gap-2 px-6 py-2.5 bg-primary text-on-primary-container font-label-bold text-label-bold rounded-lg shadow-lg hover:brightness-110 active:scale-95 transition-all">
<span class="material-symbols-outlined text-[20px]">edit</span>
                        Editar Perfil
                    </button>
<button class="flex items-center justify-center gap-2 px-6 py-2.5 bg-surface-container-high text-on-surface font-label-bold text-label-bold rounded-lg border border-outline-variant/20 hover:bg-surface-variant transition-all">
<span class="material-symbols-outlined text-[20px]">badge</span>
                        Imprimir Cartão
                    </button>
<button class="flex items-center justify-center gap-2 px-6 py-2.5 bg-surface-container-high text-on-surface font-label-bold text-label-bold rounded-lg border border-outline-variant/20 hover:bg-surface-variant transition-all">
<span class="material-symbols-outlined text-[20px]">chat</span>
                        Mensagem Encarregado
                    </button>
</div>
</div>
<!-- Dashboard Grid -->
<div class="grid grid-cols-12 gap-lg">
<!-- Left Column: Details -->
<div class="col-span-12 lg:col-span-8 space-y-lg">
<!-- Information Tabs -->
<div class="neo-card rounded-xl overflow-hidden">
<div class="flex border-b border-outline-variant/10 bg-surface-container-high/30">
<button class="px-6 py-4 text-primary font-label-bold text-label-bold border-b-2 border-primary">Dados Pessoais</button>
<button class="px-6 py-4 text-on-surface-variant font-label-bold text-label-bold hover:text-on-surface transition-colors">Encarregado</button>
<button class="px-6 py-4 text-on-surface-variant font-label-bold text-label-bold hover:text-on-surface transition-colors">Documentação</button>
</div>
<div class="p-lg grid grid-cols-2 gap-xl">
<div class="space-y-sm">
<h4 class="font-label-bold text-primary uppercase text-[11px] tracking-widest mb-4">Informação Básica</h4>
<div class="space-y-4">
<div>
<label class="font-label-muted text-label-muted block mb-1">Nome Completo</label>
<p class="font-body-md text-on-surface">João Pedro de Oliveira da Silva</p>
</div>
<div class="grid grid-cols-2 gap-md">
<div>
<label class="font-label-muted text-label-muted block mb-1">Data de Nascimento</label>
<p class="font-body-md text-on-surface">12 de Maio, 2009</p>
</div>
<div>
<label class="font-label-muted text-label-muted block mb-1">Gênero</label>
<p class="font-body-md text-on-surface">Masculino</p>
</div>
</div>
<div>
<label class="font-label-muted text-label-muted block mb-1">Nacionalidade</label>
<p class="font-body-md text-on-surface">Angolana</p>
</div>
</div>
</div>
<div class="space-y-sm">
<h4 class="font-label-bold text-primary uppercase text-[11px] tracking-widest mb-4">Endereço &amp; Contacto</h4>
<div class="space-y-4">
<div>
<label class="font-label-muted text-label-muted block mb-1">Endereço Residencial</label>
<p class="font-body-md text-on-surface">Bairro Talatona, Rua 12, Vivenda 45A, Luanda</p>
</div>
<div>
<label class="font-label-muted text-label-muted block mb-1">Telefone Principal</label>
<p class="font-body-md text-on-surface">+244 923 456 789</p>
</div>
<div>
<label class="font-label-muted text-label-muted block mb-1">E-mail</label>
<p class="font-body-md text-on-surface">joao.silva@estudante.com</p>
</div>
</div>
</div>
</div>
</div>
<!-- Payment History Table -->
<div class="neo-card rounded-xl">
<div class="p-lg flex justify-between items-center border-b border-outline-variant/10">
<h4 class="font-display-md text-headline-sm text-on-surface">Histórico de Pagamentos</h4>
<button class="text-primary font-label-bold text-label-bold hover:underline">Ver tudo</button>
</div>
<div class="overflow-x-auto">
<table class="w-full text-left">
<thead class="bg-surface-container-high/50">
<tr>
<th class="px-lg py-4 font-label-bold text-label-bold text-on-surface-variant uppercase">Mês Ref.</th>
<th class="px-lg py-4 font-label-bold text-label-bold text-on-surface-variant uppercase">Recibo</th>
<th class="px-lg py-4 font-label-bold text-label-bold text-on-surface-variant uppercase">Valor</th>
<th class="px-lg py-4 font-label-bold text-label-bold text-on-surface-variant uppercase">Data</th>
<th class="px-lg py-4 font-label-bold text-label-bold text-on-surface-variant uppercase">Status</th>
</tr>
</thead>
<tbody class="divide-y divide-outline-variant/10">
<tr class="hover:bg-surface-variant/20 transition-colors">
<td class="px-lg py-4 font-body-md text-on-surface">Abril 2024</td>
<td class="px-lg py-4 font-body-md text-on-surface-variant">#REC-4560</td>
<td class="px-lg py-4 font-data-numeric text-data-numeric text-on-surface">25.000 Kz</td>
<td class="px-lg py-4 font-body-sm text-on-surface-variant">02/04/2024</td>
<td class="px-lg py-4">
<span class="flex items-center gap-1.5 text-emerald-500 font-label-bold text-[11px] uppercase">
<span class="w-1.5 h-1.5 rounded-full bg-emerald-500"></span> Pago
                                            </span>
</td>
</tr>
<tr class="hover:bg-surface-variant/20 transition-colors">
<td class="px-lg py-4 font-body-md text-on-surface">Março 2024</td>
<td class="px-lg py-4 font-body-md text-on-surface-variant">#REC-4412</td>
<td class="px-lg py-4 font-data-numeric text-data-numeric text-on-surface">25.000 Kz</td>
<td class="px-lg py-4 font-body-sm text-on-surface-variant">05/03/2024</td>
<td class="px-lg py-4">
<span class="flex items-center gap-1.5 text-emerald-500 font-label-bold text-[11px] uppercase">
<span class="w-1.5 h-1.5 rounded-full bg-emerald-500"></span> Pago
                                            </span>
</td>
</tr>
<tr class="hover:bg-surface-variant/20 transition-colors">
<td class="px-lg py-4 font-body-md text-on-surface">Fevereiro 2024</td>
<td class="px-lg py-4 font-body-md text-on-surface-variant">#REC-4288</td>
<td class="px-lg py-4 font-data-numeric text-data-numeric text-on-surface">25.000 Kz</td>
<td class="px-lg py-4 font-body-sm text-on-surface-variant">01/02/2024</td>
<td class="px-lg py-4">
<span class="flex items-center gap-1.5 text-emerald-500 font-label-bold text-[11px] uppercase">
<span class="w-1.5 h-1.5 rounded-full bg-emerald-500"></span> Pago
                                            </span>
</td>
</tr>
</tbody>
</table>
</div>
</div>
</div>
<!-- Right Column: Financial & Quick Widgets -->
<div class="col-span-12 lg:col-span-4 space-y-lg">
<!-- Financial Status Card -->
<div class="neo-card rounded-xl p-lg bg-gradient-to-br from-primary-container/20 to-transparent border border-primary/20">
<div class="flex items-center gap-3 mb-6">
<div class="w-10 h-10 rounded-lg bg-primary/20 flex items-center justify-center">
<span class="material-symbols-outlined text-primary">account_balance_wallet</span>
</div>
<h4 class="font-display-md text-headline-sm text-on-surface">Situação Financeira</h4>
</div>
<div class="space-y-6">
<div>
<p class="font-label-muted text-label-muted text-[10px] uppercase tracking-widest mb-1">Saldo Devedor</p>
<p class="font-data-numeric text-display-lg text-error">0,00 Kz</p>
<p class="font-body-sm text-emerald-500 mt-1 flex items-center gap-1">
<span class="material-symbols-outlined text-[14px]">check_circle</span> Conta em dia
                                </p>
</div>
<div class="p-md neo-inset rounded-lg space-y-4">
<div class="flex justify-between items-center">
<span class="font-label-bold text-label-bold text-on-surface-variant">Propinas Pagas</span>
<span class="font-label-bold text-label-bold text-primary">4 / 10 Meses</span>
</div>
<div class="w-full h-2 bg-surface-container-high rounded-full overflow-hidden">
<div class="w-[40%] h-full bg-primary shadow-[0_0_8px_rgba(173,198,255,0.5)]"></div>
</div>
<div class="flex justify-between items-center text-[10px] font-label-muted text-label-muted uppercase">
<span>Início: Jan</span>
<span>Término: Out</span>
</div>
</div>
<button class="w-full py-3 bg-primary-container text-on-primary-container font-label-bold text-label-bold rounded-lg shadow-lg hover:brightness-110 transition-all flex items-center justify-center gap-2">
<span class="material-symbols-outlined">payments</span>
                                Efetuar Pagamento
                            </button>
</div>
</div>
<!-- Enrollment Details Widget -->
<div class="neo-card rounded-xl p-lg space-y-md">
<h4 class="font-label-bold text-primary uppercase text-[11px] tracking-widest border-b border-outline-variant/10 pb-2">Detalhes da Matrícula</h4>
<div class="space-y-4">
<div class="flex justify-between">
<span class="text-on-surface-variant font-body-sm">Curso:</span>
<span class="text-on-surface font-label-bold">Ensino Geral</span>
</div>
<div class="flex justify-between">
<span class="text-on-surface-variant font-body-sm">Ano Lectivo:</span>
<span class="text-on-surface font-label-bold">2024 / 2025</span>
</div>
<div class="flex justify-between">
<span class="text-on-surface-variant font-body-sm">Status:</span>
<span class="text-emerald-500 font-label-bold">Regular</span>
</div>
<div class="flex justify-between">
<span class="text-on-surface-variant font-body-sm">Data de Início:</span>
<span class="text-on-surface font-label-bold">15/01/2024</span>
</div>
</div>
</div>
<!-- Quick Actions -->
<div class="grid grid-cols-2 gap-md">
<button class="neo-card p-md rounded-xl flex flex-col items-center gap-2 hover:bg-surface-variant transition-all">
<span class="material-symbols-outlined text-primary">description</span>
<span class="text-[10px] font-label-bold text-on-surface uppercase">Boletim</span>
</button>
<button class="neo-card p-md rounded-xl flex flex-col items-center gap-2 hover:bg-surface-variant transition-all">
<span class="material-symbols-outlined text-tertiary">history_edu</span>
<span class="text-[10px] font-label-bold text-on-surface uppercase">Faltas</span>
</button>
</div>
</div>
</div>
</div>
<!-- Footer Navigation Shell -->
<footer class="mt-auto border-t border-outline-variant/10 py-md px-xl flex justify-between items-center text-on-surface-variant">
<p class="font-label-muted text-label-muted">School Manager v1.0.0 | Todos os direitos reservados</p>
<div class="flex items-center gap-md">
<span class="flex items-center gap-1 font-label-muted text-label-muted">
<span class="w-1.5 h-1.5 rounded-full bg-emerald-500"></span> Backup: OK
                </span>
<div class="w-[1px] h-4 bg-outline-variant/30"></div>
<p class="font-label-muted text-label-muted">Suporte: +244 900 000 000</p>
</div>
</footer>
</main>
<script>
        // Simple micro-interactions
        document.querySelectorAll('button, a').forEach(el => {
            el.addEventListener('mousedown', () => {
                el.style.transform = 'scale(0.96)';
            });
            el.addEventListener('mouseup', () => {
                el.style.transform = '';
            });
            el.addEventListener('mouseleave', () => {
                el.style.transform = '';
            });
        });

        // Search Bar focus effect
        const searchInput = document.querySelector('input[type="text"]');
        if (searchInput) {
            searchInput.addEventListener('focus', () => {
                searchInput.parentElement.classList.add('ring-2', 'ring-primary/30');
            });
            searchInput.addEventListener('blur', () => {
                searchInput.parentElement.classList.remove('ring-2', 'ring-primary/30');
            });
        }
    </script>
</body></html>




<!DOCTYPE html>

<html class="dark" lang="pt-AO"><head>
<meta charset="utf-8"/>
<meta content="width=device-width, initial-scale=1.0" name="viewport"/>
<title>Gestão Escolar - Módulo Relatórios</title>
<script src="https://cdn.tailwindcss.com?plugins=forms,container-queries"></script>
<link href="https://fonts.googleapis.com/css2?family=Inter:wght@400;500;600;700&amp;family=Poppins:wght@500;600;700&amp;display=swap" rel="stylesheet"/>
<link href="https://fonts.googleapis.com/css2?family=Material+Symbols+Outlined:wght,FILL@100..700,0..1&amp;display=swap" rel="stylesheet"/>
<link href="https://fonts.googleapis.com/css2?family=Material+Symbols+Outlined:wght,FILL@100..700,0..1&amp;display=swap" rel="stylesheet"/>
<style>
        :root {
            --neo-shadow: 20px 20px 40px rgba(0, 0, 0, 0.4), -5px -5px 15px rgba(255, 255, 255, 0.02);
            --neo-inner: inset 1px 1px 1px rgba(255, 255, 255, 0.05);
            --neo-pressed: inset 4px 4px 8px rgba(0, 0, 0, 0.5), inset -1px -1px 2px rgba(255, 255, 255, 0.05);
        }

        body {
            background-color: #0b1326;
            color: #dae2fd;
            overflow-x: hidden;
        }

        .neo-card {
            background: #171f33;
            box-shadow: var(--neo-shadow);
            border: 1px solid rgba(255, 255, 255, 0.03);
            position: relative;
            overflow: hidden;
        }

        .neo-card::before {
            content: '';
            position: absolute;
            top: 0;
            left: 0;
            right: 0;
            height: 1px;
            background: linear-gradient(90deg, transparent, rgba(173, 198, 255, 0.1), transparent);
        }

        .neo-button-primary {
            background: linear-gradient(145deg, #3B82F6, #2563EB);
            box-shadow: 0px 4px 12px rgba(59, 130, 246, 0.3);
            transition: all 0.2s ease;
        }

        .neo-button-primary:active {
            transform: scale(0.95);
            box-shadow: inset 2px 2px 5px rgba(0,0,0,0.3);
        }

        .neo-input {
            background: #0b1326;
            box-shadow: inset 2px 2px 5px rgba(0, 0, 0, 0.3);
            border: 1px solid #334155;
        }

        .glass-accent {
            background: rgba(255, 255, 255, 0.03);
            backdrop-filter: blur(12px);
            border: 1px solid rgba(255, 255, 255, 0.05);
        }

        .material-symbols-outlined {
            font-variation-settings: 'FILL' 0, 'wght' 400, 'GRAD' 0, 'opsz' 24;
            vertical-align: middle;
        }

        .active-glow {
            box-shadow: 0 0 15px rgba(173, 198, 255, 0.2);
        }
    </style>
<script id="tailwind-config">
      tailwind.config = {
        darkMode: "class",
        theme: {
          extend: {
            "colors": {
                    "on-secondary": "#263143",
                    "on-tertiary-fixed": "#001c39",
                    "error": "#ffb4ab",
                    "surface-container": "#171f33",
                    "error-container": "#93000a",
                    "on-surface-variant": "#c2c6d6",
                    "surface-bright": "#31394d",
                    "surface-tint": "#adc6ff",
                    "on-tertiary-container": "#002a51",
                    "on-primary-fixed-variant": "#004395",
                    "tertiary-fixed": "#d4e3ff",
                    "surface-dim": "#0b1326",
                    "outline": "#8c909f",
                    "secondary": "#bcc7de",
                    "on-tertiary": "#00315d",
                    "surface-container-low": "#131b2e",
                    "outline-variant": "#424754",
                    "secondary-fixed-dim": "#bcc7de",
                    "on-primary-container": "#00285d",
                    "on-background": "#dae2fd",
                    "surface-container-lowest": "#060e20",
                    "surface": "#0b1326",
                    "tertiary-fixed-dim": "#a4c9ff",
                    "on-primary": "#002e6a",
                    "primary-container": "#4d8eff",
                    "inverse-surface": "#dae2fd",
                    "surface-container-high": "#222a3d",
                    "on-error-container": "#ffdad6",
                    "on-secondary-fixed-variant": "#3c475a",
                    "background": "#0b1326",
                    "on-tertiary-fixed-variant": "#004883",
                    "on-error": "#690005",
                    "secondary-fixed": "#d8e3fb",
                    "primary-fixed": "#d8e2ff",
                    "on-primary-fixed": "#001a42",
                    "tertiary-container": "#4c93e7",
                    "primary": "#adc6ff",
                    "tertiary": "#a4c9ff",
                    "surface-container-highest": "#2d3449",
                    "surface-variant": "#2d3449",
                    "on-surface": "#dae2fd",
                    "secondary-container": "#3e495d",
                    "on-secondary-container": "#aeb9d0",
                    "inverse-primary": "#005ac2",
                    "primary-fixed-dim": "#adc6ff",
                    "on-secondary-fixed": "#111c2d",
                    "inverse-on-surface": "#283044"
            },
            "borderRadius": {
                    "DEFAULT": "0.25rem",
                    "lg": "0.5rem",
                    "xl": "0.75rem",
                    "full": "9999px"
            },
            "spacing": {
                    "sidebar_width": "260px",
                    "lg": "24px",
                    "base": "4px",
                    "sm": "12px",
                    "xl": "32px",
                    "md": "16px",
                    "xs": "8px",
                    "gutter": "20px"
            },
            "fontFamily": {
                    "body-lg": ["Inter"],
                    "label-muted": ["Inter"],
                    "display-lg": ["Poppins"],
                    "label-bold": ["Inter"],
                    "data-numeric": ["Inter"],
                    "headline-sm": ["Poppins"],
                    "body-md": ["Inter"],
                    "body-sm": ["Inter"],
                    "display-md": ["Poppins"]
            },
            "fontSize": {
                    "body-lg": ["16px", {"lineHeight": "1.5", "fontWeight": "400"}],
                    "label-muted": ["12px", {"lineHeight": "1", "fontWeight": "400"}],
                    "display-lg": ["32px", {"lineHeight": "1.2", "letterSpacing": "-0.02em", "fontWeight": "600"}],
                    "label-bold": ["12px", {"lineHeight": "1", "letterSpacing": "0.05em", "fontWeight": "600"}],
                    "data-numeric": ["20px", {"lineHeight": "1", "letterSpacing": "-0.01em", "fontWeight": "700"}],
                    "headline-sm": ["18px", {"lineHeight": "1.4", "fontWeight": "500"}],
                    "body-md": ["14px", {"lineHeight": "1.5", "fontWeight": "400"}],
                    "body-sm": ["13px", {"lineHeight": "1.5", "fontWeight": "400"}],
                    "display-md": ["24px", {"lineHeight": "1.3", "fontWeight": "600"}]
            }
          },
        },
      }
    </script>
</head>
<body class="bg-background text-on-surface font-body-md">
<!-- Fixed SideNavBar -->
<aside class="fixed left-0 top-0 h-full w-[260px] bg-surface-container-low flex flex-col py-lg shadow-[20px_0_40px_rgba(0,0,0,0.4)] z-50">
<div class="px-md mb-xl flex items-center gap-sm">
<div class="w-10 h-10 bg-primary rounded-lg flex items-center justify-center text-on-primary shadow-[0_0_15px_rgba(173,198,255,0.4)]">
<span class="material-symbols-outlined text-display-md" data-icon="school">school</span>
</div>
<div>
<h1 class="font-display-md text-headline-sm font-bold text-primary">Gestão Escolar</h1>
<p class="font-body-sm text-on-surface-variant opacity-60">Administração Central</p>
</div>
</div>
<nav class="flex-1 space-y-base px-base">
<a class="flex items-center gap-md px-md py-sm text-on-surface-variant hover:bg-surface-container-high transition-all rounded-r-full group" href="../dashboard_school_manager_refatorado/code.html">
<span class="material-symbols-outlined" data-icon="dashboard">dashboard</span>
<span class="font-body-md">Dashboard</span>
</a>
<a class="flex items-center gap-md px-md py-sm text-on-surface-variant hover:bg-surface-container-high transition-all rounded-r-full group" href="../gest_o_de_alunos_school_manager/code.html">
<span class="material-symbols-outlined" data-icon="group">group</span>
<span class="font-body-md">Alunos</span>
</a>
<a class="flex items-center gap-md px-md py-sm text-on-surface-variant hover:bg-surface-container-high transition-all rounded-r-full group" href="../financeiro_pagamentos_school_manager/code.html">
<span class="material-symbols-outlined" data-icon="account_balance_wallet">account_balance_wallet</span>
<span class="font-body-md">Financeiro</span>
</a>
<a class="flex items-center gap-md px-md py-sm bg-primary text-on-primary rounded-r-full shadow-[0_4px_12px_rgba(59,130,246,0.3)]" href="code.html">
<span class="material-symbols-outlined" data-icon="analytics" style="font-variation-settings: 'FILL' 1;">analytics</span>
<span class="font-body-md font-bold">Relatórios</span>
</a>
<a class="flex items-center gap-md px-md py-sm text-on-surface-variant hover:bg-surface-container-high transition-all rounded-r-full group" href="../escola_classes_school_manager/code.html">
<span class="material-symbols-outlined" data-icon="school">school</span>
<span class="font-body-md">Escola</span>
</a>
<a class="flex items-center gap-md px-md py-sm text-on-surface-variant hover:bg-surface-container-high transition-all rounded-r-full group" href="../configura_es_do_sistema_school_manager/code.html">
<span class="material-symbols-outlined" data-icon="settings">settings</span>
<span class="font-body-md">Configurações</span>
</a>
</nav>
<div class="mt-auto px-md pt-lg">
<div class="p-md rounded-xl glass-accent flex items-center gap-sm">
<img class="w-10 h-10 rounded-full border border-primary/30" data-alt="A professional portrait of a senior school administrator in a high-quality, dark-themed office environment. The lighting is dramatic and moody, reflecting a high-stakes, technological prestige aesthetic. The man is wearing a crisp dark suit, suggesting authority and precision. The overall visual style is sleek, cinematic, and professional, perfectly aligning with an enterprise software interface." src="https://lh3.googleusercontent.com/aida-public/AB6AXuD4qzdq18b0_f3gI4RNbzBz9Rikxr1sVer1xC5OlARi-npgCgKBwp3EVWdAQenHyimssoCNKjzdIeLTUj28CTw_rUT7JQDP68EJELNfAR87TBDNlyoV7pxu2BeuetkSi0zn1ivHABaWqmmdhb44J5ENWf6R9TOyD0uWRYAqQaqvJmT_SMDfwheFWGI1d5exf_TTzlWmi_Q3NXy58IVoKuH18V5qK0RV8XAY5lby-4UlF6KgN6DXKHqZfds0NDhn6L9mpEeKgAp4lYkc"/>
<div class="overflow-hidden">
<p class="font-label-bold text-on-surface truncate">Dr. Mendes</p>
<p class="text-xs text-on-surface-variant truncate">Administrador</p>
</div>
</div>
</div>
</aside>
<!-- TopAppBar -->
<header class="fixed top-0 right-0 w-[calc(100%-260px)] h-16 bg-surface flex justify-between items-center px-xl border-b border-outline-variant/20 z-40">
<div class="flex items-center gap-md">
<span class="material-symbols-outlined text-primary" data-icon="analytics">analytics</span>
<h2 class="font-headline-sm font-semibold text-on-surface">Módulo de Relatórios</h2>
</div>
<div class="flex items-center gap-lg">
<div class="relative group">
<div class="absolute inset-y-0 left-0 pl-md flex items-center pointer-events-none">
<span class="material-symbols-outlined text-on-surface-variant" data-icon="search">search</span>
</div>
<input class="neo-input w-64 pl-xl pr-md py-xs rounded-full text-body-sm focus:ring-2 focus:ring-primary focus:outline-none transition-all" placeholder="Procurar relatórios..." type="text"/>
</div>
<div class="flex items-center gap-md">
<button class="w-10 h-10 flex items-center justify-center rounded-full hover:bg-surface-container-highest transition-all text-on-surface-variant">
<span class="material-symbols-outlined" data-icon="notifications">notifications</span>
</button>
<button class="w-10 h-10 flex items-center justify-center rounded-full hover:bg-surface-container-highest transition-all text-on-surface-variant">
<span class="material-symbols-outlined" data-icon="account_circle">account_circle</span>
</button>
</div>
</div>
</header>
<!-- Main Content Canvas -->
<main class="ml-[260px] pt-24 px-xl pb-20 min-h-screen">
<!-- Welcome Header -->
<div class="mb-xl">
<h3 class="font-display-md text-display-md text-primary mb-xs">Inteligência Administrativa</h3>
<p class="font-body-lg text-on-surface-variant max-w-2xl">Acesse e gere relatórios detalhados sobre a performance financeira e o fluxo acadêmico da instituição.</p>
</div>
<!-- Dashboard Grid -->
<div class="grid grid-cols-12 gap-lg">
<!-- Category: Financial Reports -->
<section class="col-span-12 lg:col-span-8 space-y-lg">
<div class="flex items-center justify-between">
<h4 class="font-headline-sm flex items-center gap-sm">
<span class="w-1.5 h-6 bg-primary rounded-full"></span>
                        Relatórios Financeiros
                    </h4>
<span class="text-xs font-label-bold text-on-surface-variant uppercase tracking-widest">Atualizado em tempo real</span>
</div>
<div class="grid grid-cols-3 gap-lg">
<!-- Card: Matrículas -->
<div class="neo-card p-lg rounded-xl flex flex-col h-full hover:border-primary/40 transition-all cursor-pointer group">
<div class="flex items-start justify-between mb-md">
<div class="w-12 h-12 bg-primary/10 rounded-xl flex items-center justify-center text-primary border border-primary/20 shadow-inner group-hover:bg-primary group-hover:text-on-primary transition-all duration-300">
<span class="material-symbols-outlined text-display-md" data-icon="assignment_ind">assignment_ind</span>
</div>
<span class="material-symbols-outlined text-on-surface-variant opacity-30" data-icon="more_vert">more_vert</span>
</div>
<h5 class="font-headline-sm mb-xs">Matrículas</h5>
<p class="text-body-sm text-on-surface-variant mb-lg flex-1">Relatórios consolidados de inscrições, renovações e desistências por período.</p>
<div class="flex items-center gap-sm mt-auto">
<button class="neo-button-primary px-md py-xs rounded-lg text-body-sm font-semibold flex items-center gap-xs">
<span class="material-symbols-outlined text-sm" data-icon="preview">preview</span>
                                Ver
                            </button>
<div class="flex gap-xs">
<button class="w-8 h-8 rounded-lg bg-surface-container-highest flex items-center justify-center hover:text-primary transition-colors">
<span class="material-symbols-outlined text-sm" data-icon="picture_as_pdf">picture_as_pdf</span>
</button>
<button class="w-8 h-8 rounded-lg bg-surface-container-highest flex items-center justify-center hover:text-primary transition-colors">
<span class="material-symbols-outlined text-sm" data-icon="table_chart">table_chart</span>
</button>
</div>
</div>
</div>
<!-- Card: Propinas -->
<div class="neo-card p-lg rounded-xl flex flex-col h-full hover:border-primary/40 transition-all cursor-pointer group">
<div class="flex items-start justify-between mb-md">
<div class="w-12 h-12 bg-primary/10 rounded-xl flex items-center justify-center text-primary border border-primary/20 shadow-inner group-hover:bg-primary group-hover:text-on-primary transition-all duration-300">
<span class="material-symbols-outlined text-display-md" data-icon="payments">payments</span>
</div>
<span class="material-symbols-outlined text-on-surface-variant opacity-30" data-icon="more_vert">more_vert</span>
</div>
<h5 class="font-headline-sm mb-xs">Propinas</h5>
<p class="text-body-sm text-on-surface-variant mb-lg flex-1">Análise de pagamentos mensais, alunos devedores e histórico de multas aplicadas.</p>
<div class="flex items-center gap-sm mt-auto">
<button class="neo-button-primary px-md py-xs rounded-lg text-body-sm font-semibold flex items-center gap-xs">
<span class="material-symbols-outlined text-sm" data-icon="preview">preview</span>
                                Ver
                            </button>
<div class="flex gap-xs">
<button class="w-8 h-8 rounded-lg bg-surface-container-highest flex items-center justify-center hover:text-primary transition-colors">
<span class="material-symbols-outlined text-sm" data-icon="picture_as_pdf">picture_as_pdf</span>
</button>
<button class="w-8 h-8 rounded-lg bg-surface-container-highest flex items-center justify-center hover:text-primary transition-colors">
<span class="material-symbols-outlined text-sm" data-icon="table_chart">table_chart</span>
</button>
</div>
</div>
</div>
<!-- Card: Fluxo de Caixa -->
<div class="neo-card p-lg rounded-xl flex flex-col h-full hover:border-primary/40 transition-all cursor-pointer group border-l-2 border-l-primary/30">
<div class="flex items-start justify-between mb-md">
<div class="w-12 h-12 bg-primary/20 rounded-xl flex items-center justify-center text-primary border border-primary/40 shadow-inner group-hover:bg-primary group-hover:text-on-primary transition-all duration-300">
<span class="material-symbols-outlined text-display-md" data-icon="account_balance">account_balance</span>
</div>
<span class="material-symbols-outlined text-on-surface-variant opacity-30" data-icon="star" style="font-variation-settings: 'FILL' 1;">star</span>
</div>
<h5 class="font-headline-sm mb-xs">Fluxo de Caixa</h5>
<p class="text-body-sm text-on-surface-variant mb-lg flex-1">Visão holística de entradas e saídas. Balanço mensal e projeção anual.</p>
<div class="flex items-center gap-sm mt-auto">
<button class="neo-button-primary px-md py-xs rounded-lg text-body-sm font-semibold flex items-center gap-xs">
<span class="material-symbols-outlined text-sm" data-icon="preview">preview</span>
                                Ver
                            </button>
<div class="flex gap-xs">
<button class="w-8 h-8 rounded-lg bg-surface-container-highest flex items-center justify-center hover:text-primary transition-colors">
<span class="material-symbols-outlined text-sm" data-icon="picture_as_pdf">picture_as_pdf</span>
</button>
<button class="w-8 h-8 rounded-lg bg-surface-container-highest flex items-center justify-center hover:text-primary transition-colors">
<span class="material-symbols-outlined text-sm" data-icon="table_chart">table_chart</span>
</button>
</div>
</div>
</div>
</div>
<!-- Secondary Section: Administrative -->
<div class="pt-lg">
<h4 class="font-headline-sm flex items-center gap-sm mb-lg">
<span class="w-1.5 h-6 bg-tertiary rounded-full"></span>
                        Relatórios Administrativos
                    </h4>
<div class="grid grid-cols-2 gap-lg">
<div class="neo-card flex items-center gap-lg p-lg rounded-xl hover:bg-surface-container-high transition-all">
<div class="p-lg bg-surface-container-highest rounded-full">
<span class="material-symbols-outlined text-display-lg text-tertiary" data-icon="format_list_bulleted">format_list_bulleted</span>
</div>
<div class="flex-1">
<h6 class="font-headline-sm mb-base">Lista de Alunos</h6>
<p class="text-body-sm text-on-surface-variant">Listagens por turma, género e aproveitamento académico.</p>
</div>
<button class="w-12 h-12 neo-input rounded-full flex items-center justify-center hover:bg-primary hover:text-on-primary transition-all">
<span class="material-symbols-outlined" data-icon="chevron_right">chevron_right</span>
</button>
</div>
<div class="neo-card flex items-center gap-lg p-lg rounded-xl hover:bg-surface-container-high transition-all">
<div class="p-lg bg-surface-container-highest rounded-full">
<span class="material-symbols-outlined text-display-lg text-tertiary" data-icon="insights">insights</span>
</div>
<div class="flex-1">
<h6 class="font-headline-sm mb-base">Estatísticas</h6>
<p class="text-body-sm text-on-surface-variant">Gráficos de evolução, taxa de retenção e métricas de desempenho.</p>
</div>
<button class="w-12 h-12 neo-input rounded-full flex items-center justify-center hover:bg-primary hover:text-on-primary transition-all">
<span class="material-symbols-outlined" data-icon="chevron_right">chevron_right</span>
</button>
</div>
</div>
</div>
</section>
<!-- Quick Actions & Stats Panel -->
<aside class="col-span-12 lg:col-span-4 space-y-lg">
<div class="neo-card rounded-xl p-lg bg-gradient-to-br from-surface-container to-surface-container-high relative">
<div class="absolute -top-10 -right-10 w-40 h-40 bg-primary/10 blur-[60px] rounded-full"></div>
<h5 class="font-headline-sm mb-lg flex items-center gap-xs">
<span class="material-symbols-outlined text-primary" data-icon="bolt">bolt</span>
                        Ações Rápidas
                    </h5>
<div class="space-y-md">
<button class="w-full text-left p-md bg-surface-container-low rounded-xl border border-outline-variant/10 hover:border-primary/40 transition-all flex items-center justify-between group">
<div>
<p class="font-label-bold text-on-surface group-hover:text-primary">Balanço Geral do Mês</p>
<p class="text-xs text-on-surface-variant">PDF • 1.2MB • Gerado hoje</p>
</div>
<span class="material-symbols-outlined" data-icon="download">download</span>
</button>
<button class="w-full text-left p-md bg-surface-container-low rounded-xl border border-outline-variant/10 hover:border-primary/40 transition-all flex items-center justify-between group">
<div>
<p class="font-label-bold text-on-surface group-hover:text-primary">Mapa de Inadimplência</p>
<p class="text-xs text-on-surface-variant">Excel • 450KB • Gerado ontem</p>
</div>
<span class="material-symbols-outlined" data-icon="download">download</span>
</button>
<button class="w-full py-md border-2 border-dashed border-outline-variant/30 rounded-xl text-center text-body-sm text-on-surface-variant hover:border-primary/50 hover:text-primary transition-all">
                            + Configurar Novo Relatório
                        </button>
</div>
</div>
<!-- Preview Panel / Recent Reports -->
<div class="neo-card rounded-xl overflow-hidden">
<div class="p-md bg-surface-container-highest border-b border-outline-variant/10 flex items-center justify-between">
<span class="font-label-bold text-primary">Pré-visualização Recente</span>
<span class="material-symbols-outlined text-sm opacity-50" data-icon="fullscreen">fullscreen</span>
</div>
<div class="h-64 relative bg-[#0b1326] p-lg flex items-center justify-center group overflow-hidden">
<!-- Abstract Data Visualization Placeholder -->
<div class="w-full h-full flex flex-col gap-sm opacity-40 group-hover:opacity-60 transition-opacity">
<div class="flex gap-xs items-end h-32 justify-around">
<div class="w-4 bg-primary rounded-t-full h-[30%]"></div>
<div class="w-4 bg-primary rounded-t-full h-[60%]"></div>
<div class="w-4 bg-primary rounded-t-full h-[45%]"></div>
<div class="w-4 bg-primary rounded-t-full h-[85%]"></div>
<div class="w-4 bg-primary rounded-t-full h-[70%]"></div>
<div class="w-4 bg-primary rounded-t-full h-[95%]"></div>
</div>
<div class="space-y-xs">
<div class="h-2 bg-on-surface/10 rounded w-full"></div>
<div class="h-2 bg-on-surface/10 rounded w-3/4"></div>
<div class="h-2 bg-on-surface/10 rounded w-1/2"></div>
</div>
</div>
<div class="absolute inset-0 flex flex-col items-center justify-center p-xl text-center bg-surface/60 backdrop-blur-sm opacity-0 group-hover:opacity-100 transition-opacity">
<span class="material-symbols-outlined text-display-lg text-primary mb-md" data-icon="lock">lock</span>
<p class="font-label-bold">Clique para Visualizar</p>
<p class="text-xs text-on-surface-variant">Selecione um relatório para gerar a prévia interativa aqui.</p>
</div>
</div>
</div>
</aside>
</div>
</main>
<!-- Footer -->
<footer class="fixed bottom-0 right-0 w-[calc(100%-260px)] h-8 bg-surface-container-lowest flex justify-between items-center px-xl z-40">
<div class="flex items-center gap-lg">
<p class="font-label-muted text-on-surface-variant/60">v2.4.1 | Status do Backup: Sincronizado</p>
</div>
<div class="flex items-center gap-md">
<a class="font-label-muted text-on-surface-variant/60 hover:text-primary transition-colors" href="#">Suporte</a>
<span class="w-1 h-1 bg-outline-variant/30 rounded-full"></span>
<a class="font-label-muted text-on-surface-variant/60 hover:text-primary transition-colors" href="#">Documentação</a>
</div>
</footer>
<script>
        // Micro-interactions and UI Logic
        document.querySelectorAll('.neo-card').forEach(card => {
            card.addEventListener('mouseenter', () => {
                card.style.transform = 'translateY(-4px)';
                card.style.boxShadow = '25px 25px 50px rgba(0, 0, 0, 0.5)';
            });
            card.addEventListener('mouseleave', () => {
                card.style.transform = 'translateY(0)';
                card.style.boxShadow = '20px 20px 40px rgba(0, 0, 0, 0.4)';
            });
        });

        // Simple mock for "Active" Navigation tracking
        const navLinks = document.querySelectorAll('aside nav a');
        navLinks.forEach(link => {
            link.addEventListener('click', (e) => {
                navLinks.forEach(l => {
                    l.classList.remove('bg-primary', 'text-on-primary', 'shadow-[0_4px_12px_rgba(59,130,246,0.3)]');
                    l.classList.add('text-on-surface-variant');
                });
                link.classList.add('bg-primary', 'text-on-primary', 'shadow-[0_4px_12px_rgba(59,130,246,0.3)]');
                link.classList.remove('text-on-surface-variant');
            });
        });
    </script>
</body></html>



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






