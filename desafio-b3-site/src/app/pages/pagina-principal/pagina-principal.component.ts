import { CurrencyPipe } from '@angular/common';
import { Component, OnInit, ViewEncapsulation  } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { NegocioDataService } from 'src/app/services/negocio-data.service';

@Component({
  selector: 'app-pagina-principal',
  templateUrl: './pagina-principal.component.html',
  styleUrls: ['./pagina-principal.component.scss'],
  providers: [CurrencyPipe],

})
export class PaginaPrincipalComponent implements OnInit {
  form: FormGroup;

  resultadoBruto: any = '';
  resultadoLiquido: any = '';

  constructor(private fb: FormBuilder,
    private negocioService: NegocioDataService) {
    this.form = this.fb.group({
      valorMonetario: ['0', Validators.required],
      prazoMeses: ['', [Validators.required, Validators.pattern('^[0-9]+$')]]
    });
  }

  ngOnInit(): void {
    // Atualiza o valor formatado quando o valor monetário muda
    this.form.get('valorMonetario')?.valueChanges.subscribe(value => {
      const numericValue = this.parseCurrency(value);
      this.form.get('valorMonetario')?.setValue(numericValue, { emitEvent: false });
    });
  }

  onBlurValorMonetario() {
    const valor = this.form.get('valorMonetario')?.value;
    const numericValue = this.parseCurrency(valor);
    this.form.get('valorMonetario')?.setValue(this.formatCurrency(numericValue), { emitEvent: false });
  }

  onFocusValorMonetario() {
    const valor = this.form.get('valorMonetario')?.value;
    this.form.get('valorMonetario')?.setValue(this.parseCurrency(valor), { emitEvent: false });
  }

  parseCurrency(value: string): number {
    if (!value) return 0;
    const numericValue = parseFloat(value.replace(/[^\d.,]/g, '').replace(',', '.'));
    return isNaN(numericValue) ? 0 : numericValue;
  }

  formatCurrency(value: number): string {
    if (isNaN(value)) return '0';
    return value.toLocaleString('pt-BR', { style: 'currency', currency: 'BRL' });
  }

  calcular() {
    const valorMonetario = this.form.get('valorMonetario')?.value;
    const prazoMeses = this.form.get('prazoMeses')?.value;

    this.negocioService.GetCalculoCDB(valorMonetario, prazoMeses).subscribe(
      value => {
        this.resultadoBruto = value.data.resultadoBruto;
        this.resultadoLiquido = value.data.resultadoLiquido;
      },
      value =>  {

      })
  }

  limpar() {
    // Resetar o formulário para seus valores padrão
    this.form.reset({
      valorMonetario: '',
      prazoMeses: ''
    });

    // Garantir que os resultados também sejam limpos
    this.resultadoBruto = '';
    this.resultadoLiquido = '';

    // Se necessário, atualizar o formulário para refletir as mudanças
    this.form.markAsPristine();
    this.form.markAsUntouched();
  }
}
