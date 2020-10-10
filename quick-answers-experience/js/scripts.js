Vue.component("tree-item", {
    template: "#item-template",
    props: {
        item: Object
    },
    data: function () {
        return {
            isOpen: false,
            selected: {}
        }
    },
    computed: {
        isFolder: function () {
            return this.item.children && this.item.children.length;
        }
    },
    methods: {
        toggle: function() {
            if (this.isFolder) {
                this.isOpen = !this.isOpen;
            }
        },
        makeFolder: function() {
            if (!this.isFolder) {
                this.$emit("make-folder", this.item);
                this.isOpen = true;
            }
        },
        changeHandle: function(selected) {
            if (!this.isFolder) {
                this.$emit("update:typification");
            }
        }
    }
});

var app = new Vue({
    el: "#app",
    data: {
        name: "Quick Answers",
        customerService: {
            identifier: "",
            validator: {
                regex: ""
            },
            typification: {}
        },
        errors: {
            identifier: {
                "message": "",
                "status": false
            },
            customerService: {
                "message": "",
                "status": false
            }
        },
        search: {
            historic: {
                identifier: ""
            }
        },
        historic: [],
        typifications: [],
        tree: [],
        validators: [],
    },
    created() {
        this.init();
    },
    computed: {
        filterHistory: function() {
            return this.historic.filter(x => {
                if(this.search.historic.identifier == "") 
                    return true;

                return x.identifier.toLowerCase().indexOf(this.search.historic.identifier.toLowerCase()) > -1;
            });
        }
    },
    methods: {
        init: function() {
            
            this.typifications = [];

            this.customerService = {
                identifier: "",
                validator: {
                    regex: ""
                },
                typification: {}
            };   

            axios.get('https://localhost:5001/api/IdentificationValidator/')
                .then(function (response) {
                    app.$data.validators = response.data;
                })
                .catch(function (error) {
                    console.log(error);
                });

            axios.get('https://localhost:5001/api/Typification/Tree')
                .then(function (response) {
                    app.$data.typifications = response.data;
                })
                .catch(function (error) {
                    console.log(error);
                });

            axios.get('https://localhost:5001/api/CustomerService')
                .then(function (response) {
                    console.log(response.data);
                    app.$data.historic = response.data.map(function (x){
                        return {
                            customerServiceId: x.customerServiceId,
                            identifier: x.identifier,
                            typification: x.typification,
                            typificationId: x.typificationId,
                            validator: x.validator,
                            validatorId: x.validatorId,
                            startService: moment(new Date(x.startService)).format('DD/MM/YYYY HH:mm'),
                            endService: moment(new Date(x.endService)).format('DD/MM/YYYY HH:mm')
                        }
                    });
                })
                .catch(function (error) {
                    console.log(error);
                });

        },
        openHistoric: function () {
            $("#viewHistoric").modal('show');
        },
        
        validateIdentifier: function () {
            if(this.customerService.identifier != "" &&  new RegExp(this.customerService.validator.regex).test(this.customerService.identifier))
                return true;

            this.errors.identifier.status = true;

            if(!this.customerService.identifier)
                this.errors.identifier.message = "Insira o identificador do cliente!";
                
            if(!new RegExp(this.customerService.validator.regex).test(this.customerService.identifier))
                this.errors.identifier.message = "É preciso que o formato corresponda ao exigido!";

            return false;
        },
        validateCustomerService: function() {
            if(this.customerService.typification != undefined)
                return true;

            this.errors.customerService.status = true;

            if(this.customerService.typification == undefined)
                this.errors.customerService.message =  "Selecione uma tabulação!";

            return false;
        },
        startService: function (e) {
            if(this.validateIdentifier() == false)
                return;

            this.errors.identifier = {}
            
            axios.post('https://localhost:5001/api/CustomerService/', {
                    identifier: this.customerService.identifier,
                    validatorId: this.customerService.validator.id
                }).then(function (response) {

                    let customerService = response.data;
                    customerService.validator = app.$data.customerService.validator;

                    app.$data.customerService = customerService;           
                    $("#viewService").modal({
                        keyboard: false,
                        show: true,
                        backdrop: "static"
                    });

                }).catch(function (error) {
                    console.log(error);
                });

        },
        endService: function() {
            if(this.validateCustomerService() == false)
                return;
            
            axios.put('https://localhost:5001/api/CustomerService/' + this.customerService.customerServiceId, {
                    customerServiceId: this.customerService.customerServiceId,
                    identifier: this.customerService.identifier,
                    validatorId: this.customerService.validator.id,
                    typificationId: this.customerService.typification.id,
                    startService: this.customerService.startService,
                    endService: "0001-01-01T00:00:00"
                }).then(function (response) {

                    app.init();

                    $("#viewService").modal('hide');
                    
                    $(".toast").toast({
                        animation: true,
                        autohide: true,
                        delay: 3000
                    });
                    $(".toast").toast('show');

                }).catch(function (error) {
                    console.log(error);
                });
        
        }
    }
});
