import { Component } from '@angular/core';
import { Routes } from '@angular/router';
import { Home } from './components/home/home';
import { CadastroPeca } from './components/cadastro-peca/cadastro-peca';
import { CadastroEstacao } from './components/cadastro-estacao/cadastro-estacao';
import { ListaPeca } from './components/lista-peca/lista-peca';
import { ListaEstacao } from './components/lista-estacao/lista-estacao';

export const routes: Routes = [
    {
        path: "home",
        component: Home
    },
    {
      path: "cadastro-peca",
      component: CadastroPeca
    },
    {
      path: "cadastro-estacao",
      component: CadastroEstacao
    },
    {
      path: "lista-peca",
      component: ListaPeca
    },
    {
      path: "lista-estacao",
      component: ListaEstacao
    }
    // {
    //   path: "rastreabilidade",
    //   component: Rastreabilidade
    // }
];
